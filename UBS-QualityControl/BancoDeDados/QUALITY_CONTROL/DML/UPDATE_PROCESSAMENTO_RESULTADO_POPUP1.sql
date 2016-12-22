UPDATE 
	Quality_Control..RESULTADO_ENQUADRAMENTO SET IN_LIBERADO = 'S'
FROM 
	Quality_Control..RESULTADO_ENQUADRAMENTO re
	 INNER JOIN Quality_Control..TIPO_FILTRO tf 
	  ON re.CD_SUBTIPO_FILTRO = tf.CD_TIPO_FILTRO
	 INNER JOIN WM_DB..CARTEIRA_COTA cc 
	  ON re.CD_CARTEIRA = cc.CD_CARTEIRA AND cc.DT_COTA = re.DT_RESULTADO
	 LEFT JOIN WM_DB..ATIVOS a 
	  ON re.CD_ATIVO = a.CD_ATIVO AND re.CD_TIPO_ATIVO = a.CD_TIPO_ATIVO
	 INNER JOIN WM_DB..CARTEIRA c 
	  ON re.CD_CARTEIRA = c.CD_CARTEIRA
WHERE 
	re.DT_RESULTADO = '2013-05-09'
	AND re.CD_SUBTIPO_FILTRO = 7
	AND re.CD_ATIVO = 'BBDC3' 
	AND re.CD_TIPO_ATIVO = 'RV'
	
/**************************************************************************************/	
	
SELECT 
	* 
FROM 
	Quality_Control..RESULTADO_ENQUADRAMENTO 
WHERE 
	DT_RESULTADO = '20130208'
	AND CD_SUBTIPO_FILTRO = 1
	AND CD_ATIVO = 'BBDC3' 
--	AND CD_TIPO_ATIVO = 'RV'	