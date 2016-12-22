﻿USE [Quality_Control]
GO

/****** Object:  StoredProcedure [dbo].[SP_LISTA_INCONSISTENCIA_DETALHE]    Script Date: 09/27/2013 18:41:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_LISTA_INCONSISTENCIA_DETALHE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_LISTA_INCONSISTENCIA_DETALHE]
GO

USE [Quality_Control]
GO

/****** Object:  StoredProcedure [dbo].[SP_LISTA_INCONSISTENCIA_DETALHE]    Script Date: 09/27/2013 18:41:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_LISTA_INCONSISTENCIA_DETALHE]
(
	@CD_PROCESSAMENTO INT , /*CODIGO DO PROCESSAMENTO*/
	@CD_CARTEIRA VARCHAR(15) /*CODIGO DA CARTEIRA*/
)
AS BEGIN

	SELECT DISTINCT
		c.CD_CARTEIRA, 
		c.NO_CARTEIRA,
		TF.CD_TIPO_FILTRO_PAI,
		tf.NO_TIPO_FILTRO, 
		CASE  
		WHEN re.CD_TIPO_FILTRO = 1  THEN 'Valor na ponta A: '+ ISNULL(CAST(VL_PONTA_A AS VARCHAR),'') +'|Valor na ponta B: '+ ISNULL(CAST(VL_PONTA_B AS VARCHAR),'')  -- + re.VL_PONTA_A + re.VL_PONTA_B
		WHEN re.CD_TIPO_FILTRO = 2  THEN 'Limite mínimo aceito pelo perfil: '+ ISNULL(CAST(VL_LIMITE_MIN AS VARCHAR),'') +' |Limite máximo aceito pelo perfil: '+ ISNULL(CAST(VL_LIMITE_MAX AS VARCHAR),'') +'|Valorização apurada: '+ ISNULL(CAST(VL_VARIACAO_APURADO AS VARCHAR),'') -- + re.VL_PONTA_A + re.VL_PONTA_A + re.VL_PONTA_A  
		WHEN re.CD_SUBTIPO_FILTRO = 13 THEN 'Data da abertura: ' + CONVERT(VARCHAR, DT_ABERTURA_CART, 103)  --+ re.DT_ABERTURA_CART
		WHEN re.CD_SUBTIPO_FILTRO = 14 THEN 'Data do encerramento:' + CONVERT(VARCHAR, DT_ENCERRAMENTO_CART, 103)  -- + re.DT_ENCERRAMENTO_CART
		WHEN re.CD_SUBTIPO_FILTRO = 15 THEN 'AuM: ' + ISNULL(CAST(VL_PONTA_A AS VARCHAR),'') --+ re.VL_PONTA_A
		WHEN re.CD_SUBTIPO_FILTRO = 16 THEN 'Ativo: ' + ISNULL(re.CD_ATIVO,'') + '-' + ISNULL(re.CD_TIPO_ATIVO,'')  
		ELSE ' - '
		END	AS DS_CAUSA_INCONSISTENCIA,
		re.CD_SUBTIPO_FILTRO,
		re.CD_PROCESSAMENTO,
		re.IN_LIBERADO		
	FROM Quality_Control..RESULTADO_ENQUADRAMENTO re
	INNER JOIN WM_DB..CARTEIRA c ON re.CD_CARTEIRA = c.CD_CARTEIRA
	INNER JOIN Quality_Control..TIPO_FILTRO tf ON re.CD_TIPO_FILTRO = tf.CD_TIPO_FILTRO_PAI AND re.CD_SUBTIPO_FILTRO = tf.CD_TIPO_FILTRO
	WHERE 
	re.CD_PROCESSAMENTO = @CD_PROCESSAMENTO 
	AND re.CD_CARTEIRA = @CD_CARTEIRA
	AND re.IN_ENQUADRADO = 'N'
	ORDER BY 
	TF.CD_TIPO_FILTRO_PAI
	
END



GO


