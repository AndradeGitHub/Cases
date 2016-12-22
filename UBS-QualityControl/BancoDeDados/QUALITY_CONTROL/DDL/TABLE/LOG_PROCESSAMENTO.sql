USE [Quality_Control]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LOG_PROCESSAMENTO_PROCESSAMENTO]') AND parent_object_id = OBJECT_ID(N'[dbo].[LOG_PROCESSAMENTO]'))
ALTER TABLE [dbo].[LOG_PROCESSAMENTO] DROP CONSTRAINT [FK_LOG_PROCESSAMENTO_PROCESSAMENTO]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LOG_SUBTIP_FIL]') AND parent_object_id = OBJECT_ID(N'[dbo].[LOG_PROCESSAMENTO]'))
ALTER TABLE [dbo].[LOG_PROCESSAMENTO] DROP CONSTRAINT [FK_LOG_SUBTIP_FIL]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LOG_TIP_FIL]') AND parent_object_id = OBJECT_ID(N'[dbo].[LOG_PROCESSAMENTO]'))
ALTER TABLE [dbo].[LOG_PROCESSAMENTO] DROP CONSTRAINT [FK_LOG_TIP_FIL]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LOG_PROCESSAMENTO_DT_PROCESSAMENTO]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LOG_PROCESSAMENTO] DROP CONSTRAINT [DF_LOG_PROCESSAMENTO_DT_PROCESSAMENTO]
END

GO

USE [Quality_Control]
GO

/****** Object:  Table [dbo].[LOG_PROCESSAMENTO]    Script Date: 08/26/2013 13:52:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOG_PROCESSAMENTO]') AND type in (N'U'))
DROP TABLE [dbo].[LOG_PROCESSAMENTO]
GO

USE [Quality_Control]
GO

/****** Object:  Table [dbo].[LOG_PROCESSAMENTO]    Script Date: 08/26/2013 13:52:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LOG_PROCESSAMENTO](
	[CD_LOG] [int] IDENTITY(1,1) NOT NULL,
	[DT_PROCESSADA] [smalldatetime] NOT NULL,
	[CD_CARTEIRA] [varchar](15) NOT NULL,
	[CD_TIPO_FILTRO] [int] NULL,
	[CD_SUBTIPO_FILTRO] [int] NULL,
	[DS_DESCRICAO] [varchar](1000) NULL,
	[DT_PROCESSAMENTO] [datetime] NOT NULL,
	[CD_USUARIO_RESPONSAVEL] [int] NOT NULL,
	[CD_PROCESSAMENTO] [int] NULL,
	[IN_TIPO_MENSAGEM] [char](4) NULL,
 CONSTRAINT [PK_LOG_PROC] PRIMARY KEY CLUSTERED 
(
	[CD_LOG] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[LOG_PROCESSAMENTO]  WITH CHECK ADD  CONSTRAINT [FK_LOG_PROCESSAMENTO_PROCESSAMENTO] FOREIGN KEY([CD_PROCESSAMENTO])
REFERENCES [dbo].[PROCESSAMENTO] ([CD_PROCESSAMENTO])
GO

ALTER TABLE [dbo].[LOG_PROCESSAMENTO] CHECK CONSTRAINT [FK_LOG_PROCESSAMENTO_PROCESSAMENTO]
GO

ALTER TABLE [dbo].[LOG_PROCESSAMENTO]  WITH CHECK ADD  CONSTRAINT [FK_LOG_SUBTIP_FIL] FOREIGN KEY([CD_TIPO_FILTRO])
REFERENCES [dbo].[TIPO_FILTRO] ([CD_TIPO_FILTRO])
GO

ALTER TABLE [dbo].[LOG_PROCESSAMENTO] CHECK CONSTRAINT [FK_LOG_SUBTIP_FIL]
GO

ALTER TABLE [dbo].[LOG_PROCESSAMENTO]  WITH CHECK ADD  CONSTRAINT [FK_LOG_TIP_FIL] FOREIGN KEY([CD_TIPO_FILTRO])
REFERENCES [dbo].[TIPO_FILTRO] ([CD_TIPO_FILTRO])
GO

ALTER TABLE [dbo].[LOG_PROCESSAMENTO] CHECK CONSTRAINT [FK_LOG_TIP_FIL]
GO

ALTER TABLE [dbo].[LOG_PROCESSAMENTO] ADD  CONSTRAINT [DF_LOG_PROCESSAMENTO_DT_PROCESSAMENTO]  DEFAULT (getdate()) FOR [DT_PROCESSAMENTO]
GO
