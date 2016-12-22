USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_RENTABILIDADECARTEIRA]    Script Date: 08/26/2013 14:15:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_WMDB_CARGA_RENTABILIDADECARTEIRA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_WMDB_CARGA_RENTABILIDADECARTEIRA]
GO

USE [WM_DB]
GO

/****** Object:  StoredProcedure [dbo].[SP_WMDB_CARGA_RENTABILIDADECARTEIRA]    Script Date: 08/26/2013 14:15:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SP_WMDB_CARGA_RENTABILIDADECARTEIRA] (@dt_inicio smalldatetime = null, @LISTA_CD_CARTEIRA VARCHAR(MAX) = null)
As
set nocount on 

-- SP_WMDB_CARGA_RENTABILIDADECARTEIRA @dt_inicio = '2013-01-01'

declare @table table
(
	CD varchar(15)
)

declare @table_ult_data table 
(
	CD_CARTEIRA VARCHAR(15),
	DT_INICIO SMALLDATETIME,
	DT_FIM SMALLDATETIME
)

INSERT INTO @table_ult_data
select distinct CD_CARTEIRA, 
       DT_INICIO, 
       DT_FIM 
	  from LOG_PROCESSAMENTO_CARTEIRA z 
    where DT_FIM_PROC = (select MAX(DT_FIM_PROC) 
						 from LOG_PROCESSAMENTO_CARTEIRA 
					   where CD_CARTEIRA = z.CD_CARTEIRA)

if not @dt_inicio is null
	begin
		update @table_ult_data set DT_INICIO = @dt_inicio, DT_FIM = @dt_inicio
		
		delete from @table_ult_data where CD_CARTEIRA in (select codcli from ymf_sac..RV_DATA_PROC where CL < @dt_inicio)		
	end 
	
IF NOT @LISTA_CD_CARTEIRA IS NULL
BEGIN
	INSERT INTO @table (CD) 
	SELECT DISTINCT STRVAL FROM WM_DB.[dbo].[Split](@LISTA_CD_CARTEIRA, ',')			
END
  
if @dt_inicio is null
	begin
		DELETE z  FROM [WM_DB].[dbo].rentabilidade_carteira z
			Inner Join (Select MIN(dt_inicial)'dt_inicio', MAX(dt_final) 'dt_fim', a.cd_carteira
							From WM_V_UBS_LOG_PROC_CARTEIRA a
							Inner Join log_processamento_carteira b
								on b.cd_carteira = a.cd_carteira
						Where a.DT > b.dt_carteira
						Group by a.cd_carteira) y
				on y.cd_carteira = z.CD_CARTEIRA
		where z.DT_REFERENCIA >= y.dt_inicio
		  and (z.CD_CARTEIRA in (select CD from @table) or @LISTA_CD_CARTEIRA is null)	
	end 	
else
	begin
		DELETE FROM [WM_DB].[dbo].rentabilidade_carteira
		WHERE (CD_CARTEIRA in (select CD from @table) or @LISTA_CD_CARTEIRA is null)	
		  AND DT_REFERENCIA >= @dt_inicio 
	end   	
	
/* Diario */
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.cd_carteira, g.DT_FIM, 1, ((d.vl_cota / b.vl_cota) - 1) *100
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira  
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira 
 Where d.dt_ref = g.DT_FIM
   and b.dt_ref = (select MAX(dt_cota) from CARTEIRA_COTA z where z.cd_carteira = b.cd_carteira and DT_COTA < g.DT_FIM)
   and b.vl_cota > 0	      

/* Mensal */
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.cd_carteira, g.DT_FIM, 2, ((d.vl_cota / b.vl_cota) - 1) *100
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira 		
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira
Where b.dt_ref = (select MAX(dt_cota) from CARTEIRA_COTA z where z.CD_CARTEIRA = b.CD_CARTEIRA and year(dt_cota) = year(DATEADD(month, -1, g.DT_FIM)) and MONTH(dt_cota) = month(DATEADD(month, -1, g.DT_FIM)))
  and d.dt_ref = g.DT_FIM
  and b.vl_cota > 0          
    
