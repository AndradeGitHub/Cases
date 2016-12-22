USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_GERAL]    Script Date: 08/26/2013 14:09:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WMDB_CARGA_GERAL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WMDB_CARGA_GERAL]
GO

USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_GERAL]    Script Date: 08/26/2013 14:09:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SP_WMDB_CARGA_GERAL](@DATA_INICIO SMALLDATETIME = NULL, @LISTA_CD_CARTEIRA VARCHAR(MAX) = NULL, @in_aut_manual char(1) = 'A', @cd_usuario int = null)
as
set nocount on

declare @cd_carga int
select @cd_carga = IsNull(MAX(cd_carga), 0) + 1 from carga

INSERT INTO CARGA
(CD_CARGA, DT_REFERENCIA, IN_TIPO_PROCESSAMENTO, DT_EXECUCAO_INI, DT_EXECUCAO_FIM, CD_USUARIO_RESPONSAVEL, IN_FINALIZADO)
VALUES
(@cd_carga, ISNULL(@DATA_INICIO, GETDATE()), @in_aut_manual, GETDATE(), null, @cd_usuario, 'N')

	/* inicio carga dados institucionais*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Dados Institucionais', GETDATE(), @cd_usuario, 'INFO')

		EXEC dbo.SP_WMDB_CARGA_DADOS_INSTITUCIONAIS

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Dados Institucionais', GETDATE(), @cd_usuario, 'INFO')	
	/* fim carga dados institucionais*/

	/* inicio carga carteira*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Dados Carteira', GETDATE(), @cd_usuario, 'INFO')
	
		EXEC dbo.SP_WMDB_CARGA_CARTEIRA
	
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Dados Carteira', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga carteira*/
	
	/* inicio carga acesso usuario*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Acesso Usuário', GETDATE(), @cd_usuario, 'INFO')
	
		EXEC dbo.SP_WMDB_CARGA_ACESSO_USUARIO
		
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Acesso Usuário', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga acesso usuario */		

	/* inicio carga perfil risco*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Perfil Risco', GETDATE(), @cd_usuario, 'INFO')		

		EXEC dbo.SP_WMDB_CARGA_PERFIL_RISCO

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Perfil Risco', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga perfil risco */			

	/* inicio carga Mercado*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Mercado', GETDATE(), @cd_usuario, 'INFO')		
	
		EXEC dbo.SP_WMDB_CARGA_MERCADO

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Mercado', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga Mercado */			

	/* inicio carga Indexadores*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Indexadores', GETDATE(), @cd_usuario, 'INFO')		

		EXEC dbo.SP_WMDB_CARGA_INDEXADORES
		
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Indexadores', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga Indexadores */			

	/* inicio carga Carteira Cota*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Carteira Cota - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')			
	
		EXEC dbo.SP_WMDB_CARGA_CARTEIRA_COTA @dt_inicio = @DATA_INICIO, @LISTA_CD_CARTEIRA = @LISTA_CD_CARTEIRA

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Carteira Cota - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga Carteira Cota */			

	/* inicio carga indexador preco*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Indexador Preço', GETDATE(), @cd_usuario, 'INFO')	
	
		declare @dt_inicio_index smalldatetime
		select @dt_inicio_index = DATEADD(day, -7, GETDATE())
		EXEC dbo.SP_WMDB_CARGA_INDEXADOR_PRECO @dt_inicio = @dt_inicio_index
		
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Indexador Preço', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga indexador preco */			
		
	/* inicio carga Ativos*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Ativos', GETDATE(), @cd_usuario, 'INFO')	
	
		EXEC dbo.SP_WMDB_CARGA_ATIVOS

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Ativos', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga Ativos*/			

	/* inicio carga posição*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Posição - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')	
	
		EXEC dbo.SP_WMDB_CARGA_POSICAO @dt_inicio = @DATA_INICIO, @LISTA_CD_CARTEIRA = @LISTA_CD_CARTEIRA

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Posição - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga posição*/					

	/* inicio carga movimento*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Movimento - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')			

		EXEC dbo.SP_WMDB_CARGA_MOVIMENTO @dt_inicio = @DATA_INICIO, @LISTA_CD_CARTEIRA = @LISTA_CD_CARTEIRA

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Movimento - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga movimento*/	

	/* inicio carga rentabilidade carteira benchmark*/
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Rentabilidade Carteira Benchmark - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')
	
		EXEC dbo.SP_WMDB_CARGA_RENT_CART_BENCHMARK @dt_inicio = @DATA_INICIO, @LISTA_CD_CARTEIRA = @LISTA_CD_CARTEIRA
	
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Rentabilidade Carteira Benchmark - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga rentabilidade carteira benchmark */	
	
	/* inicio carga carteira benchmark */
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Carteira Benchmark', GETDATE(), @cd_usuario, 'INFO')	
	
		EXEC dbo.SP_WMDB_CARGA_CARTEIRA_BENCHMARK

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Carteira Benchmark', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga carteira benchmark */	

	/* inicio carga Rentabilidade Carteira */
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Rentabilidade Carteira - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')	
	
		--exec dbo.SP_WMDB_CARGA_RENTABILIDADECARTEIRA @LISTA_CD_CARTEIRA = @LISTA_CD_CARTEIRA

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Rentabilidade Carteira - ' + ISNULL(@LISTA_CD_CARTEIRA, 'Todas as Carteiras'), GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga Rentabilidade Carteira */	

declare @data_fim smalldatetime
declare @data_ini smalldatetime

select @data_ini = @DATA_INICIO
select @data_fim = dateadd(month, 1, @data_ini)

	/* inicio carga Rentabilidade Indexadores */
	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Inicio Carga Rentabilidade Indexadores', GETDATE(), @cd_usuario, 'INFO')	

		while @data_ini < @data_fim
		  begin 
		  
			EXEC dbo.SP_WMDB_CARGA_RENTABILIDADEINDEXADORES @data = @data_ini
			
			Select @data_ini = dbo.fn_calcula_dia_util(@data_ini, 1)		
			
			print @data_ini
		  end 

	insert into LOG_CARGA
	(CD_CARGA, DT_CARGA, CD_CARTEIRA, DS_DESCRICAO, DT_LOG, CD_USUARIO_RESPONSAVEL, IN_TIPO_MENSAGEM)
	VALUES
	(@cd_carga, @DATA_INICIO, NULL, 'Fim Carga Rentabilidade Indexadores', GETDATE(), @cd_usuario, 'INFO')			
	/* fim carga Rentabilidade Indexadores */	  
  
/* Atualiza Log de Processamento da Carteira */
insert into log_processamento_carteira
Select a.cd_carteira, MIN(dt)'dt_inicio', MIN(dt_inicial), MAX(dt_final), 'F', MAX(dt_fim_proc) 'dt_fim', max(dt_carteira)
	From WM_V_UBS_LOG_PROC_CARTEIRA a
--Where cd_carteira = '37173-3'
Group by a.cd_carteira
Having MIN(dt) > IsNull(@DATA_INICIO, (Select MAX(dt_fim_proc) from log_processamento_carteira where cd_carteira = a.cd_carteira))

update CARGA
	set DT_EXECUCAO_FIM = GETDATE(),
		IN_FINALIZADO = 'S'
where cd_carga = @cd_carga	

GO

