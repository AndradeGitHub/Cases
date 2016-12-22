USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_CARTEIRA_COTA]    Script Date: 08/26/2013 14:14:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WMDB_CARGA_CARTEIRA_COTA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WMDB_CARGA_CARTEIRA_COTA]
GO

USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_CARTEIRA_COTA]    Script Date: 08/26/2013 14:14:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_WMDB_CARGA_CARTEIRA_COTA] (@LISTA_CD_CARTEIRA VARCHAR(MAX) = null, @dt_inicio smalldatetime = null)
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
		DELETE z  FROM [WM_DB].[dbo].CARTEIRA_COTA z
			Inner Join (Select MIN(dt_inicial)'dt_inicio', MAX(dt_final) 'dt_fim', a.cd_carteira
							From WM_V_UBS_LOG_PROC_CARTEIRA a
							Inner Join log_processamento_carteira b
								on b.cd_carteira = a.cd_carteira
						Where a.DT > b.dt_carteira
						Group by a.cd_carteira) y
				on y.cd_carteira = z.CD_CARTEIRA
		where z.DT_COTA >= y.dt_inicio
		  and (z.CD_CARTEIRA in (select CD from @table) or @LISTA_CD_CARTEIRA is null)	
	end 	
else
	begin
		DELETE FROM [WM_DB].[dbo].CARTEIRA_COTA
		WHERE (CD_CARTEIRA in (select CD from @table) or @LISTA_CD_CARTEIRA is null)	
		  AND DT_COTA >= @dt_inicio 
	end    

INSERT INTO WM_DB.dbo.CARTEIRA_COTA
(CD_CARTEIRA, DT_COTA, QT_COTA, VL_COTA, VL_PATRIMONIO, VL_APLICACAO, VL_RESGATE)
SELECT A.CD_CARTEIRA, A.DT_REF, A.VL_QT_COTA, A.VL_COTA, A.VL_SALDO, A.VL_APLICACAO, A.VL_RESGATE 
	FROM [WM_V_UBS_CARTEIRA_COTA] A
	LEFT JOIN (select top 1 CD_CARTEIRA, 
					   DT_INICIO, 
					   DT_FIM 
				  from LOG_PROCESSAMENTO_CARTEIRA z 
			    where DT_FIM_PROC = (select MAX(DT_FIM_PROC) 
									 from LOG_PROCESSAMENTO_CARTEIRA 
								   where CD_CARTEIRA = z.CD_CARTEIRA)) G
	ON G.CD_CARTEIRA = A.[CD_CARTEIRA]	
WHERE A.CD_CARTEIRA IN (SELECT CD_CARTEIRA FROM WM_DB.dbo.CARTEIRA)
  and (A.CD_CARTEIRA IN (SELECT CD FROM @TABLE) OR @LISTA_CD_CARTEIRA is null)
  and A.DT_REF >= ISNULL(@dt_inicio, G.DT_INICIO)
GO

