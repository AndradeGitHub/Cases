using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioModeloQC : DbContext
    {
        public RepositorioModeloQC()
        {            
            var adapterQC = (IObjectContextAdapter)this;
            var objectContextQC = adapterQC.ObjectContext;
            objectContextQC.CommandTimeout = 5000; //seconds
        }

        public DbSet<TipoDeFiltro> TipoFiltro { get; set; }
        public DbSet<LimitePerfilRisco> LimitePerfilRisco { get; set; }        
        public DbSet<LogProcessamentoDB> LogProcessamento { get; set; }
        public DbSet<EnquadramentoDB> ResultadoEnquadramento { get; set; }        
        public DbSet<ProcessamentoDB> ProcessamentoDB { get; set; }
        //public DbSet<UsuarioDB> Usuario { get; set; }          

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {                                    
            base.OnModelCreating(modelBuilder);

            #region LimitePerfilRisco
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.codLimitePerfilRisco).HasColumnName("CD_LIMITE_PERFIL_RISCO").IsRequired();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.codPerfilRisco).HasColumnName("CD_PERFIL_RISCO").IsRequired();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.dtIniVigencia).HasColumnName("DT_INI_VIGENCIA").IsRequired();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.dtFimVigencia).HasColumnName("DT_FIM_VIGENCIA").IsRequired();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.vlrLimiteMin).HasColumnName("VL_LIMITE_MIN").IsRequired();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.vlrLimiteMax).HasColumnName("VL_LIMITE_MAX").IsOptional();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.codTipoFiltro).HasColumnName("CD_TIPO_FILTRO").IsRequired();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.codSubTipoFiltro).HasColumnName("CD_SUBTIPO_FILTRO").IsRequired();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.dtAlteracao).HasColumnName("DT_ALTERACAO").IsOptional();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.codUsuarioAlteracao).HasColumnName("CD_USUARIO_ALTERACAO").IsOptional();            
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.inExcecao).HasColumnName("IN_EXCECAO").IsRequired();
            modelBuilder.Entity<LimitePerfilRisco>().Property(dp => dp.inDiarioMensal).HasColumnName("IN_DIARIO_MENSAL").IsRequired();            
            modelBuilder.Entity<LimitePerfilRisco>().ToTable("LIMITES_PERFIL_RISCO");
            #endregion

            #region TipoDeFiltro
            modelBuilder.Entity<TipoDeFiltro>().Property(dp => dp.codTipoFiltro).HasColumnName("CD_TIPO_FILTRO").IsRequired();
            modelBuilder.Entity<TipoDeFiltro>().Property(dp => dp.nomeTipoFiltro).HasColumnName("NO_TIPO_FILTRO").IsOptional();
            modelBuilder.Entity<TipoDeFiltro>().Property(dp => dp.nomeTipoFiltroAbrev).HasColumnName("NO_TIPO_FILTRO_ABR").IsOptional();
            modelBuilder.Entity<TipoDeFiltro>().Property(dp => dp.codTipoFiltroPai).HasColumnName("CD_TIPO_FILTRO_PAI").IsRequired();
            modelBuilder.Entity<TipoDeFiltro>().Property(dp => dp.inAtivoInativo).HasColumnName("IN_ATIVO_INATIVO").IsRequired();
            modelBuilder.Entity<TipoDeFiltro>().Property(dp => dp.dtAlteracao).HasColumnName("DT_ALTERACAO").IsOptional();
            modelBuilder.Entity<TipoDeFiltro>().Property(dp => dp.codUsuarioAlteracao).HasColumnName("CD_USUARIO_ALTERACAO").IsOptional();
            modelBuilder.Entity<TipoDeFiltro>().ToTable("TIPO_FILTRO");
            #endregion

            #region LogProcessamento
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.codLog).HasColumnName("CD_LOG").IsRequired();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.dtProcessada).HasColumnName("DT_PROCESSADA").IsRequired();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.codCarteira).HasColumnName("CD_CARTEIRA").IsRequired();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.codTipoFiltro).HasColumnName("CD_TIPO_FILTRO").IsOptional();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.codSubTipoFiltro).HasColumnName("CD_SUBTIPO_FILTRO").IsOptional();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.dsDescricao).HasColumnName("DS_DESCRICAO").IsOptional();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.dtProcessamento).HasColumnName("DT_PROCESSAMENTO").IsRequired();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.codUsuarioResponsavel).HasColumnName("CD_USUARIO_RESPONSAVEL").IsRequired();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.codProcessamento).HasColumnName("CD_PROCESSAMENTO").IsOptional();
            modelBuilder.Entity<LogProcessamentoDB>().Property(dp => dp.inTipoMensagem).HasColumnName("IN_TIPO_MENSAGEM").IsOptional();
            modelBuilder.Entity<LogProcessamentoDB>().ToTable("LOG_PROCESSAMENTO");
            #endregion

            #region Enquadramento
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.codResultado).HasColumnName("CD_RESULTADO").IsRequired();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.dtResultado).HasColumnName("DT_RESULTADO").IsRequired();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.codCarteira).HasColumnName("CD_CARTEIRA").IsRequired();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.codTipoFiltro).HasColumnName("CD_TIPO_FILTRO").IsRequired();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.codSubTipoFiltro).HasColumnName("CD_SUBTIPO_FILTRO").IsRequired();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.codAtivo).HasColumnName("CD_ATIVO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.codTipoAtivo).HasColumnName("CD_TIPO_ATIVO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.inEnquadrado).HasColumnName("IN_ENQUADRADO").IsRequired();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.vlrPontaA).HasColumnName("VL_PONTA_A").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.vlrPontaB).HasColumnName("VL_PONTA_B").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.dtAberturaCart).HasColumnName("DT_ABERTURA_CART").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.dtEncerramentoCart).HasColumnName("DT_ENCERRAMENTO_CART").IsOptional();            
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.dtAlteracao).HasColumnName("DT_ALTERACAO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.txDescricao).HasColumnName("TX_DESCRICAO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.codUsuarioAlteracao).HasColumnName("CD_USUARIO_ALTERACAO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.codProcessamento).HasColumnName("CD_PROCESSAMENTO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.inLiberado).HasColumnName("IN_LIBERADO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.dtLiberacao).HasColumnName("DT_LIBERACAO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().Property(dp => dp.dtPosicao).HasColumnName("DT_POSICAO").IsOptional();
            modelBuilder.Entity<EnquadramentoDB>().ToTable("RESULTADO_ENQUADRAMENTO");                
            #endregion

            #region ProcessamentoDB
            modelBuilder.Entity<ProcessamentoDB>().Property(dp => dp.codProcessamento).HasColumnName("CD_PROCESSAMENTO").IsRequired();
            modelBuilder.Entity<ProcessamentoDB>().Property(dp => dp.dtReferencia).HasColumnName("DT_REFERENCIA").IsRequired();
            modelBuilder.Entity<ProcessamentoDB>().Property(dp => dp.inPeriodoProcessamento).HasColumnName("IN_PERIODO_PROCESSAMENTO").IsRequired();
            modelBuilder.Entity<ProcessamentoDB>().Property(dp => dp.inTipoProcessamento).HasColumnName("IN_TIPO_PROCESSAMENTO").IsRequired();
            modelBuilder.Entity<ProcessamentoDB>().Property(dp => dp.dtExecucaoIni).HasColumnName("DT_EXECUCAO_INI").IsRequired();
            modelBuilder.Entity<ProcessamentoDB>().Property(dp => dp.dtExecucaoFim).HasColumnName("DT_EXECUCAO_FIM").IsOptional();
            modelBuilder.Entity<ProcessamentoDB>().Property(dp => dp.codUsuarioResponsavel).HasColumnName("CD_USUARIO_RESPONSAVEL").IsRequired();
            modelBuilder.Entity<ProcessamentoDB>().Property(dp => dp.inFinalizado).HasColumnName("IN_FINALIZADO").IsRequired();
            modelBuilder.Entity<ProcessamentoDB>().ToTable("PROCESSAMENTO");
            #endregion

            //modelBuilder.Entity<UsuarioDB>().Property(dp => dp.codUsuario).HasColumnName("CD_USUARIO").IsRequired();
            //modelBuilder.Entity<UsuarioDB>().Property(dp => dp.nomeLogin).HasColumnName("NO_LOGIN").IsOptional();
            //modelBuilder.Entity<UsuarioDB>().Property(dp => dp.dsSenha).HasColumnName("DS_SENHA").IsOptional();
            //modelBuilder.Entity<UsuarioDB>().Property(dp => dp.inAtivoInativo).HasColumnName("IN_ATIVO_INATIVO").IsOptional();            
            //modelBuilder.Entity<UsuarioDB>().ToTable("USUARIO");  
        }
    }
}