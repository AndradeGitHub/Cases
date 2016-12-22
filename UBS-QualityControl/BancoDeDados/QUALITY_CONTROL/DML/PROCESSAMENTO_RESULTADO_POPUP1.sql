/****************************************************************************
 Query para filtros sem considerar ativos (nao bloqueio de ativos)	
****************************************************************************/
DECLARE @DT_REF DATETIME 
SELECT @DT_REF = '2013-09-30' 

SELECT DISTINCT
	c.CD_CARTEIRA,
	c.NO_CARTEIRA
FROM 
	QUALITY_CONTROL..RESULTADO_ENQUADRAMENTO RE
	 INNER JOIN QUALITY_CONTROL..TIPO_FILTRO TF ON 
	  RE.CD_SUBTIPO_FILTRO = TF.CD_TIPO_FILTRO
	 INNER JOIN WM_DB..CARTEIRA c ON 
	  re.CD_CARTEIRA = c.CD_CARTEIRA
	 INNER JOIN QUALITY_CONTROL..PROCESSAMENTO P ON 
	  RE.CD_PROCESSAMENTO = P.CD_PROCESSAMENTO
WHERE 
	RE.DT_RESULTADO			 = @DT_REF
	AND RE.IN_ENQUADRADO	 = 'N'
	AND P.CD_PROCESSAMENTO	 = (SELECT MAX(CD_PROCESSAMENTO) FROM QUALITY_CONTROL..PROCESSAMENTO WHERE DT_REFERENCIA = @DT_REF AND IN_FINALIZADO = 'S')
	AND P.IN_FINALIZADO		 = 'S'
	AND re.CD_SUBTIPO_FILTRO = 10
ORDER BY 
	c.CD_CARTEIRA


/****************************************************************************
 Query para filtros considerando ativos (bloqueio de ativos)	
****************************************************************************/
SELECT DISTINCT
	c.CD_CARTEIRA,
	c.NO_CARTEIRA	
FROM 
	Quality_Control..RESULTADO_ENQUADRAMENTO re
	 INNER JOIN Quality_Control..TIPO_FILTRO tf ON 
	  re.CD_SUBTIPO_FILTRO = tf.CD_TIPO_FILTRO
	 INNER JOIN WM_DB..ATIVOS a ON 
	  re.CD_ATIVO = a.CD_ATIVO AND 
	  re.CD_TIPO_ATIVO = a.CD_TIPO_ATIVO
	 INNER JOIN WM_DB..CARTEIRA c ON 
	  re.CD_CARTEIRA = c.CD_CARTEIRA
WHERE 
	re.DT_RESULTADO          = '2013-09-30'
	AND re.CD_SUBTIPO_FILTRO = 16
	AND (re.CD_ATIVO	     = 'BBDC4')
   	AND (re.CD_TIPO_ATIVO    = 'RV')
ORDER BY c.CD_CARTEIRA