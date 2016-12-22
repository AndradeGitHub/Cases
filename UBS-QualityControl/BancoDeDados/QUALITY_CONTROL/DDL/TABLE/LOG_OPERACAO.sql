USE [Quality_Control]
GO

/****** Object:  Table [dbo].[LOG_OPERACAO]    Script Date: 08/26/2013 13:52:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_OPERACAO]') AND type in (N'U'))
DROP TABLE [dbo].[LOG_OPERACAO]
GO

USE [Quality_Control]
GO

/****** Object:  Table [dbo].[LOG_OPERACAO]    Script Date: 08/26/2013 13:52:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LOG_OPERACAO](
	[NO_FUNCIONALIDADE] [varchar](100) NOT NULL,
	[NO_ACAO] [varchar](100) NOT NULL,
	[TX_DESCRICAO] [varchar](max) NOT NULL,
	[DT_LOG] [smalldatetime] NOT NULL,
	[CD_USUARIO] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

