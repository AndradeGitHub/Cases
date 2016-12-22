USE [WM_DB]
GO

/****** Object:  View [dbo].[WM_V_UBS_LOG_PROC_CARTEIRA]    Script Date: 08/26/2013 14:17:23 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[WM_V_UBS_LOG_PROC_CARTEIRA]'))
DROP VIEW [dbo].[WM_V_UBS_LOG_PROC_CARTEIRA]
GO

USE [WM_DB]
GO

/****** Object:  View [dbo].[WM_V_UBS_LOG_PROC_CARTEIRA]    Script Date: 08/26/2013 14:17:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[WM_V_UBS_LOG_PROC_CARTEIRA] AS
(
	Select A.CLCLI_CD 'CD_CARTEIRA', 
	       A.DT, 
	       B.DT_INICIAL, 
	       B.DT_FINAL, 
	       B.IC_TIPOPROC, 
	       B.DT_FIM_PROC,
	       c.CL 'DT_CARTEIRA'
		From ymf_sac..SAC_BA_LOG_CLIENTES A
		Inner Join YMF_SAC..SAC_BA_LOG_PROC B
			on b.ID = a.ID
		   And b.DT = a.DT		
		Inner Join ymf_sac..RV_DATA_PROC C
			on c.CODCLI = a.CLCLI_CD
	Where a.IC_STATUS_PROC = 'F'
	  And b.DT_FINAL = c.CL
	  And b.SG_MODULO = 'CL'
	  And b.IC_TIPOPROC = 'F'
	  -- And CLCLI_CD = '37784-7'	
	  and c.CL is not null
)	
GO

