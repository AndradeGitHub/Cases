﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ubs.brasil.qualitycontrol.comum.globals;
using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.core
{
    public class LimiteMensal : Operacao<LimitePerfilRisco>
    {
        private static dynamic repositorioFabrica;
        private static dynamic fabricaLogOperacao; 

        public LimiteMensal(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            fabricaLogOperacao = OperacaoFabrica.CriarOperacaoLog<OperacaoLog>(repositorioModeloQC, repositorioModeloWM_DB);

            repositorioFabrica = RepositorioFabrica.CriarRepositorio<LimitePerfilRisco, RepositorioLimiteMensal>(repositorioModeloQC, repositorioModeloWM_DB);                                                       
        }

        public override int Gravar(List<LimitePerfilRisco> limitePerfilRisco)
        {
            int ret = repositorioFabrica.Gravar(limitePerfilRisco);

            if (ret > 0)
                GravarLogOperacao(limitePerfilRisco, "Inclusão");

            return ret;
        }

        public override int Alterar(List<LimitePerfilRisco> limitePerfilRisco)
        {
            int ret = repositorioFabrica.Alterar(limitePerfilRisco);

            if (ret > 0)
                GravarLogOperacao(limitePerfilRisco, "Alteração");

            return ret;
        }

        public override int Apagar(List<LimitePerfilRisco> limitePerfilRisco)
        {
            int ret = repositorioFabrica.Apagar(limitePerfilRisco);

            if (ret > 0)
                GravarLogOperacao(limitePerfilRisco, "Exclusão");

            return ret;
        }

        private void GravarLogOperacao(List<LimitePerfilRisco> limitePerfilRisco, string acao)
        {
            List<LogOperacao> lstLogOperacao = new List<LogOperacao>();

            foreach (LimitePerfilRisco limite in limitePerfilRisco)
            {
                LogOperacao logOperacao = new LogOperacao();
                
                logOperacao.nomeTipoFuncionalidade = "Limite Mensal";
                logOperacao.nomeFuncionalidade = "Limite Mensal";
                logOperacao.acao = acao;
                logOperacao.dtLogOperacao = (DateTime)limite.dtAlteracao;
                logOperacao.codUsuario = limite.codUsuarioAlteracao;
                logOperacao.nomeTipoDescricao = "OK";

                logOperacao.txDescricao = string.Concat("Perfil Risco: ", limite.codPerfilRisco,
                                                        ", Tipo Filtro: ", Utils.GetEnumDescriptionTipoFiltro(limite.codSubTipoFiltro),
                                                        ", Valor Limite Min: ", limite.vlrLimiteMin.ToString(),
                                                        ", Valor Limite Max: ", limite.vlrLimiteMax.ToString(),
                                                        ", Mês/Ano: ", string.Format("{0}/{1}", limite.dtIniVigencia.Month, limite.dtIniVigencia.Year));                                                                        

                lstLogOperacao.Add(logOperacao);
            }            
            
            fabricaLogOperacao.Gravar(lstLogOperacao);     
        }        
    }
}