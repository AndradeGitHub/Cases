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
    public class RepositorioModeloWM_DB : DbContext
    {
        public DbSet<PerfilDeRisco> PerfilRisco { get; set; }
        public DbSet<Carteira> Carteira { get; set; }
        public DbSet<CarteiraCota> CarteiraCota { get; set; }
        public DbSet<Ativo> Ativo { get; set; }
        public DbSet<LogOperacao> LogOperacao { get; set; }
        public DbSet<PosicaoDB> Posicao { get; set; }
        public DbSet<CargaDB> Carga { get; set; }    
        public DbSet<LogCargaDB> LogCarga { get; set; }    

        public RepositorioModeloWM_DB()
        {
            var adapterWM_DB = (IObjectContextAdapter)this;
            var objectContextWM_DB = adapterWM_DB.ObjectContext;
            objectContextWM_DB.CommandTimeout = 5000; //seconds            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);

            #region PerfilDeRisco
            modelBuilder.Entity<PerfilDeRisco>().Property(dp => dp.codPerfilRisco).HasColumnName("CD_PERFIL_RISCO").IsRequired();
            modelBuilder.Entity<PerfilDeRisco>().Property(dp => dp.nomePerfilRisco).HasColumnName("NO_PERFIL_RISCO").IsRequired();
            modelBuilder.Entity<PerfilDeRisco>().Property(dp => dp.inAtivoInativo).HasColumnName("IN_ATIVO_INATIVO").IsOptional();
            modelBuilder.Entity<PerfilDeRisco>().ToTable("PERFIL_RISCO");
            #endregion

            #region Carteira
            modelBuilder.Entity<Carteira>().Property(dp => dp.codCarteira).HasColumnName("CD_CARTEIRA").IsRequired();
            modelBuilder.Entity<Carteira>().Property(dp => dp.nomeCarteira).HasColumnName("NO_CARTEIRA").IsRequired();
            modelBuilder.Entity<Carteira>().Property(dp => dp.nuCGC).HasColumnName("NU_CGC").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.inAtivoInativo).HasColumnName("IN_ATIVO_INATIVO").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.codCliente).HasColumnName("CD_CLIENTE").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.dtAbertura).HasColumnName("DT_ABERTURA").IsRequired();
            modelBuilder.Entity<Carteira>().Property(dp => dp.dtEncerramento).HasColumnName("DT_ENCERRAMENTO").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.codPerfilRisco).HasColumnName("CD_PERFIL_RISCO").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.codAdministrador).HasColumnName("CD_ADMINISTRADOR").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.codCustodiante).HasColumnName("CD_CUSTODIANTE").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.codGestor).HasColumnName("CD_GESTOR").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.codIndex).HasColumnName("CD_INDEX").IsOptional();
            modelBuilder.Entity<Carteira>().Property(dp => dp.codUsuarioCA).HasColumnName("CD_USUARIO_CA").IsOptional();
            modelBuilder.Entity<Carteira>().ToTable("CARTEIRA");
            #endregion

            #region CarteiraCota
            modelBuilder.Entity<CarteiraCota>().Property(dp => dp.codCarteira).HasColumnName("CD_CARTEIRA").IsRequired();
            modelBuilder.Entity<CarteiraCota>().Property(dp => dp.dtCota).HasColumnName("DT_COTA").IsRequired();
            modelBuilder.Entity<CarteiraCota>().Property(dp => dp.qtCota).HasColumnName("QT_COTA").IsOptional();
            modelBuilder.Entity<CarteiraCota>().Property(dp => dp.vlCota).HasColumnName("VL_COTA").IsOptional();
            modelBuilder.Entity<CarteiraCota>().Property(dp => dp.vlPatrimonio).HasColumnName("VL_PATRIMONIO").IsOptional();
            modelBuilder.Entity<CarteiraCota>().ToTable("CARTEIRA_COTA");
            #endregion

            #region Ativo
            modelBuilder.Entity<Ativo>().Property(dp => dp.codAtivo).HasColumnName("CD_ATIVO").IsRequired();
            modelBuilder.Entity<Ativo>().Property(dp => dp.codTipoAtivo).HasColumnName("CD_TIPO_ATIVO").IsRequired();
            modelBuilder.Entity<Ativo>().Property(dp => dp.nomeAtivo).HasColumnName("NO_ATIVO").IsOptional();
            modelBuilder.Entity<Ativo>().Property(dp => dp.codClasseAtivo).HasColumnName("CD_CLASSE_ATIVO").IsOptional();
            modelBuilder.Entity<Ativo>().Property(dp => dp.codIsin).HasColumnName("CD_ISIN").IsOptional();
            modelBuilder.Entity<Ativo>().ToTable("ATIVOS");
            #endregion

            #region LogOperação            
            modelBuilder.Entity<LogOperacao>().Property(dp => dp.nomeSistema).HasColumnName("NO_SISTEMA").IsRequired();
            modelBuilder.Entity<LogOperacao>().Property(dp => dp.nomeFuncionalidade).HasColumnName("NO_FUNCIONALIDADE").IsRequired();
            modelBuilder.Entity<LogOperacao>().Property(dp => dp.nomeTipoFuncionalidade).HasColumnName("NO_TIPO_FUNCIONALIDADE").IsRequired();
            modelBuilder.Entity<LogOperacao>().Property(dp => dp.acao).HasColumnName("NO_ACAO").IsRequired();
            modelBuilder.Entity<LogOperacao>().Property(dp => dp.nomeTipoDescricao).HasColumnName("NO_TIPO_DESCRICAO").IsRequired();
            modelBuilder.Entity<LogOperacao>().Property(dp => dp.txDescricao).HasColumnName("TX_DESCRICAO").IsRequired();
            modelBuilder.Entity<LogOperacao>().Property(dp => dp.dtLogOperacao).HasColumnName("DT_LOG").IsRequired();
            modelBuilder.Entity<LogOperacao>().Property(dp => dp.codUsuario).HasColumnName("CD_USUARIO").IsRequired();
            modelBuilder.Entity<LogOperacao>().ToTable("LOG_OPERACAO");
            #endregion

            #region Posição
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.dtPosicao).HasColumnName("DT_POSICAO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.codCarteira).HasColumnName("CD_CARTEIRA").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.codAtivo).HasColumnName("CD_ATIVO").IsOptional();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.codTipoAtivo).HasColumnName("CD_TIPO_ATIVO").IsOptional();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.dtAquisicao).HasColumnName("DT_AQUISICAO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.puAquisicao).HasColumnName("PU_AQUISICAO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.vlrSaldoBruto).HasColumnName("VL_SALDO_BRUTO").IsOptional();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.puAtual).HasColumnName("PU_ATUAL").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.vlrIRRF).HasColumnName("VL_IRRF").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.vlrIOF).HasColumnName("VL_IOF").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.qtDisp).HasColumnName("QT_DISP").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.qtBloqueada).HasColumnName("QT_BLOQUEADA").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.vlrSaldoLiquido).HasColumnName("VL_SALDO_LIQUIDO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.dtLiquidacao).HasColumnName("DT_LIQUIDACAO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.descHistorico).HasColumnName("DS_HISTORICO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.codYMFLegado).HasColumnName("COD_YMF_LEGADO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.vlrTaxa).HasColumnName("VL_TAXA").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.codInterno).HasColumnName("CD_INTERNO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.codClearing).HasColumnName("CD_CLEARING").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.vlrAtivo).HasColumnName("VL_ATIVO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().Property(dp => dp.vlrPassivo).HasColumnName("VL_PASSIVO").IsRequired();
            modelBuilder.Entity<PosicaoDB>().ToTable("POSICAO");
            #endregion

            #region Carga
            modelBuilder.Entity<CargaDB>().Property(dp => dp.codCarga).HasColumnName("CD_CARGA").IsRequired();
            modelBuilder.Entity<CargaDB>().Property(dp => dp.dtReferencia).HasColumnName("DT_REFERENCIA").IsRequired();
            modelBuilder.Entity<CargaDB>().Property(dp => dp.inTipoProcessamento).HasColumnName("IN_TIPO_PROCESSAMENTO").IsRequired();
            modelBuilder.Entity<CargaDB>().Property(dp => dp.dtExecucaoIni).HasColumnName("DT_EXECUCAO_INI").IsRequired();
            modelBuilder.Entity<CargaDB>().Property(dp => dp.dtExecucaoFim).HasColumnName("DT_EXECUCAO_FIM").IsOptional();
            modelBuilder.Entity<CargaDB>().Property(dp => dp.codUsuarioResponsavel).HasColumnName("CD_USUARIO_RESPONSAVEL").IsOptional();
            modelBuilder.Entity<CargaDB>().Property(dp => dp.inFinalizado).HasColumnName("IN_FINALIZADO").IsRequired();
            modelBuilder.Entity<CargaDB>().ToTable("CARGA");
            #endregion

            #region LogCarga
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.codLog).HasColumnName("CD_LOG").IsRequired();
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.codCarga).HasColumnName("CD_CARGA").IsRequired();
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.dtCarga).HasColumnName("DT_CARGA").IsOptional();
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.codCarteira).HasColumnName("CD_CARTEIRA").IsOptional();
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.codOrdem).HasColumnName("CD_ORDEM").IsOptional();
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.dsDescricao).HasColumnName("DS_DESCRICAO").IsOptional();
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.dtLog).HasColumnName("DT_LOG").IsRequired();
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.codUsuarioResponsavel).HasColumnName("CD_USUARIO_RESPONSAVEL").IsOptional();
            modelBuilder.Entity<LogCargaDB>().Property(dp => dp.inTipoMensagem).HasColumnName("IN_TIPO_MENSAGEM").IsOptional();
            modelBuilder.Entity<LogCargaDB>().ToTable("LOG_CARGA");
            #endregion
        }
    }
}