USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_RENT_CART_BENCHMARK]    Script Date: 08/26/2013 14:15:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WMDB_CARGA_RENT_CART_BENCHMARK]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WMDB_CARGA_RENT_CART_BENCHMARK]
GO

USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_RENT_CART_BENCHMARK]    Script Date: 08/26/2013 14:15:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SP_WMDB_CARGA_RENT_CART_BENCHMARK] (@dt_inicio smalldatetime = NULL, @LISTA_CD_CARTEIRA varchar(max) = null)
as

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
		DELETE z  FROM [WM_DB].[dbo].[RENTABILIDADE_CARTEIRA_BENCHMARK] z
			Inner Join (Select MIN(dt_inicial)'dt_inicio', MAX(dt_final) 'dt_fim', a.cd_carteira
							From WM_V_UBS_LOG_PROC_CARTEIRA a
							Inner Join log_processamento_carteira b
								on b.cd_carteira = a.cd_carteira
						Where a.DT > b.dt_carteira
						Group by a.cd_carteira) y
				on y.cd_carteira = z.CD_CARTEIRA
		where z.DT_POSICAO >= y.dt_inicio
		  and (z.CD_CARTEIRA in (select CD from @table) or @LISTA_CD_CARTEIRA is null)	
	end 	
else
	begin
		DELETE FROM [WM_DB].[dbo].[RENTABILIDADE_CARTEIRA_BENCHMARK]
		WHERE (CD_CARTEIRA in (select CD from @table) or @LISTA_CD_CARTEIRA is null)	
		  AND DT_POSICAO >= @dt_inicio 
	end   


INSERT INTO WM_DB.[dbo].[RENTABILIDADE_CARTEIRA_BENCHMARK]
           ([CD_CARTEIRA] ,[DT_POSICAO] ,[CD_INDEXADOR] ,[DT_DIA_INI] ,[DT_DIA_FIM] ,[DT_MES_INI] ,[DT_MES_FIM]
           ,[DT_ANO_INI] ,[DT_ANO_FIM] ,[DT_TRI_INI] ,[DT_TRI_FIM] ,[DT_06M_INI] ,[DT_06M_FIM] ,[DT_12M_INI] ,[DT_12M_FIM]
           ,[DT_360D_INI] ,[DT_360D_FIM] ,[VL_RENT_DIA] ,[VL_RENT_MES] ,[VL_RENT_ANO] ,[VL_RENT_TRI] ,[VL_RENT_06M] ,[VL_RENT_12M]
           ,[VL_RENT_36M])
SELECT 
		 A.CLCLI_CD
		,A.DT_POS
		,A.IDDIR_CD_COMP1
		,A.DT_DIA_INI, DT_DIA_FIM
		,A.DT_MES_INI, DT_MES_FIM
		,A.DT_ANO_INI, DT_ANO_FIM
		,A.DT_TRI_INI, DT_TRI_FIM
		,A.DT_06M_INI, DT_06M_FIM
		,A.DT_12M_INI, DT_12M_FIM
		,A.DT_360D_INI, DT_360D_FIM	
        ,A.PC_DIA_I1
        ,A.PC_MES_I1
        ,A.PC_ANO_I1
        ,A.PC_TRI_I1
        ,A.PC_06M_I1
        ,A.PC_12M_I1
        ,A.PC_360DF_I1				
FROM YMF_SAC..SAC_CL_RENTAB A
LEFT JOIN (select top 1 CD_CARTEIRA, 
				   DT_INICIO, 
				   DT_FIM 
			  from LOG_PROCESSAMENTO_CARTEIRA z 
		    where DT_FIM_PROC = (select MAX(DT_FIM_PROC) 
								 from LOG_PROCESSAMENTO_CARTEIRA 
							   where CD_CARTEIRA = z.CD_CARTEIRA)) G
	ON G.CD_CARTEIRA = A.CLCLI_CD	
WHERE A.DT_POS >= ISNULL(@dt_inicio, G.DT_INICIO)
  AND CLCLI_CD IN (SELECT CD_CARTEIRA FROM WM_DB..CARTEIRA)
  AND (CLCLI_CD IN (SELECT CD FROM @table) OR @LISTA_CD_CARTEIRA is null)
  AND NOT IDDIR_CD_COMP1 IS NULL  

GO

