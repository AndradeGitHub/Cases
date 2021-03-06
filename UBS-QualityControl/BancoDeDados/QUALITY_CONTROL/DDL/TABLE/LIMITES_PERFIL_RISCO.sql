USE [Quality_Control]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_sub_tipo_filtro]') AND parent_object_id = OBJECT_ID(N'[dbo].[LIMITES_PERFIL_RISCO]'))
ALTER TABLE [dbo].[LIMITES_PERFIL_RISCO] DROP CONSTRAINT [fk_sub_tipo_filtro]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TIPO_FILTRO]') AND parent_object_id = OBJECT_ID(N'[dbo].[LIMITES_PERFIL_RISCO]'))
ALTER TABLE [dbo].[LIMITES_PERFIL_RISCO] DROP CONSTRAINT [FK_TIPO_FILTRO]
GO

USE [Quality_Control]
GO

/****** Object:  Table [dbo].[LIMITES_PERFIL_RISCO]    Script Date: 08/26/2013 13:52:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LIMITES_PERFIL_RISCO]') AND type in (N'U'))
DROP TABLE [dbo].[LIMITES_PERFIL_RISCO]
GO

USE [Quality_Control]
GO

/****** Object:  Table [dbo].[LIMITES_PERFIL_RISCO]    Script Date: 08/26/2013 13:52:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LIMITES_PERFIL_RISCO](
	[CD_LIMITE_PERFIL_RISCO] [int] NOT NULL,
	[CD_PERFIL_RISCO] [char](8) NOT NULL,
	[DT_INI_VIGENCIA] [smalldatetime] NOT NULL,
	[DT_FIM_VIGENCIA] [smalldatetime] NOT NULL,
	[VL_LIMITE_MIN] [float] NULL,
	[VL_LIMITE_MAX] [float] NULL,
	[CD_TIPO_FILTRO] [int] NOT NULL,
	[CD_SUBTIPO_FILTRO] [int] NOT NULL,
	[DT_ALTERACAO] [datetime] NULL,
	[CD_USUARIO_ALTERACAO] [int] NULL,
	[IN_EXCECAO] [char](1) NOT NULL,
	[IN_DIARIO_MENSAL] [char](1) NOT NULL,
 CONSTRAINT [PK_LIMITES_PERFIL_RISCO] PRIMARY KEY CLUSTERED 
(
	[CD_LIMITE_PERFIL_RISCO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_LIMITES_PERFIL_RISCO] UNIQUE NONCLUSTERED 
(
	[CD_PERFIL_RISCO] ASC,
	[DT_INI_VIGENCIA] ASC,
	[CD_TIPO_FILTRO] ASC,
	[CD_SUBTIPO_FILTRO] ASC,
	[IN_EXCECAO] ASC,
	[IN_DIARIO_MENSAL] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[LIMITES_PERFIL_RISCO]  WITH CHECK ADD  CONSTRAINT [fk_sub_tipo_filtro] FOREIGN KEY([CD_SUBTIPO_FILTRO])
REFERENCES [dbo].[TIPO_FILTRO] ([CD_TIPO_FILTRO])
GO

ALTER TABLE [dbo].[LIMITES_PERFIL_RISCO] CHECK CONSTRAINT [fk_sub_tipo_filtro]
GO

ALTER TABLE [dbo].[LIMITES_PERFIL_RISCO]  WITH CHECK ADD  CONSTRAINT [FK_TIPO_FILTRO] FOREIGN KEY([CD_TIPO_FILTRO])
REFERENCES [dbo].[TIPO_FILTRO] ([CD_TIPO_FILTRO])
GO

ALTER TABLE [dbo].[LIMITES_PERFIL_RISCO] CHECK CONSTRAINT [FK_TIPO_FILTRO]
GO

