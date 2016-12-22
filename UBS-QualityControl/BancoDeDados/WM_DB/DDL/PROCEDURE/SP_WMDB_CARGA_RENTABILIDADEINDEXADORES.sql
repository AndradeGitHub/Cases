USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_RENTABILIDADEINDEXADORES]    Script Date: 08/26/2013 14:16:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WMDB_CARGA_RENTABILIDADEINDEXADORES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WMDB_CARGA_RENTABILIDADEINDEXADORES]
GO

USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_RENTABILIDADEINDEXADORES]    Script Date: 08/26/2013 14:16:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SP_WMDB_CARGA_RENTABILIDADEINDEXADORES] (@data smalldatetime)
As
set nocount on 
/*
declare @data smalldatetime
select @data = '2013-03-15 00:00:00.000'
*/
declare @table_periodo table 
(
	dt_ini_1 smalldatetime,
	dt_ini_2 smalldatetime,
	dt_ini_3 smalldatetime,
	dt_ini_4 smalldatetime,
	dt_ini_5 smalldatetime,
	dt_ini_6 smalldatetime,	
	dt_ini_7 smalldatetime,
	dt_ini_8 smalldatetime
)

insert into @table_periodo
select  dbo.fn_dt_inicio_rentabilidade(@data, 1),
        dbo.fn_dt_inicio_rentabilidade(@data, 2),
        dbo.fn_dt_inicio_rentabilidade(@data, 3),
        dbo.fn_dt_inicio_rentabilidade(@data, 4),
        dbo.fn_dt_inicio_rentabilidade(@data, 5),
        dbo.fn_dt_inicio_rentabilidade(@data, 6),
        dbo.fn_dt_inicio_rentabilidade(@data, 7),
        dbo.fn_dt_inicio_rentabilidade(@data, 8)
        
--Select * From @table_periodo        
--Select * FROM YMF_SAC..SAC_CL_RENTAB where clcli_cd = '01177-0' and dt_pos = '2013-03-15'        

