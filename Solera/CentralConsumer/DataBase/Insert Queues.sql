SELECT * FROM QUEUE

--LOCAL
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentralBradescoPedido_LOCAL', 'ExchangeAxPedidoCentralBradesco_LOCAL', 1, 'Bradesco', 1)
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentraBradescoConfirmacao_LOCAL', 'ExchangeAxPedidoCentralBradesco_LOCAL', 2, 'Bradesco', 1)
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentraBradescoAutorizacao_LOCAL', 'ExchangeAxPedidoCentralBradesco_LOCAL', 3, 'Bradesco', 1)

--DEV
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentralBradescoPedido_DEV', 'ExchangeAxPedidoCentralBradesco_DEV', 1, 'Bradesco', 1)
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentraBradescoConfirmacao_DEV', 'ExchangeAxPedidoCentralBradesco_DEV', 2, 'Bradesco', 1)
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentraBradescoAutorizacao_DEV', 'ExchangeAxPedidoCentralBradesco_DEV', 3, 'Bradesco', 1)

--HOM
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentralBradescoPedido_HOM', 'ExchangeAxPedidoCentralBradesco_HOM', 1, 'Bradesco', 1)
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentraBradescoConfirmacao_HOM', 'ExchangeAxPedidoCentralBradesco_HOM', 2, 'Bradesco', 1)
--INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('QueueAxPedidoCentraBradescoAutorizacao_HOM', 'ExchangeAxPedidoCentralBradesco_HOM', 3, 'Bradesco', 1)

--PROD
  --BRADESCO 
    --INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('"QueueAxPedidoCentralBradescoPedido"', 'ExchangeAxPedidoCentralBradesco', 1, 'Bradesco', 1)
    --INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('"QueueAxPedidoCentraBradescoConfirmacao"', 'ExchangeAxPedidoCentralBradesco', 2, 'Bradesco', 1)
    --INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('"QueueAxPedidoCentraBradescoAutorizacao"', 'ExchangeAxPedidoCentralBradesco', 3, 'Bradesco', 1)
  --HDI
    --INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('"QueueAxPedidoCentralHDIPedido"', 'ExchangeAxPedidoCentralHDI', 1, 'HDI', 1)
    --INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('"QueueAxPedidoCentraHDIConfirmacao"', 'ExchangeAxPedidoCentralHDI', 2, 'HDI', 1)
    --INSERT INTO QUEUE (Nome, Exchange, Operacao, Cliente, Status) VALUES ('"QueueAxPedidoCentraHDIAutorizacao"', 'ExchangeAxPedidoCentralHDI', 3, 'HDI', 1)