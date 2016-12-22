USE [Quality_Control]
GO

/****** Object:  StoredProcedure [dbo].[SP_PROCESSAMENTO_FILTROS]    Script Date: 09/10/2013 15:35:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_PROCESSAMENTO_FILTROS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_PROCESSAMENTO_FILTROS]
GO

USE [Quality_Control]
GO

/****** Object:  StoredProcedure [dbo].[SP_PROCESSAMENTO_FILTROS]    Script Date: 09/10/2013 15:35:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_PROCESSAMENTO_FILTROS]
(
	@CD_USUARIO INT,  /*USUARIO QUE ACIONOU O PROCESSO, IMPORTANTE PARA OS LOGS*/
	@DT_REF VARCHAR(10) , /*DATA DO PROCESSAMENTO*/
	@LISTA_CD_CARTEIRA VARCHAR(MAX), /*LISTA COM A COLEÇÃO DE CÓDIGO DAS CARTEIRAS, SEPARADOS POR VÍRGULA, QUE O USUARIO SELECIONOU PARA REALIZAR O PROCESSO. SE VAZIO, CONSIDERAR TODAS AS CARTEIRAS*/
	@IN_TIPO_EXECUCAO CHAR(1) /*MANUAL (ACIONADO PELO USUÁRIO) OU AUTOMÁTICA(ACIONADO PELO PROCESSO BATCH OU JOB AGENDADO NO SERVIDOR*/
)
AS BEGIN

	DECLARE @DT_REFERENCIA SMALLDATETIME
	
	SET @DT_REFERENCIA = CONVERT(SMALLDATETIME, @DT_REF, 103)


	IF (SELECT TOP 1 IN_FINALIZADO FROM PROCESSAMENTO WHERE DT_REFERENCIA = (SELECT MAX(DT_REFERENCIA) FROM PROCESSAMENTO )) = 'N'
	BEGIN
		SELECT  'Existe outro processo em andamento.'
		RETURN
	END


	DECLARE @table TABLE
	(
		CD VARCHAR(15)
	)

	--se a tabela com os códigos de carteira estiver vazia, insere todas as carteias
	IF @LISTA_CD_CARTEIRA = ''
	BEGIN
		INSERT INTO @table (CD) 
		SELECT DISTINCT CD_CARTEIRA FROM WM_DB..CARTEIRA WHERE IN_ATIVO_INATIVO = 'A'
	END
	ELSE
	BEGIN
		INSERT INTO @table (CD) 
		SELECT DISTINCT STRVAL FROM WM_DB.[dbo].[Split](@LISTA_CD_CARTEIRA, ',')		
		
	END

	DECLARE @CD_PROCESSAMENTO_NEWID INT , @IN_PERIODO_PROCESSAMENTO CHAR(1), @ERR_MSG VARCHAR(MAX), @ERR_LINE INT, @ERR_PROC VARCHAR(256)
	DECLARE @CD_RES_SINTETICO_NEW_ID INT, @IN_ENQ_SINTETICO CHAR(1)

	SELECT @CD_PROCESSAMENTO_NEWID = ISNULL(MAX(CD_PROCESSAMENTO),0) +1 
	FROM PROCESSAMENTO




	--Se for o último dia do mes, o processamento será do tipo 'M'ensal. Senão, 'D'iário
	IF MONTH(@DT_REFERENCIA) != MONTH(WM_DB.dbo.fn_calcula_dia_util(@DT_REFERENCIA,1))
	BEGIN
		SET @IN_PERIODO_PROCESSAMENTO = 'M'
	END
	ELSE
	BEGIN
		SET @IN_PERIODO_PROCESSAMENTO = 'D'
	END


	/** inicio do processo **/
	INSERT INTO [Quality_Control].[dbo].[PROCESSAMENTO]
			   ([CD_PROCESSAMENTO]
			   ,[DT_REFERENCIA]
			   ,[IN_PERIODO_PROCESSAMENTO]
			   ,[IN_TIPO_PROCESSAMENTO]
			   ,[DT_EXECUCAO_INI]
			   ,[DT_EXECUCAO_FIM]
			   ,[CD_USUARIO_RESPONSAVEL]
			   ,[IN_FINALIZADO])

	VALUES
	(@CD_PROCESSAMENTO_NEWID,
	@DT_REFERENCIA,
	@IN_PERIODO_PROCESSAMENTO,
	@IN_TIPO_EXECUCAO,
	GETDATE(),
	NULL,
	@CD_USUARIO,
	'N')



	/** inicio do loop  **/
	DECLARE @CD_CARTEIRA VARCHAR(30)

	-- Cursor para percorrer os nomes dos objetos
	DECLARE cursor_carteiras CURSOR FOR
		SELECT
			  CD
		FROM
			@table


	-- Abrindo Cursor para leitura
	OPEN cursor_carteiras

	-- Lendo a próxima linha
	FETCH NEXT FROM cursor_carteiras INTO @CD_CARTEIRA

	--Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

	/**** INICIO DO BLOCO DE PROCESSAMENTO ***/
		BEGIN TRANSACTION;
		PRINT 'TRANSACTION OPENED';

		BEGIN TRY		

			/*PARTE 1 -  CONCILIAÇÃO */
			--PARA CADA ATIVO/CARTEIRA

			-- 1.1 insere carteiras provenientes do resultado da conciliação
			INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO]
					   ([CD_RESULTADO]
					   ,[DT_RESULTADO]
					   ,[CD_CARTEIRA]
					   ,[CD_TIPO_FILTRO]
					   ,[CD_SUBTIPO_FILTRO]
					   ,[CD_ATIVO]
					   ,[CD_TIPO_ATIVO]
					   ,[IN_ENQUADRADO]
					   ,[VL_PONTA_A]
					   ,[VL_PONTA_B]
					   ,[DT_ABERTURA_CART]
					   ,[DT_ENCERRAMENTO_CART]
					   ,[DT_ALTERACAO]
					   ,[CD_USUARIO_ALTERACAO]
					   ,[CD_PROCESSAMENTO])


			SELECT 
			ISNULL((SELECT MAX(CD_RESULTADO) FROM RESULTADO_ENQUADRAMENTO),0) +  ROW_NUMBER() 
			OVER (ORDER BY DT_REFERENCIA) AS CD_RESULTADO,
			DT_REFERENCIA, CD_CARTEIRA, 
			tf.CD_TIPO_FILTRO_PAI AS CD_TIPO_FILTRO, tfa.CD_TIPO_FILTRO AS CD_SUBTIPO_FILTRO,
			CD_ATIVO, c.CD_TIPO_ATIVO,
			c.IN_CONCILIADO AS IN_ENQUADRADO, 
			VL_REGISTRO_A AS VL_PONTA_A,
			VL_REGISTRO_B AS VL_PONTA_B, NULL AS DT_ABERTURA_CART,
			NULL AS DT_ENCERRAMENTO_CART,
			GETDATE() AS DT_ALTERACAO,
			@CD_USUARIO AS CD_USUARIO_ALTERACAO, @CD_PROCESSAMENTO_NEWID

			FROM WM_DB..CONCILIACAO c
			LEFT JOIN Quality_Control..TIPO_FILTRO_ATIVO tfa 
			ON c.CD_TIPO_ATIVO = tfa.CD_TIPO_ATIVO
			LEFT JOIN Quality_Control..TIPO_FILTRO tf ON tfa.CD_TIPO_FILTRO = tf.CD_TIPO_FILTRO
			WHERE DT_REFERENCIA = @DT_REFERENCIA
			AND c.CD_CARTEIRA = @CD_CARTEIRA
			
			--INSERE LOG DE insere carteiras provenientes do resultado da conciliação
			INSERT INTO LOG_PROCESSAMENTO 
			(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO)
			SELECT   
				@DT_REFERENCIA, @CD_CARTEIRA, tf.CD_TIPO_FILTRO_PAI,tfa.CD_TIPO_FILTRO,
				CASE c.IN_CONCILIADO WHEN 'S' THEN 'OK' ELSE 'Não conciliado no DASH' END, 
				@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID		
			FROM WM_DB..CONCILIACAO c
			LEFT JOIN Quality_Control..TIPO_FILTRO_ATIVO tfa 
			ON c.CD_TIPO_ATIVO = tfa.CD_TIPO_ATIVO
			LEFT JOIN Quality_Control..TIPO_FILTRO tf ON tfa.CD_TIPO_FILTRO = tf.CD_TIPO_FILTRO
			WHERE DT_REFERENCIA = @DT_REFERENCIA
			AND c.CD_CARTEIRA = @CD_CARTEIRA

			-- 1.2 insere carteiras cadastradas que não vieram na conciliação (***)
			--provisoriamente removido por nao estar na especificação
			--INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO]
			--		   ([CD_RESULTADO]
			--		   ,[DT_RESULTADO]
			--		   ,[CD_CARTEIRA]
			--		   ,[CD_TIPO_FILTRO]
			--		   ,[CD_SUBTIPO_FILTRO]
			--		   ,[CD_ATIVO]
			--		   ,[CD_TIPO_ATIVO]
			--		   ,[IN_ENQUADRADO]
			--		   ,[VL_PONTA_A]
			--		   ,[VL_PONTA_B]
			--		   ,[DT_ABERTURA_CART]
			--		   ,[DT_ENCERRAMENTO_CART]
			--		   ,[DT_ALTERACAO]
			--		   ,[CD_USUARIO_ALTERACAO]
			--		   ,[CD_PROCESSAMENTO])

			--SELECT 
			--ISNULL((SELECT MAX(CD_RESULTADO) FROM RESULTADO_ENQUADRAMENTO),0) +  ROW_NUMBER() 
			--OVER (ORDER BY CD_CARTEIRA) AS CD_RESULTADO,
			--@DT_REFERENCIA AS DT_REFERENCIA, CD_CARTEIRA, 
			--CD_TIPO_FILTRO_PAI AS CD_TIPO_FILTRO, CD_TIPO_FILTRO AS CD_SUBTIPO_FILTRO,
			--NULL AS CD_ATIVO, NULL AS CD_TIPO_ATIVO,
			--'N' AS IN_ENQUADRADO, 
			--NULL AS VL_PONTA_A,
			--NULL AS VL_PONTA_B, NULL AS DT_ABERTURA_CART,
			--NULL AS DT_ENCERRAMENTO_CART,
			--GETDATE() AS DT_ALTERACAO,
			--@CD_USUARIO AS CD_USUARIO_ALTERACAO, @CD_PROCESSAMENTO_NEWID

			--FROM WM_DB..CARTEIRA c
			--JOIN TIPO_FILTRO f ON CD_TIPO_FILTRO_PAI = 1 AND CD_TIPO_FILTRO != CD_TIPO_FILTRO_PAI
			--WHERE c.IN_ATIVO_INATIVO = 'A'
			--AND c.CD_CARTEIRA = @CD_CARTEIRA
			--AND NOT EXISTS 
			--(SELECT CD_CARTEIRA FROM WM_DB..CONCILIACAO WHERE CD_CARTEIRA = c.CD_CARTEIRA)

			--INSERE LOG DE insere carteiras cadastradas que não vieram na conciliação (***)
			--INSERT INTO LOG_PROCESSAMENTO 
			--(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO)
			--SELECT   
			--	@DT_REFERENCIA, @CD_CARTEIRA, CD_TIPO_FILTRO_PAI,CD_TIPO_FILTRO,
			--	'Não há registro correspondente no repositório de conciliações do DASH', 
			--	@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID		
			--FROM WM_DB..CARTEIRA c
			--JOIN TIPO_FILTRO f ON CD_TIPO_FILTRO_PAI = 1 AND CD_TIPO_FILTRO != CD_TIPO_FILTRO_PAI
			--WHERE c.IN_ATIVO_INATIVO = 'A'
			--AND c.CD_CARTEIRA = @CD_CARTEIRA
			--AND NOT EXISTS 
			--(SELECT CD_CARTEIRA FROM WM_DB..CONCILIACAO WHERE CD_CARTEIRA = c.CD_CARTEIRA)

			/* PARTE 2 - RENTABILIDADE */
			--PARA CADA CARTEIRA
			-- 2.1 RENTABILIDADE DA COTA

			
			/* Rentabilidade Cota */
			INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO]
					   ([CD_RESULTADO]
					   ,[DT_RESULTADO]
					   ,[CD_CARTEIRA]
					   ,[CD_TIPO_FILTRO]
					   ,[CD_SUBTIPO_FILTRO]
					   ,[CD_ATIVO]
					   ,[CD_TIPO_ATIVO]
					   ,[IN_ENQUADRADO]
					   ,[VL_PONTA_A]
					   ,[VL_PONTA_B]
					   ,[DT_ABERTURA_CART]
					   ,[DT_ENCERRAMENTO_CART]
					   ,[DT_ALTERACAO]
					   ,[CD_USUARIO_ALTERACAO]
					   ,[CD_PROCESSAMENTO])



			SELECT  
				ISNULL((SELECT MAX(CD_RESULTADO) FROM RESULTADO_ENQUADRAMENTO),0) +  ROW_NUMBER() 
				OVER (ORDER BY a.CD_CARTEIRA) AS CD_RESULTADO,
				@DT_REFERENCIA AS DT_REFERENCIA, a.CD_CARTEIRA, 
				b.CD_TIPO_FILTRO, b.CD_SUBTIPO_FILTRO,
				NULL AS CD_ATIVO, NULL AS CD_TIPO_ATIVO,
				CASE 
					WHEN abs((case when c.VL_COTA < d.VL_COTA then c.VL_COTA / d.VL_COTA else d.VL_COTA / c.VL_COTA end *100) -100) < ISNULL(le.VL_LIMITE_MIN, b.VL_LIMITE_MIN) THEN 'N'
					WHEN abs((case when c.VL_COTA < d.VL_COTA then c.VL_COTA / d.VL_COTA else d.VL_COTA / c.VL_COTA end *100) -100) > ISNULL(le.VL_LIMITE_MAX, b.VL_LIMITE_MAX) THEN 'N'
					ELSE 'S' 
				END AS IN_ENQUADRADO, 
				c.VL_COTA AS VL_PONTA_A,
				d.VL_COTA AS VL_PONTA_B, NULL AS DT_ABERTURA_CART,
				NULL AS DT_ENCERRAMENTO_CART,
				GETDATE() AS DT_ALTERACAO,
				@CD_USUARIO AS CD_USUARIO_ALTERACAO, @CD_PROCESSAMENTO_NEWID
			FROM 
			WM_DB..CARTEIRA a
				
				Inner Join LIMITES_PERFIL_RISCO b
					on ltrim(rtrim(b.cd_perfil_risco)) = ltrim(rtrim(a.cd_perfil_risco)) AND b.IN_EXCECAO = 'N'
				Inner Join wm_db..CARTEIRA_COTA c
					on c.CD_CARTEIRA = a.cd_carteira
				   and c.DT_COTA = @DT_REFERENCIA
				Inner Join wm_db..CARTEIRA_COTA d
					on d.CD_CARTEIRA = a.cd_carteira
				   and d.DT_COTA = 
					CASE @IN_PERIODO_PROCESSAMENTO 
					WHEN 'D' THEN
						(SELECT MAX(DT_COTA) From WM_DB..CARTEIRA_COTA WHERE CD_CARTEIRA = a.CD_CARTEIRA AND DT_COTA < @DT_REFERENCIA)
					WHEN 'M' THEN
						(SELECT MAX(DT_COTA) FROM WM_DB..CARTEIRA_COTA 
						WHERE CD_CARTEIRA = a.CD_CARTEIRA AND DT_COTA < 
						CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01'))
					ELSE NULL END
				LEFT JOIN LIMITES_PERFIL_RISCO le
					ON LTRIM(RTRIM(le.CD_PERFIL_RISCO)) = LTRIM(RTRIM(a.CD_PERFIL_RISCO)) AND 
					le.CD_TIPO_FILTRO = b.CD_TIPO_FILTRO AND le.CD_SUBTIPO_FILTRO = b.CD_SUBTIPO_FILTRO AND
					le.IN_EXCECAO = 'S' AND le.DT_INI_VIGENCIA = @DT_REFERENCIA
					
			where 
			  b.CD_TIPO_FILTRO = 2
			  and b.CD_SUBTIPO_FILTRO IN (11, 12)
			  AND a.CD_CARTEIRA = @CD_CARTEIRA
			  and b.IN_DIARIO_MENSAL = @IN_PERIODO_PROCESSAMENTO
			AND ((b.IN_DIARIO_MENSAL = 'M' and b.dt_ini_vigencia <= CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01')
			  and b.DT_FIM_VIGENCIA >= CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01')) 
				OR
			(b.IN_DIARIO_MENSAL = 'D'
			and  b.dt_ini_vigencia <= @DT_REFERENCIA
			  and IsNUll(b.DT_FIM_VIGENCIA, dateadd(day, 1, @DT_REFERENCIA)) >= @DT_REFERENCIA))
			  
			  
			--INSERE LOG DE Rentabilidade Cota
			INSERT INTO LOG_PROCESSAMENTO 
			(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO)
			SELECT   
				@DT_REFERENCIA, @CD_CARTEIRA, b.CD_TIPO_FILTRO, b.CD_SUBTIPO_FILTRO,
				CASE 
					WHEN abs((case when c.VL_COTA < d.VL_COTA then c.VL_COTA / d.VL_COTA else d.VL_COTA / c.VL_COTA end *100) -100) < ISNULL(le.VL_LIMITE_MIN, b.VL_LIMITE_MIN) THEN 
					'Rentabilidade da cota está abaixo do limite mínimo estabelecido'
					WHEN abs((case when c.VL_COTA < d.VL_COTA then c.VL_COTA / d.VL_COTA else d.VL_COTA / c.VL_COTA end *100) -100) > ISNULL(le.VL_LIMITE_MAX, b.VL_LIMITE_MAX) THEN 
					'Rentabilidade da cota está acima do limite máximo estabelecido'
					ELSE 'OK' 
				END, 
				@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID		
			FROM 
			WM_DB..CARTEIRA a
				Inner Join LIMITES_PERFIL_RISCO b
					on ltrim(rtrim(b.cd_perfil_risco)) = ltrim(rtrim(a.cd_perfil_risco)) AND b.IN_EXCECAO = 'N'
				Inner Join wm_db..CARTEIRA_COTA c
					on c.CD_CARTEIRA = a.cd_carteira
				   and c.DT_COTA = @DT_REFERENCIA
				Inner Join wm_db..CARTEIRA_COTA d
					on d.CD_CARTEIRA = a.cd_carteira
				   and d.DT_COTA = 
					CASE @IN_PERIODO_PROCESSAMENTO 
					WHEN 'D' THEN
						(SELECT MAX(DT_COTA) From WM_DB..CARTEIRA_COTA WHERE CD_CARTEIRA = a.CD_CARTEIRA AND DT_COTA < @DT_REFERENCIA)
					WHEN 'M' THEN
						(SELECT MAX(DT_COTA) FROM WM_DB..CARTEIRA_COTA 
						WHERE CD_CARTEIRA = a.CD_CARTEIRA AND DT_COTA < 
						CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01'))
					ELSE NULL END
				LEFT JOIN LIMITES_PERFIL_RISCO le
					ON LTRIM(RTRIM(le.CD_PERFIL_RISCO)) = LTRIM(RTRIM(a.CD_PERFIL_RISCO)) AND 
					le.CD_TIPO_FILTRO = b.CD_TIPO_FILTRO AND le.CD_SUBTIPO_FILTRO = b.CD_SUBTIPO_FILTRO AND
					le.IN_EXCECAO = 'S' AND le.DT_INI_VIGENCIA = @DT_REFERENCIA
					
			where 
			  b.CD_TIPO_FILTRO = 2
			  and b.CD_SUBTIPO_FILTRO IN (11, 12)
			  AND a.CD_CARTEIRA = @CD_CARTEIRA
			  and b.IN_DIARIO_MENSAL = @IN_PERIODO_PROCESSAMENTO
			AND ((b.IN_DIARIO_MENSAL = 'M' and b.dt_ini_vigencia <= CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01')
			  and b.DT_FIM_VIGENCIA >= CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01')) 
				OR
			(b.IN_DIARIO_MENSAL = 'D'
			and  b.dt_ini_vigencia <= @DT_REFERENCIA
			  and IsNUll(b.DT_FIM_VIGENCIA, dateadd(day, 1, @DT_REFERENCIA)) >= @DT_REFERENCIA))
			  

			-- 2.2 RENTABILIDADE DO PATRIMONIO
			INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO]
					   ([CD_RESULTADO]
					   ,[DT_RESULTADO]
					   ,[CD_CARTEIRA]
					   ,[CD_TIPO_FILTRO]
					   ,[CD_SUBTIPO_FILTRO]
					   ,[CD_ATIVO]
					   ,[CD_TIPO_ATIVO]
					   ,[IN_ENQUADRADO]
					   ,[VL_PONTA_A]
					   ,[VL_PONTA_B]
					   ,[DT_ABERTURA_CART]
					   ,[DT_ENCERRAMENTO_CART]
					   ,[DT_ALTERACAO]
					   ,[CD_USUARIO_ALTERACAO]
					   ,[CD_PROCESSAMENTO])

			SELECT  
				ISNULL((SELECT MAX(CD_RESULTADO) FROM RESULTADO_ENQUADRAMENTO),0) +  ROW_NUMBER() 
				OVER (ORDER BY a.CD_CARTEIRA) AS CD_RESULTADO,
				@DT_REFERENCIA AS DT_REFERENCIA, a.CD_CARTEIRA, 
				b.CD_TIPO_FILTRO, b.CD_SUBTIPO_FILTRO,
				NULL AS CD_ATIVO, NULL AS CD_TIPO_ATIVO,
				CASE 
					WHEN abs((case when c.VL_PATRIMONIO < d.VL_PATRIMONIO then c.VL_PATRIMONIO / d.VL_PATRIMONIO else d.VL_PATRIMONIO / c.VL_PATRIMONIO end *100) -100) < ISNULL(le.VL_LIMITE_MIN, b.VL_LIMITE_MIN) THEN 'N'
					WHEN abs((case when c.VL_PATRIMONIO < d.VL_PATRIMONIO then c.VL_PATRIMONIO / d.VL_PATRIMONIO else d.VL_PATRIMONIO / c.VL_PATRIMONIO end *100) -100) > ISNULL(le.VL_LIMITE_MAX, b.VL_LIMITE_MAX) THEN 'N'
					ELSE 'S' 
				END AS IN_ENQUADRADO,
				c.VL_PATRIMONIO AS VL_PONTA_A,
				d.VL_PATRIMONIO AS VL_PONTA_B, NULL AS DT_ABERTURA_CART,
				NULL AS DT_ENCERRAMENTO_CART,
				GETDATE() AS DT_ALTERACAO,
				@CD_USUARIO AS CD_USUARIO_ALTERACAO, @CD_PROCESSAMENTO_NEWID
			FROM 
			WM_DB..CARTEIRA a
				--INNER JOIN @table t ON a.CD_CARTEIRA = t.CD

				Inner Join LIMITES_PERFIL_RISCO b
					on ltrim(rtrim(b.cd_perfil_risco)) = ltrim(rtrim(a.cd_perfil_risco)) AND b.IN_EXCECAO = 'N'
				Inner Join wm_db..CARTEIRA_COTA c
					on c.CD_CARTEIRA = a.cd_carteira
				   and c.DT_COTA = @DT_REFERENCIA
				Inner Join wm_db..CARTEIRA_COTA d
					on d.CD_CARTEIRA = a.cd_carteira
				   and d.DT_COTA = 
					CASE @IN_PERIODO_PROCESSAMENTO 
					WHEN 'D' THEN
						(SELECT MAX(DT_COTA) From WM_DB..CARTEIRA_COTA WHERE CD_CARTEIRA = a.CD_CARTEIRA AND DT_COTA < @DT_REFERENCIA)
					WHEN 'M' THEN
						(SELECT MAX(DT_COTA) FROM WM_DB..CARTEIRA_COTA 
						WHERE CD_CARTEIRA = a.CD_CARTEIRA AND DT_COTA < 
						CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01'))
					ELSE NULL END	   
				LEFT JOIN LIMITES_PERFIL_RISCO le
					ON LTRIM(RTRIM(le.CD_PERFIL_RISCO)) = LTRIM(RTRIM(a.CD_PERFIL_RISCO)) AND 
					le.CD_TIPO_FILTRO = b.CD_TIPO_FILTRO AND le.CD_SUBTIPO_FILTRO = b.CD_SUBTIPO_FILTRO AND
					le.IN_EXCECAO = 'S' AND le.DT_INI_VIGENCIA = @DT_REFERENCIA
				   
			where 
			  b.CD_TIPO_FILTRO = 2
			  and b.CD_SUBTIPO_FILTRO IN (9, 10)
			  AND a.CD_CARTEIRA = @CD_CARTEIRA
			  and b.IN_DIARIO_MENSAL = @IN_PERIODO_PROCESSAMENTO
			AND ((b.IN_DIARIO_MENSAL = 'M' and b.dt_ini_vigencia <= CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01')
			  and b.DT_FIM_VIGENCIA >= CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01')) 
				OR
			(b.IN_DIARIO_MENSAL = 'D'
			and  b.dt_ini_vigencia <= @DT_REFERENCIA
			  and IsNUll(b.DT_FIM_VIGENCIA, dateadd(day, 1, @DT_REFERENCIA)) >= @DT_REFERENCIA))
			  
			  
			  
			--INSERE LOG DE RENTABILIDADE DO PATRIMONIO
			INSERT INTO LOG_PROCESSAMENTO 
			(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO)
			SELECT   
				@DT_REFERENCIA, @CD_CARTEIRA, b.CD_TIPO_FILTRO, b.CD_SUBTIPO_FILTRO,
				CASE 
					WHEN abs((case when c.VL_PATRIMONIO < d.VL_PATRIMONIO then c.VL_PATRIMONIO / d.VL_PATRIMONIO else d.VL_PATRIMONIO / c.VL_PATRIMONIO end *100) -100) < ISNULL(le.VL_LIMITE_MIN, b.VL_LIMITE_MIN) THEN 
					'Rentabilidade do patrimônio está abaixo do limite mínimo estabelecido'
					WHEN abs((case when c.VL_PATRIMONIO < d.VL_PATRIMONIO then c.VL_PATRIMONIO / d.VL_PATRIMONIO else d.VL_PATRIMONIO / c.VL_PATRIMONIO end *100) -100) > ISNULL(le.VL_LIMITE_MAX, b.VL_LIMITE_MAX) THEN 
					'Rentabilidade do patrimônio está acima do limite máximo estabelecido'
					ELSE 'OK' 
				END, 
				@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID		
			FROM 
			WM_DB..CARTEIRA a
				--INNER JOIN @table t ON a.CD_CARTEIRA = t.CD

				Inner Join LIMITES_PERFIL_RISCO b
					on ltrim(rtrim(b.cd_perfil_risco)) = ltrim(rtrim(a.cd_perfil_risco)) AND b.IN_EXCECAO = 'N'
				Inner Join wm_db..CARTEIRA_COTA c
					on c.CD_CARTEIRA = a.cd_carteira
				   and c.DT_COTA = @DT_REFERENCIA
				Inner Join wm_db..CARTEIRA_COTA d
					on d.CD_CARTEIRA = a.cd_carteira
				   and d.DT_COTA = 
					CASE @IN_PERIODO_PROCESSAMENTO 
					WHEN 'D' THEN
						(SELECT MAX(DT_COTA) From WM_DB..CARTEIRA_COTA WHERE CD_CARTEIRA = a.CD_CARTEIRA AND DT_COTA < @DT_REFERENCIA)
					WHEN 'M' THEN
						(SELECT MAX(DT_COTA) FROM WM_DB..CARTEIRA_COTA 
						WHERE CD_CARTEIRA = a.CD_CARTEIRA AND DT_COTA < 
						CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01'))
					ELSE NULL END	   

				LEFT JOIN LIMITES_PERFIL_RISCO le
					ON LTRIM(RTRIM(le.CD_PERFIL_RISCO)) = LTRIM(RTRIM(a.CD_PERFIL_RISCO)) AND 
					le.CD_TIPO_FILTRO = b.CD_TIPO_FILTRO AND le.CD_SUBTIPO_FILTRO = b.CD_SUBTIPO_FILTRO AND
					le.IN_EXCECAO = 'S' AND le.DT_INI_VIGENCIA = @DT_REFERENCIA

			where 
			  b.CD_TIPO_FILTRO = 2
			  and b.CD_SUBTIPO_FILTRO IN (9, 10)
			  AND a.CD_CARTEIRA = @CD_CARTEIRA
			  and b.IN_DIARIO_MENSAL = @IN_PERIODO_PROCESSAMENTO
			AND ((b.IN_DIARIO_MENSAL = 'M' and b.dt_ini_vigencia <= CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01')
			  and b.DT_FIM_VIGENCIA >= CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01')) 
				OR
			(b.IN_DIARIO_MENSAL = 'D'
			and  b.dt_ini_vigencia <= @DT_REFERENCIA
			  and IsNUll(b.DT_FIM_VIGENCIA, dateadd(day, 1, @DT_REFERENCIA)) >= @DT_REFERENCIA))
			  

			/*  PARTE 3 - ACURACIDADE */
			--PARA CADA CARTEIRA

			--3.1 - CARTEIRA NOVA
			INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO]
					   ([CD_RESULTADO]
					   ,[DT_RESULTADO]
					   ,[CD_CARTEIRA]
					   ,[CD_TIPO_FILTRO]
					   ,[CD_SUBTIPO_FILTRO]
					   ,[CD_ATIVO]
					   ,[CD_TIPO_ATIVO]
					   ,[IN_ENQUADRADO]
					   ,[VL_PONTA_A]
					   ,[VL_PONTA_B]
					   ,[DT_ABERTURA_CART]
					   ,[DT_ENCERRAMENTO_CART]
					   ,[DT_ALTERACAO]
					   ,[CD_USUARIO_ALTERACAO]
					   ,[CD_PROCESSAMENTO])

			SELECT 
				ISNULL((SELECT MAX(CD_RESULTADO) FROM RESULTADO_ENQUADRAMENTO),0) +  ROW_NUMBER() 
				OVER (ORDER BY CD_CARTEIRA) AS CD_RESULTADO,
				@DT_REFERENCIA AS DT_REFERENCIA, CD_CARTEIRA, 
				3 AS CD_TIPO_FILTRO, 
				13 AS CD_SUBTIPO_FILTRO,
				NULL AS CD_ATIVO, NULL AS CD_TIPO_ATIVO,
				CASE @IN_PERIODO_PROCESSAMENTO
				WHEN 'D' THEN
					CASE WHEN c.DT_ABERTURA >= @DT_REFERENCIA THEN 'N' ELSE 'S' END 
				WHEN 'M' THEN
					CASE WHEN c.DT_ABERTURA BETWEEN  CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01') AND @DT_REFERENCIA 
					THEN 'N' ELSE 'S' END 
				ELSE NULL END
				AS IN_ENQUADRADO, 

				NULL AS VL_PONTA_A,
				NULL AS VL_PONTA_B, c.DT_ABERTURA AS DT_ABERTURA_CART,
				c.DT_ENCERRAMENTO AS DT_ENCERRAMENTO_CART,
				GETDATE() AS DT_ALTERACAO,
				@CD_USUARIO AS CD_USUARIO_ALTERACAO, @CD_PROCESSAMENTO_NEWID
			FROM WM_DB..CARTEIRA c
			--INNER JOIN @table t ON c.CD_CARTEIRA = t.CD
			WHERE IN_ATIVO_INATIVO = 'A'
			AND c.CD_CARTEIRA = @CD_CARTEIRA
			
			--INSERE LOG DE ACURACIDADE CARTEIRA NOVA
			INSERT INTO LOG_PROCESSAMENTO 
			(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO)
			SELECT   
				@DT_REFERENCIA, @CD_CARTEIRA, 3,13,
				CASE @IN_PERIODO_PROCESSAMENTO
				WHEN 'D' THEN
					CASE WHEN c.DT_ABERTURA >= @DT_REFERENCIA THEN 
					'Data de abertura da carteira é a data referência do processamento' ELSE 'OK' END 
				WHEN 'M' THEN
					CASE WHEN c.DT_ABERTURA BETWEEN  CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01') AND @DT_REFERENCIA 
					THEN 'Data de abertura da carteira no mesmo mês da data referência do processamento' ELSE 'OK' END 
				ELSE NULL END
				AS IN_ENQUADRADO, 
				@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID		
			FROM WM_DB..CARTEIRA c
			WHERE IN_ATIVO_INATIVO = 'A'
			AND c.CD_CARTEIRA = @CD_CARTEIRA

			--3.1 - ENCERRAMENTO DE CARTEIRA
			INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO]
					   ([CD_RESULTADO]
					   ,[DT_RESULTADO]
					   ,[CD_CARTEIRA]
					   ,[CD_TIPO_FILTRO]
					   ,[CD_SUBTIPO_FILTRO]
					   ,[CD_ATIVO]
					   ,[CD_TIPO_ATIVO]
					   ,[IN_ENQUADRADO]
					   ,[VL_PONTA_A]
					   ,[VL_PONTA_B]
					   ,[DT_ABERTURA_CART]
					   ,[DT_ENCERRAMENTO_CART]
					   ,[DT_ALTERACAO]
					   ,[CD_USUARIO_ALTERACAO]
					   ,[CD_PROCESSAMENTO])

			SELECT 
				ISNULL((SELECT MAX(CD_RESULTADO) FROM RESULTADO_ENQUADRAMENTO),0) +  ROW_NUMBER() 
				OVER (ORDER BY CD_CARTEIRA) AS CD_RESULTADO,
				@DT_REFERENCIA AS DT_REFERENCIA, CD_CARTEIRA, 
				3 AS CD_TIPO_FILTRO, 
				14 AS CD_SUBTIPO_FILTRO,
				NULL AS CD_ATIVO, NULL AS CD_TIPO_ATIVO,
				CASE @IN_PERIODO_PROCESSAMENTO
				WHEN 'D' THEN
					CASE WHEN c.DT_ENCERRAMENTO <= @DT_REFERENCIA THEN 'N' ELSE 'S' END 
				WHEN 'M' THEN
					CASE WHEN c.DT_ENCERRAMENTO BETWEEN  CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01') AND @DT_REFERENCIA 
					THEN 'N' ELSE 'S' END 
				ELSE NULL END
				AS IN_ENQUADRADO, 
				NULL AS VL_PONTA_A,
				NULL AS VL_PONTA_B, c.DT_ABERTURA AS DT_ABERTURA_CART,
				c.DT_ENCERRAMENTO AS DT_ENCERRAMENTO_CART,
				GETDATE() AS DT_ALTERACAO,
				@CD_USUARIO AS CD_USUARIO_ALTERACAO, @CD_PROCESSAMENTO_NEWID
			FROM WM_DB..CARTEIRA c
			--INNER JOIN @table t ON c.CD_CARTEIRA = t.CD
			WHERE IN_ATIVO_INATIVO = 'A'
			AND c.CD_CARTEIRA = @CD_CARTEIRA
			
			--INSERE LOG DE ACURACIDADE ENCERRAMENTO DE CARTEIRA
			INSERT INTO LOG_PROCESSAMENTO 
			(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO)
			SELECT   
				@DT_REFERENCIA, @CD_CARTEIRA, 3,14,
				CASE @IN_PERIODO_PROCESSAMENTO
				WHEN 'D' THEN
					CASE WHEN c.DT_ENCERRAMENTO <= @DT_REFERENCIA THEN 
					'Data do processamento é a data de encerramento da carteira' ELSE 'OK' END 
				WHEN 'M' THEN
					CASE WHEN c.DT_ENCERRAMENTO BETWEEN  CONVERT(SMALLDATETIME,CONVERT(VARCHAR,YEAR(@DT_REFERENCIA)) + '-' + CONVERT(VARCHAR,MONTH(@DT_REFERENCIA)) + '-01') AND @DT_REFERENCIA 
					THEN 'Data do processamento está no mês de encerramento da carteira' ELSE 'OK' END 
				ELSE NULL END
				AS IN_ENQUADRADO, 
				@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID		
			FROM WM_DB..CARTEIRA c
			WHERE IN_ATIVO_INATIVO = 'A'
			AND c.CD_CARTEIRA = @CD_CARTEIRA

			--3.3 - Bloqueio de ativos
			INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO]
					   ([CD_RESULTADO]
					   ,[DT_RESULTADO]
					   ,[CD_CARTEIRA]
					   ,[CD_TIPO_FILTRO]
					   ,[CD_SUBTIPO_FILTRO]
					   ,[CD_ATIVO]
					   ,[CD_TIPO_ATIVO]
					   ,[IN_ENQUADRADO]
					   ,[VL_PONTA_A]
					   ,[VL_PONTA_B]
					   ,[DT_ABERTURA_CART]
					   ,[DT_ENCERRAMENTO_CART]
					   ,[DT_ALTERACAO]
					   ,[CD_USUARIO_ALTERACAO]
					   ,[CD_PROCESSAMENTO])

			SELECT 
			ISNULL((SELECT MAX(CD_RESULTADO) FROM RESULTADO_ENQUADRAMENTO),0) +  ROW_NUMBER() 
			OVER (ORDER BY c.CD_CARTEIRA) AS CD_RESULTADO,
			@DT_REFERENCIA AS DT_REFERENCIA, c.CD_CARTEIRA, 
			3 AS CD_TIPO_FILTRO, 16 AS CD_SUBTIPO_FILTRO,
			p.CD_ATIVO, p.CD_TIPO_ATIVO,
			'N' AS IN_ENQUADRADO, 
			NULL AS VL_PONTA_A,
			NULL AS VL_PONTA_B, NULL AS DT_ABERTURA_CART,
			NULL AS DT_ENCERRAMENTO_CART,
			GETDATE() AS DT_ALTERACAO,
			@CD_USUARIO AS CD_USUARIO_ALTERACAO, @CD_PROCESSAMENTO_NEWID

			FROM WM_DB..CARTEIRA c
			INNER JOIN 
				(SELECT DISTINCT CD_CARTEIRA, CD_ATIVO, CD_TIPO_ATIVO FROM WM_DB..POSICAO WHERE DT_POSICAO = @DT_REFERENCIA) p 
			ON c.CD_CARTEIRA = p.CD_CARTEIRA 			
			
			WHERE c.IN_ATIVO_INATIVO = 'A'
			AND c.CD_CARTEIRA = @CD_CARTEIRA
			AND @IN_PERIODO_PROCESSAMENTO = 'M' 
			
			--INSERE LOG DE BLOQUEIO DE ATIVOS
			INSERT INTO LOG_PROCESSAMENTO 
			(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO)
			SELECT   
				@DT_REFERENCIA, @CD_CARTEIRA, 3 AS CD_TIPO_FILTRO_PAI, 16 AS CD_TIPO_FILTRO,
				'Bloqueio de ativo realizado durante o processamento mensal.' + p.CD_ATIVO + ' ' + p.CD_TIPO_ATIVO, 
				@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID		
			FROM WM_DB..CARTEIRA c
			INNER JOIN 
				(SELECT DISTINCT CD_CARTEIRA, CD_ATIVO, CD_TIPO_ATIVO FROM WM_DB..POSICAO WHERE DT_POSICAO = @DT_REFERENCIA) p 
			ON c.CD_CARTEIRA = p.CD_CARTEIRA 			
			
			WHERE c.IN_ATIVO_INATIVO = 'A'
			AND c.CD_CARTEIRA = @CD_CARTEIRA
			AND @IN_PERIODO_PROCESSAMENTO = 'M' 
			
			--3.4 - CARTEIRA zerada
			INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO]
					   ([CD_RESULTADO]
					   ,[DT_RESULTADO]
					   ,[CD_CARTEIRA]
					   ,[CD_TIPO_FILTRO]
					   ,[CD_SUBTIPO_FILTRO]
					   ,[CD_ATIVO]
					   ,[CD_TIPO_ATIVO]
					   ,[IN_ENQUADRADO]
					   ,[VL_PONTA_A]
					   ,[VL_PONTA_B]
					   ,[DT_ABERTURA_CART]
					   ,[DT_ENCERRAMENTO_CART]
					   ,[DT_ALTERACAO]
					   ,[CD_USUARIO_ALTERACAO]
					   ,[CD_PROCESSAMENTO])

			SELECT 
				ISNULL((SELECT MAX(CD_RESULTADO) FROM RESULTADO_ENQUADRAMENTO),0) +  ROW_NUMBER() 
				OVER (ORDER BY c.CD_CARTEIRA) AS CD_RESULTADO,
				@DT_REFERENCIA AS DT_REFERENCIA, c.CD_CARTEIRA, 
				3 AS CD_TIPO_FILTRO, 
				15 AS CD_SUBTIPO_FILTRO,
				NULL AS CD_ATIVO, NULL AS CD_TIPO_ATIVO,
				CASE WHEN ISNULL(cc.VL_PATRIMONIO, 0) <=0 THEN 'N' ELSE 'S' END AS IN_ENQUADRADO, 
				NULL AS VL_PONTA_A,
				NULL AS VL_PONTA_B, c.DT_ABERTURA AS DT_ABERTURA_CART,
				c.DT_ENCERRAMENTO AS DT_ENCERRAMENTO_CART,
				GETDATE() AS DT_ALTERACAO,
				@CD_USUARIO AS CD_USUARIO_ALTERACAO, @CD_PROCESSAMENTO_NEWID 
			FROM WM_DB..CARTEIRA c
				--INNER JOIN @table t ON c.CD_CARTEIRA = t.CD
				LEFT JOIN WM_DB..CARTEIRA_COTA cc ON c.CD_CARTEIRA = cc.CD_CARTEIRA
				AND cc.DT_COTA = @DT_REFERENCIA
			WHERE c.IN_ATIVO_INATIVO = 'A'
			AND c.CD_CARTEIRA = @CD_CARTEIRA
			
			--INSERE LOG DE ACURACIDADE CARTEIRA zerada
			INSERT INTO LOG_PROCESSAMENTO 
			(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO)
			SELECT   
				@DT_REFERENCIA, @CD_CARTEIRA, 3,15,
				CASE WHEN ISNULL(cc.VL_PATRIMONIO, 0) <=0 THEN 'Patrimônio zerado' ELSE 'OK' END
				AS IN_ENQUADRADO, 
				@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID		
			FROM WM_DB..CARTEIRA c
				LEFT JOIN WM_DB..CARTEIRA_COTA cc ON c.CD_CARTEIRA = cc.CD_CARTEIRA
				AND cc.DT_COTA = @DT_REFERENCIA
			WHERE c.IN_ATIVO_INATIVO = 'A'
			AND c.CD_CARTEIRA = @CD_CARTEIRA

			/****** FIM DO PROCESSAMENTO ********/
			
			SELECT @CD_RES_SINTETICO_NEW_ID = ISNULL(MAX(CD_RESULTADO_SINTETICO),0) + 1 FROM Quality_Control..RESULTADO_ENQUADRAMENTO_SINTETICO
			
			SELECT @IN_ENQ_SINTETICO = (CASE WHEN EXISTS (SELECT 1 FROM Quality_Control..RESULTADO_ENQUADRAMENTO 
							WHERE CD_PROCESSAMENTO = @CD_PROCESSAMENTO_NEWID AND CD_CARTEIRA = @CD_CARTEIRA 
							AND IN_ENQUADRADO='N') THEN 'N' ELSE 'S' END)
			
			--insere registro unico para processamento da carteira
			INSERT INTO [Quality_Control].[dbo].[RESULTADO_ENQUADRAMENTO_SINTETICO]
					   ([CD_RESULTADO_SINTETICO]
					   ,[DT_RESULTADO]
					   ,[CD_CARTEIRA]
					   ,[CD_PROCESSAMENTO]
					   ,[IN_ENQUADRADO]
					   ,[DT_ALTERACAO]
					   ,[CD_USUARIO_ALTERACAO])
				 VALUES
					   (@CD_RES_SINTETICO_NEW_ID
					   ,@DT_REFERENCIA
					   ,@CD_CARTEIRA
					   ,@CD_PROCESSAMENTO_NEWID
					   ,@IN_ENQ_SINTETICO
					   ,GETDATE()
					   ,@CD_USUARIO)			
			
			
		END TRY
		BEGIN CATCH
			SET @ERR_MSG = ERROR_MESSAGE()
			SET @ERR_LINE = ERROR_LINE()
			SET @ERR_PROC  = ERROR_PROCEDURE()

		
			IF @@TRANCOUNT > 0
				ROLLBACK TRANSACTION;
				PRINT 'TRANSACTION ROLLED BACK';
				
				
			SET @ERR_MSG =@ERR_MSG + ' procedure: ' + @ERR_PROC + ' line:' + CAST(@ERR_LINE AS VARCHAR)	  
			PRINT @ERR_MSG
			
			INSERT INTO LOG_PROCESSAMENTO 
			(DT_PROCESSADA, CD_CARTEIRA, CD_TIPO_FILTRO, CD_SUBTIPO_FILTRO, DS_DESCRICAO, CD_USUARIO_RESPONSAVEL, CD_PROCESSAMENTO, IN_TIPO_MENSAGEM)
			SELECT   
				@DT_REFERENCIA, @CD_CARTEIRA, NULL,NULL,
				LEFT(ISNULL(@ERR_MSG,''),999), 
				@CD_USUARIO,  @CD_PROCESSAMENTO_NEWID, 'ERRO'		
				
		END CATCH;	
		
		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION;	
			PRINT 'TRANSACTION COMMITTED';

		/***  FINAL DO BLOCO DE PROCESSAMENTO ***/
		-- Lendo a próxima linha
		FETCH NEXT FROM cursor_carteiras INTO @CD_CARTEIRA
	END

	-- Fechando Cursor para leitura
	CLOSE cursor_carteiras

	-- Desalocando o cursor
	DEALLOCATE cursor_carteiras 

	/** fim do loop **/;


	/** marcando processo como finalizado **/
	UPDATE PROCESSAMENTO SET DT_EXECUCAO_FIM = GETDATE(), IN_FINALIZADO = 'S'
	WHERE CD_PROCESSAMENTO = @CD_PROCESSAMENTO_NEWID

	SELECT 'Processamento realizado com sucesso.'
		
END


GO

