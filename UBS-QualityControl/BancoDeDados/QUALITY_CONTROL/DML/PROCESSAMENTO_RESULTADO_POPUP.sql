DECLARE @DT_REF DATETIME 
SELECT @DT_REF = '2013-09-30' 

SELECT 	
	re.CD_ATIVO, 
	CASE re.CD_SUBTIPO_FILTRO WHEN 5 THEN a.NO_ATIVO WHEN 8 THEN a.NO_ATIVO ELSE re.CD_ATIVO END AS NM_ATIVO,
	COUNT(DISTINCT re.CD_CARTEIRA) AS QTD_CARTEIRAS,
	SUM(po.VL_SALDO_BRUTO ) AS VL_POSICAO_ATIVO,
	SUM(cc.VL_PATRIMONIO ) AS VL_PATRIMONIO,
	re.IN_LIBERADO
FROM 
	Quality_Control..RESULTADO_ENQUADRAMENTO re
	  INNER JOIN Quality_Control..TIPO_FILTRO tf ON 
	    re.CD_SUBTIPO_FILTRO = tf.CD_TIPO_FILTRO
	  INNER JOIN Quality_Control..PROCESSAMENTO p ON 
		re.CD_PROCESSAMENTO = p.CD_PROCESSAMENTO
	  LEFT JOIN WM_DB..ATIVOS a ON 
	    re.CD_ATIVO = a.CD_ATIVO AND 
	    re.CD_TIPO_ATIVO = a.CD_TIPO_ATIVO	  
	  LEFT JOIN WM_DB..CARTEIRA_COTA cc ON 
	    re.CD_CARTEIRA = cc.CD_CARTEIRA AND 
	    cc.DT_COTA = re.DT_POSICAO
	  LEFT JOIN  WM_DB..POSICAO po ON
	    re.CD_CARTEIRA = po.CD_CARTEIRA AND
	    re.DT_POSICAO = po.DT_POSICAO AND
	    re.CD_ATIVO = po.CD_ATIVO AND
	    re.CD_TIPO_ATIVO = po.CD_TIPO_ATIVO
WHERE 
	re.DT_RESULTADO = @DT_REF
	AND	re.CD_SUBTIPO_FILTRO = 16
	AND IN_ENQUADRADO = 'N'
	AND IN_LIBERADO = 'N' 
	AND p.CD_PROCESSAMENTO = (SELECT MAX(CD_PROCESSAMENTO) FROM Quality_Control..PROCESSAMENTO WHERE DT_REFERENCIA = @DT_REF AND IN_FINALIZADO = 'S')
	AND p.IN_FINALIZADO = 'S'
GROUP BY 	
	re.CD_ATIVO,
	re.CD_TIPO_ATIVO, 
	a.NO_ATIVO, 
	re.CD_SUBTIPO_FILTRO, 
	re.IN_LIBERADO
	
UNION
	
SELECT 	
	re.CD_ATIVO, 
	tf.NO_TIPO_FILTRO AS NM_ATIVO,
	COUNT(DISTINCT re.CD_CARTEIRA) AS QTD_CARTEIRAS,
	SUM(po.VL_SALDO_BRUTO ) AS VL_POSICAO_ATIVO,
	SUM(cc.VL_PATRIMONIO ) AS VL_PATRIMONIO,
	re.IN_LIBERADO
FROM 
	Quality_Control..RESULTADO_ENQUADRAMENTO re
	  INNER JOIN Quality_Control..TIPO_FILTRO tf ON 
	    re.CD_SUBTIPO_FILTRO = tf.CD_TIPO_FILTRO
	  INNER JOIN Quality_Control..PROCESSAMENTO p ON 
		re.CD_PROCESSAMENTO = p.CD_PROCESSAMENTO
	  LEFT JOIN WM_DB..CARTEIRA_COTA cc ON 
	    re.CD_CARTEIRA = cc.CD_CARTEIRA AND 
	    cc.DT_COTA = re.DT_POSICAO
	  LEFT JOIN  WM_DB..POSICAO po ON
	    re.CD_CARTEIRA = po.CD_CARTEIRA AND
	    re.DT_POSICAO = po.DT_POSICAO AND
	    re.CD_ATIVO = po.CD_ATIVO AND
	    re.CD_TIPO_ATIVO = po.CD_TIPO_ATIVO	    
WHERE 
	re.DT_RESULTADO = @DT_REF
	AND	re.CD_SUBTIPO_FILTRO = 16
	AND IN_ENQUADRADO = 'N'
	AND IN_LIBERADO = 'N'
	AND re.CD_SUBTIPO_FILTRO IN (11,13,14,15)
	AND p.CD_PROCESSAMENTO = (SELECT MAX(CD_PROCESSAMENTO) FROM Quality_Control..PROCESSAMENTO WHERE DT_REFERENCIA = @DT_REF AND IN_FINALIZADO = 'S')
	AND p.IN_FINALIZADO = 'S'	
GROUP BY 	
	re.CD_ATIVO,
	re.CD_TIPO_ATIVO, 
	re.CD_SUBTIPO_FILTRO, 
	tf.NO_TIPO_FILTRO, 
	re.IN_LIBERADO
ORDER BY	
	re.CD_ATIVO