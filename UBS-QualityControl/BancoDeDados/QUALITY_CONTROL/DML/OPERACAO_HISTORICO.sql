USE [QUALITY_CONTROL]    
                        
SELECT
	*
FROM                        
	LOG_OPERACAO
WHERE
	CD_USUARIO = 1 AND
	DT_LOG = '20130717' AND
	NO_FUNCIONALIDADE = 'Limite Diário' AND
	NO_ACAO = 'Inclusão'