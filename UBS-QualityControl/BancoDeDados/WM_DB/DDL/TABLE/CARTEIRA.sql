USE [WM_DB]
GO

/****** Object:  Table [dbo].[CARTEIRA]    Script Date: 08/26/2013 13:58:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CARTEIRA]') AND type in (N'U'))
DROP TABLE [dbo].[CARTEIRA]
GO

USE [WM_DB]
GO

/****** Object:  Table [dbo].[CARTEIRA]    Script Date: 08/26/2013 13:58:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CARTEIRA](
	[CD_CARTEIRA] [varchar](15) NOT NULL,
	[NO_CARTEIRA] [varchar](80) NOT NULL,
	[NU_CGC] [varchar](14) NULL,
	[IN_ATIVO_INATIVO] [char](1) NULL,
	[CD_CLIENTE] [int] NULL,
	[DT_ABERTURA] [datetime] NOT NULL,
	[DT_ENCERRAMENTO] [datetime] NULL,
	[CD_PERFIL_RISCO] [char](8) NULL,
	[CD_ADMINISTRADOR] [varchar](15) NULL,
	[CD_CUSTODIANTE] [varchar](12) NULL,
	[CD_GESTOR] [varchar](12) NULL,
	[CD_INDEX] [char](8) NULL,
	[CD_USUARIO_CA] [int] NULL,
	[IN_TIPO_PESSOA] [char](1) NULL,
	[CD_MOEDA] [varchar](8) NULL,
	[IN_TIPO_CARTEIRA] [char](1) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

