USE QUALITY_CONTROL

SELECT	LP.cd_carteira, 
		LP.dt_processada, 
		LP.dt_processamento, 
		TF1.no_tipo_filtro + ' - ' + TF2.no_tipo_filtro + ' - ' + LP.ds_descricao,		
		cd_usuario_responsavel,
		cd_log
FROM	
		log_processamento	LP
		 LEFT JOIN tipo_filtro TF1 ON LP.cd_tipo_filtro = TF1.cd_tipo_filtro
		 LEFT JOIN tipo_filtro	TF2 ON LP.cd_subtipo_filtro = TF2.cd_tipo_filtro
WHERE	
		LP.dt_processada = '08-may-13' 
ORDER BY 
	1, 3 DESC	 