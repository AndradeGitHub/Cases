USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_MOVIMENTO]    Script Date: 08/26/2013 14:15:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WMDB_CARGA_MOVIMENTO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WMDB_CARGA_MOVIMENTO]
GO

USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_MOVIMENTO]    Script Date: 08/26/2013 14:15:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SP_WMDB_CARGA_MOVIMENTO] (@dt_inicio smalldatetime = NULL, @LISTA_CD_CARTEIRA varchar(max) = null)
AS
set nocount on 

declare @table table
(
	CD varchar(15)
)

IF NOT @LISTA_CD_CARTEIRA IS NULL
BEGIN
	INSERT INTO @table (CD) 
	SELECT DISTINCT STRVAL FROM WM_DB.[dbo].[Split](@LISTA_CD_CARTEIRA, ',')			
END

if @dt_inicio is null
	begin
		DELETE z  FROM [WM_DB].[dbo].[MOVIMENTO] z
			Inner Join (Select MIN(dt_inicial)'dt_inicio', MAX(dt_final) 'dt_fim', a.cd_carteira
							From WM_V_UBS_LOG_PROC_CARTEIRA a
							Inner Join log_processamento_carteira b
								on b.cd_carteira = a.cd_carteira
						Where a.DT > b.dt_carteira
						Group by a.cd_carteira) y
				on y.cd_carteira = z.CD_CARTEIRA
		where z.DT_MOVIMENTO >= y.dt_inicio
		  and (z.CD_CARTEIRA in (select CD from @table) or @LISTA_CD_CARTEIRA is null)	
	end 	
else
	begin
		DELETE FROM [WM_DB].[dbo].[MOVIMENTO]
		WHERE (CD_CARTEIRA in (select CD from @table) or @LISTA_CD_CARTEIRA is null)	
		  AND [DT_MOVIMENTO] >= @dt_inicio 
	end  

INSERT INTO [WM_DB].[dbo].[MOVIMENTO]
           ([CD_MOVIMENTO]
           ,[DT_MOVIMENTO]
           ,[CD_CARTEIRA]
           ,[CD_ATIVO]
           ,[CD_TIPO_ATIVO]
           ,[CD_TIPO_MOVIMENTO]
           ,[VL_PU]
           ,[VL_QUANTIDADE]
           ,[VL_VALOR_MOV]
           ,[VL_IOF]
           ,[VL_IRRF]
           ,[CD_MOVIMENTO_LEGADO]
           ,[DT_SOLICITACAO]
           ,[CD_CORRETORA]
           ,[VL_COTA_MOV]
           ,[PC_TAXA]
           ,[VL_PASSIVO]
           ,[VL_ATIVO]
           ,[DT_APLICACAO]
           ,[VL_AJUSTE]
           ,[VL_BRUTO]
           ,[TX_DETALHE_MOVIMENTO]
           ,[DT_LIQ_FISICA]
		   ,[DT_LIQ_FINANC]
		   ,[VL_RENDIMENTO]
		   ,[PC_INDEXADOR_UTLZ])
SELECT  
            v.[CD_MOVIMENTO]
           ,v.[DT_MOVIMENTO]
           ,v.[CD_CARTEIRA]
           ,v.[CD_ATIVO]
           ,v.[CD_TIPO_ATIVO]
           ,v.[CD_TIPO_MOVIMENTO]
           ,v.[VL_PU]
           ,v.[VL_QUANTIDADE]
           ,v.[VL_VALOR_MOV]
           ,v.[VL_IOF]
           ,v.[VL_IRRF]
           ,v.[CD_MOVIMENTO_LEGADO]
           ,v.[DT_SOLICITACAO]
           ,v.[CD_CORRETORA]
           ,v.[VL_COTA_MOV]
           ,v.[PC_TAXA]
           ,v.[VL_PASSIVO]
           ,v.[VL_ATIVO]
           ,v.[DT_APLICACAO]
           ,v.[VL_AJUSTE]
           ,v.[VL_BRUTO]
           ,v.[TX_DETALHE_MOVIMENTO]
           ,v.[DT_LIQ_FISICA]
		   ,v.[DT_LIQ_FINANC]
		   ,v.[VL_RENDIMENTO]           
		   ,v.[PC_INDEXADOR_UTLZ]
FROM [WM_V_UBS_MOVIMENTO] v
	INNER JOIN WM_DB.dbo.ATIVOS a ON v.CD_ATIVO = a.CD_ATIVO AND v.CD_TIPO_ATIVO = a.CD_TIPO_ATIVO
	INNER JOIN WM_DB.dbo.CARTEIRA c ON v.CD_CARTEIRA = c.CD_CARTEIRA
	LEFT JOIN (select top 1 CD_CARTEIRA, 
					   DT_INICIO, 
					   DT_FIM 
				  from LOG_PROCESSAMENTO_CARTEIRA z 
			    where DT_FIM_PROC = (select MAX(DT_FIM_PROC) 
									 from LOG_PROCESSAMENTO_CARTEIRA 
								   where CD_CARTEIRA = z.CD_CARTEIRA)) G
	ON G.CD_CARTEIRA = v.[CD_CARTEIRA]
WHERE v.[DT_MOVIMENTO] >= ISNULL(@dt_inicio, G.DT_INICIO)
  AND (v.CD_CARTEIRA in (select cd from @table) or @LISTA_CD_CARTEIRA is null)
GO

