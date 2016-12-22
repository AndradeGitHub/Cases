USE [WM_DB]
GO

/****** Object:  Table [dbo].[LOG_OPERACAO]    Script Date: 08/13/2013 09:24:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_OPERACAO]') AND type in (N'U'))
DROP TABLE [dbo].[LOG_OPERACAO]
GO

USE [WM_DB]
GO

/****** Object:  Table [dbo].[LOG_OPERACAO]    Script Date: 08/13/2013 09:24:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LOG_OPERACAO](
	[NO_SISTEMA] [varchar](50) NOT NULL,
	[NO_FUNCIONALIDADE] [varchar](100) NOT NULL,
	[NO_TIPO_FUNCIONALIDADE] [varchar](50) NOT NULL,
	[NO_ACAO] [varchar](100) NOT NULL,
	[NO_TIPO_DESCRICAO]	[varchar](50) NOT NULL,
	[TX_DESCRICAO] [varchar](max) NOT NULL,
	[DT_LOG] [smalldatetime] NOT NULL,
	[CD_USUARIO] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


