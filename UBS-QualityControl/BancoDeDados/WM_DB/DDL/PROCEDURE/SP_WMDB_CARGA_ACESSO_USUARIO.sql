USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_ACESSO_USUARIO]    Script Date: 08/26/2013 14:13:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WMDB_CARGA_ACESSO_USUARIO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WMDB_CARGA_ACESSO_USUARIO]
GO

USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_ACESSO_USUARIO]    Script Date: 08/26/2013 14:13:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SP_WMDB_CARGA_ACESSO_USUARIO]
As

Set Nocount On 
/* SCRIPT RESPONSÁVEL PELA ATUALIZAÇÃO DAS TABELAS DE USUÁRIOS */

/* Insere Usuários que ainda não foram carregados no WM_DB */
INSERT INTO [WM_DB].[dbo].[ACESSO]
           ([CD_ACESSO]
           ,[NO_ACESSO]
           ,[IN_ATIVO_INATIVO]
           ,[CD_USUARIO_ULT_ALT]
           ,[DT_ULT_ALT])
SELECT  CD_USUARIO,
		NO_USUARIO,
		IN_ATIVO_INATIVO,
		NULL,
		NULL
FROM [WM_V_UBS_USUARIO]
WHERE CD_USUARIO NOT IN (SELECT CD_ACESSO FROM [WM_DB].[dbo].[ACESSO])

/* Inativa Usuários que não existirem no SAC */
Update [WM_DB].[dbo].[ACESSO]
	Set IN_ATIVO_INATIVO = A.IN_ATIVO_INATIVO,
	    NO_ACESSO = A.NO_USUARIO
From [WM_V_UBS_USUARIO] A
Inner Join [WM_DB].[dbo].[ACESSO] B
	On B.CD_ACESSO = a.CD_USUARIO


/* Insere Usuários que ainda não foram carregados no WM_DB */
INSERT INTO [WM_DB].[dbo].[USUARIO]
           ([CD_ACESSO]
           ,[CD_USUARIO]
           ,[NO_LOGIN]
           ,[DS_SENHA]
           ,[CD_USUARIO_GESTOR]
           ,[IN_ATIVO_INATIVO]
           ,[CD_USUARIO_ULT_ALT]
           ,[DT_ULT_ALT]
			,IN_CLIENT_ADVISOR
			,DS_EMAIL
			,DS_TELEFONE)
SELECT  CD_USUARIO,
		CD_USUARIO,
		NO_LOGIN,
		DS_SENHA,
		CD_USUARIO_GESTOR,
		IN_ATIVO_INATIVO,
		1,
		NULL
		,IN_CLIENT_ADVISOR
		,DS_EMAIL
		,DS_TELEFONE
FROM [WM_V_UBS_USUARIO]
WHERE CD_USUARIO NOT IN (SELECT CD_ACESSO FROM [WM_DB].[dbo].[USUARIO])

/* Atualiza os dados dos usuários já carregados no WM_DB */
Update [WM_DB].[dbo].[USUARIO]
	Set NO_LOGIN = A.NO_LOGIN,
		DS_SENHA = A.DS_SENHA,
		CD_USUARIO_GESTOR = A.CD_USUARIO_GESTOR,
		IN_ATIVO_INATIVO = A.IN_ATIVO_INATIVO,
		IN_CLIENT_ADVISOR = A.IN_CLIENT_ADVISOR,
		DS_EMAIL = A.DS_EMAIL,
		DS_TELEFONE = A.DS_TELEFONE
From [WM_V_UBS_USUARIO] A
Inner Join [WM_DB].[dbo].[USUARIO] B
	On B.CD_ACESSO = A.CD_USUARIO

GO