delete from WM_DB..RENTABILIDADE_BENCHMARK where DT_REFERENCIA = @data
/* Diario */
INSERT INTO WM_DB..RENTABILIDADE_BENCHMARK
(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.CD_INDEXADOR, @data, 1, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
From WM_V_UBS_INDEXADOR_PRECO b
Inner Join WM_V_UBS_INDEXADOR_PRECO d
	on d.CD_INDEXADOR = b.CD_INDEXADOR	
Inner Join @table_periodo c
    on b.DT_REF = c.dt_ini_1 
  and d.DT_REF = @data
  and b.VL_PRECO > 0
Inner Join WM_V_UBS_INDEXADOR e
    on e.CD_INDEXADOR =  b.CD_INDEXADOR  


/* Mensal */
insert into WM_DB..RENTABILIDADE_BENCHMARK
(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.CD_INDEXADOR, @data, 2, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
From WM_V_UBS_INDEXADOR_PRECO b
Inner Join WM_V_UBS_INDEXADOR_PRECO d
	on d.CD_INDEXADOR = b.CD_INDEXADOR	
Inner Join @table_periodo c
    on b.dt_ref = c.dt_ini_2
  and d.dt_ref = @data
  and b.VL_PRECO > 0      
Inner Join WM_V_UBS_INDEXADOR e
    on e.CD_INDEXADOR =  b.CD_INDEXADOR
    
/* Ano */  
insert into WM_DB..RENTABILIDADE_BENCHMARK
(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.CD_INDEXADOR, @data, 3, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
From WM_V_UBS_INDEXADOR_PRECO b
Inner Join WM_V_UBS_INDEXADOR_PRECO d
	on d.CD_INDEXADOR = b.CD_INDEXADOR	
Inner Join @table_periodo c
 on b.dt_ref = c.dt_ini_3
  and d.dt_ref = @data
  and b.VL_PRECO > 0      
Inner Join WM_V_UBS_INDEXADOR e
    on e.CD_INDEXADOR =  b.CD_INDEXADOR  
    
/* 3 Meses */  
insert into WM_DB..RENTABILIDADE_BENCHMARK
(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.CD_INDEXADOR, @data, 4, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
From WM_V_UBS_INDEXADOR_PRECO b
Inner Join WM_V_UBS_INDEXADOR_PRECO d
	on d.CD_INDEXADOR = b.CD_INDEXADOR	
Inner Join @table_periodo c
 on b.dt_ref = c.dt_ini_4
  and d.dt_ref = @data
  and b.VL_PRECO > 0      
Inner Join WM_V_UBS_INDEXADOR e
    on e.CD_INDEXADOR =  b.CD_INDEXADOR  
    
/* 6 Meses */  
insert into WM_DB..RENTABILIDADE_BENCHMARK
(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.CD_INDEXADOR, @data, 5, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
From WM_V_UBS_INDEXADOR_PRECO b
Inner Join WM_V_UBS_INDEXADOR_PRECO d
	on d.CD_INDEXADOR = b.CD_INDEXADOR	
Inner Join @table_periodo c
 on b.dt_ref = c.dt_ini_5
  and d.dt_ref = @data
  and b.VL_PRECO > 0      
Inner Join WM_V_UBS_INDEXADOR e
    on e.CD_INDEXADOR =  b.CD_INDEXADOR      
    
/* 12 Meses */  
insert into WM_DB..RENTABILIDADE_BENCHMARK
(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.CD_INDEXADOR, @data, 6, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
From WM_V_UBS_INDEXADOR_PRECO b
Inner Join WM_V_UBS_INDEXADOR_PRECO d
	on d.CD_INDEXADOR = b.CD_INDEXADOR	
Inner Join @table_periodo c
 on b.dt_ref = c.dt_ini_6
  and d.dt_ref = @data
  and b.VL_PRECO > 0      
Inner Join WM_V_UBS_INDEXADOR e
    on e.CD_INDEXADOR =  b.CD_INDEXADOR          
    
/* 24 Meses */  
insert into WM_DB..RENTABILIDADE_BENCHMARK
(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.CD_INDEXADOR, @data, 7, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
From WM_V_UBS_INDEXADOR_PRECO b
Inner Join WM_V_UBS_INDEXADOR_PRECO d
	on d.CD_INDEXADOR = b.CD_INDEXADOR	
Inner Join @table_periodo c
 on b.dt_ref = c.dt_ini_7
  and d.dt_ref = @data
  and b.VL_PRECO > 0      
Inner Join WM_V_UBS_INDEXADOR e
    on e.CD_INDEXADOR =  b.CD_INDEXADOR          
    
/* 36 Meses */  
insert into WM_DB..RENTABILIDADE_BENCHMARK
(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.CD_INDEXADOR, @data, 8, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
From WM_V_UBS_INDEXADOR_PRECO b
Inner Join WM_V_UBS_INDEXADOR_PRECO d
	on d.CD_INDEXADOR = b.CD_INDEXADOR	
Inner Join @table_periodo c
 on b.dt_ref = c.dt_ini_8
  and d.dt_ref = @data
  and b.VL_PRECO > 0      
Inner Join WM_V_UBS_INDEXADOR e
    on e.CD_INDEXADOR =  b.CD_INDEXADOR  
    
/* Desdeo Inicio */  
--insert into RENTABILIDADE_BENCHMARK
--(CD_INDEXADOR, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
--select b.CD_INDEXADOR, @data, 9, ((d.VL_PRECO / b.VL_PRECO) - 1) *100 
--From WM_V_UBS_INDEXADOR_PRECO b
--Inner Join WM_V_UBS_INDEXADOR_PRECO d
--	on d.CD_INDEXADOR = b.CD_INDEXADOR	
--Inner Join INDEXADORES cart
--	on cart.CD_INDEXADOR = b.CD_INDEXADOR collate SQL_Latin1_General_CP1_CI_AS
--Inner Join @table_periodo c
-- on b.dt_ref = c.dt_ini_8
--  and d.dt_ref = cart.dt_abertura
--  and b.VL_PRECO > 0      
--Inner Join WM_V_UBS_INDEXADOR e
--    on e.CD_INDEXADOR =  b.CD_INDEXADOR         
/*
Select * FROM YMF_SAC..SAC_CL_RENTAB where clcli_cd = '01177-0' and dt_pos = '2013-03-15'

declare @data smalldatetime

select @data = '2013-03-15 00:00:00.000'
  
Select a.*, b.no_tipo_periodo, c.dt_abertura 
	From rentabilidade_carteira a
	inner join tipo_periodo_rentabilidade b
		on b.cd_tipo_periodo = a.cd_tipo_periodo
    inner join carteira c
		on c.cd_carteira = a.cd_carteira
where a.cd_carteira = '01177-0'
-- delete from rentabilidade_carteira  

Select * from dw_WM..tb232 where cod_cli = 'Cliente_01177-0' and dt_ref = @data
*/


GO