/* Ano */  
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)   
select b.cd_carteira, g.DT_FIM, 3, ((d.vl_cota / b.vl_cota) - 1) *100
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira 		
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira  
where b.dt_ref = (select MAX(dt_cota) from CARTEIRA_COTA z where z.CD_CARTEIRA = b.CD_CARTEIRA and year(dt_cota) = year(DATEADD(year, -1, g.DT_FIM)) and MONTH(dt_cota) = 12)
  and d.dt_ref = g.DT_FIM
  and b.vl_cota > 0          
    
/* 3 Meses */  
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)
select b.cd_carteira, g.DT_FIM, 4, ((d.vl_cota / b.vl_cota) - 1) *100 
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira 			
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira  
Where b.dt_ref = (select MAX(dt_cota) from CARTEIRA_COTA z where z.CD_CARTEIRA = b.CD_CARTEIRA and year(dt_cota) = year(DATEADD(month, -3, g.DT_FIM)) and MONTH(dt_cota) = month(DATEADD(month, -3, g.DT_FIM)))
  and d.dt_ref = g.DT_FIM
  and b.vl_cota > 0       
  
/* 6 Meses */  
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.cd_carteira, g.DT_FIM, 5, ((d.vl_cota / b.vl_cota) - 1) *100
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira 	    
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira   
where b.dt_ref = (select MAX(dt_cota) from CARTEIRA_COTA z where z.CD_CARTEIRA = b.CD_CARTEIRA and year(dt_cota) = year(DATEADD(month, -6, g.DT_FIM)) and MONTH(dt_cota) = month(DATEADD(month, -6, g.DT_FIM)))
  and d.dt_ref = g.DT_FIM
  and b.vl_cota > 0        
    
/* 12 Meses */  
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.cd_carteira, g.DT_FIM, 6, ((d.vl_cota / b.vl_cota) - 1) *100 
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira 	    	
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira          
Where b.dt_ref = (select MAX(dt_cota) from CARTEIRA_COTA z where z.CD_CARTEIRA = b.CD_CARTEIRA and year(dt_cota) = year(DATEADD(month, -12, g.DT_FIM)) and MONTH(dt_cota) = month(DATEADD(month, -12, g.DT_FIM)))
  and d.dt_ref = g.DT_FIM
  and b.vl_cota > 0       
    
/* 24 Meses */  
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)  
select b.cd_carteira, g.DT_FIM, 7, ((d.vl_cota / b.vl_cota) - 1) *100 
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira	
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira          
Where  b.dt_ref = (select MAX(dt_cota) from CARTEIRA_COTA z where z.CD_CARTEIRA = b.CD_CARTEIRA and year(dt_cota) = year(DATEADD(month, -24, g.DT_FIM)) and MONTH(dt_cota) = month(DATEADD(month, -24, g.DT_FIM)))
  and d.dt_ref = g.DT_FIM
  and b.vl_cota > 0    
        
/* 36 Meses */  
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.cd_carteira, g.DT_FIM, 8, ((d.vl_cota / b.vl_cota) - 1) *100 
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira		
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira  
Where b.dt_ref = (select MAX(dt_cota) from CARTEIRA_COTA z where z.CD_CARTEIRA = b.CD_CARTEIRA and year(dt_cota) = year(DATEADD(month, -36, g.DT_FIM)) and MONTH(dt_cota) = month(DATEADD(month, -36, g.DT_FIM)))
  and d.dt_ref = g.DT_FIM
  and b.vl_cota > 0    
  
/* Desdeo Inicio */  
insert into WM_DB..rentabilidade_carteira
(CD_CARTEIRA, DT_REFERENCIA, CD_TIPO_PERIODO, VL_RENT)        
select b.cd_carteira, g.DT_FIM, 9, ((d.vl_cota / b.vl_cota) - 1) * 100
From WM_V_UBS_CARTEIRA_COTA b
Inner Join WM_V_UBS_CARTEIRA_COTA d
	on d.cd_carteira = b.cd_carteira	
Inner Join wm_db..Carteira cart
	on cart.cd_carteira = b.cd_carteira
Inner Join @table_ult_data G
	on G.CD_CARTEIRA = b.cd_carteira		
Inner Join WM_V_UBS_CLIENTE e
    on e.cd_carteira =  b.cd_carteira          
where b.dt_ref = cart.dt_abertura
  and d.dt_ref = g.DT_FIM
  and b.vl_cota > 0          
  
GO

