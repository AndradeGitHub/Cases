USE AXBASEAUDABRIDGE;  
GO 

/* BRADESCO */
DECLARE @ID INTEGER;

SELECT @ID = ID FROM SEGURADORA WHERE CNPJ = '33055146000193';

INSERT INTO [AxBaseAudabridge].[dbo].[Plugin] VALUES (1, 'T1BradescoPlugin', 1, @ID)
INSERT INTO [AxBaseAudabridge].[dbo].[Plugin] VALUES (2, 'T2BradescoPlugin', 1, @ID)
INSERT INTO [AxBaseAudabridge].[dbo].[Plugin] VALUES (3, 'T3BradescoPlugin', 1, @ID)
INSERT INTO [AxBaseAudabridge].[dbo].[Plugin] VALUES (4, 'T4BradescoPlugin', 1, @ID)
INSERT INTO [AxBaseAudabridge].[dbo].[Plugin] VALUES (5, 'T5BradescoPlugin', 1, @ID)
GO
