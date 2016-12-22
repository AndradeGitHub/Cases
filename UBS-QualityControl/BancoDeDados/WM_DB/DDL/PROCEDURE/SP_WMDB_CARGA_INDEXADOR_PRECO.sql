USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_INDEXADOR_PRECO]    Script Date: 08/26/2013 14:14:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WMDB_CARGA_INDEXADOR_PRECO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WMDB_CARGA_INDEXADOR_PRECO]
GO

USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_INDEXADOR_PRECO]    Script Date: 08/26/2013 14:14:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[SP_WMDB_CARGA_INDEXADOR_PRECO] (@dt_inicio smalldatetime)
as

set nocount on

delete from WM_DB.dbo.INDEXADOR_PRECO where DT_PRECO >= @dt_inicio

INSERT INTO WM_DB.dbo.INDEXADOR_PRECO
(CD_INDEXADOR, DT_PRECO, VL_PRECO, VL_FATOR, VL_PATRIMONIO)
SELECT IDX_INDEXDIR.CODINDEX AS CD_INDEXADOR,  
       DATA AS DT_PRECO,   
       ISNULL(VALORACU, 1.0) AS VL_PRECO,  
       ISNULL(FATORACU, 1.0) AS VL_FATOR,  
       VALOR VL_PATRIMONIO  
 FROM YMF_SAC..IDX_INDEX  
INNER JOIN YMF_SAC..IDX_INDEXDIR  
ON IDX_INDEX.CODINDEX = IDX_INDEXDIR.CODINDEX  
WHERE DATA >= @dt_inicio

delete from WM_DB.DBO.ATIVOS_PRECO where dt_cotacao >= @dt_inicio

insert into wm_db..ativos_preco
(CD_ATIVO, CD_TIPO_ATIVO, DT_COTACAO, VL_PRECO)
Select codpap, 'RV', DATA, isnull(FECH, PRECO)
	From [YMF_SAC]..[RV_COTPAP] a
Where isnull(FECH, PRECO) != 0
  And DATA >= @dt_inicio
  
GO

