USE [WM_DB]
GO

/****** Object:  Table [dbo].[CARTEIRA_COTA]    Script Date: 08/26/2013 13:58:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CARTEIRA_COTA]') AND type in (N'U'))
DROP TABLE [dbo].[CARTEIRA_COTA]
GO

USE [WM_DB]
GO

/****** Object:  Table [dbo].[CARTEIRA_COTA]    Script Date: 08/26/2013 13:58:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CARTEIRA_COTA](
	[CD_CARTEIRA] [varchar](15) NOT NULL,
	[DT_COTA] [datetime] NOT NULL,
	[QT_COTA] [float] NULL,
	[VL_COTA] [float] NULL,
	[VL_PATRIMONIO] [float] NULL,
	[VL_APLICACAO] [float] NULL,
	[VL_RESGATE] [float] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

