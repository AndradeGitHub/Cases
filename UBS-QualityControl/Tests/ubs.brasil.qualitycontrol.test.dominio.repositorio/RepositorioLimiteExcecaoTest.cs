﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.test.dominio.repositorio
{
    [TestClass]
    public class RepositorioLimiteExcecaoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private LimitePerfilRisco limitePerfilRisco;
        private List<LimitePerfilRisco> lstLimitePerfilRisco;
        private IRepositorio<LimitePerfilRisco> repositorioFabrica;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveGravarLimiteExcecao()
        {
            lstLimitePerfilRisco = new List<LimitePerfilRisco>();

            limitePerfilRisco = new LimitePerfilRisco();            
            limitePerfilRisco.codPerfilRisco = "AGGRESSI";
            limitePerfilRisco.dtIniVigencia = Convert.ToDateTime("2050-11-25");
            limitePerfilRisco.dtFimVigencia = Convert.ToDateTime("2050-12-25");
            limitePerfilRisco.vlrLimiteMin = 200;
            limitePerfilRisco.vlrLimiteMax = 200;
            limitePerfilRisco.codTipoFiltro = 9;
            limitePerfilRisco.codSubTipoFiltro = 2;
            limitePerfilRisco.dtAlteracao = DateTime.Now;
            limitePerfilRisco.codUsuarioAlteracao = 1;
            limitePerfilRisco.inExcecao = "S";
            limitePerfilRisco.inDiarioMensal = "D";

            lstLimitePerfilRisco.Add(limitePerfilRisco);

            repositorioFabrica = new RepositorioLimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            int ret = repositorioFabrica.Gravar(lstLimitePerfilRisco);

            Assert.IsTrue(ret > 0);
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveAlterarLimiteExcecao()
        {
            repositorioFabrica = new RepositorioLimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            lstLimitePerfilRisco = new List<LimitePerfilRisco>();

            limitePerfilRisco = new LimitePerfilRisco();
            limitePerfilRisco.codLimitePerfilRisco = 218;
            limitePerfilRisco.codPerfilRisco = "AGGRESSI";
            limitePerfilRisco.dtIniVigencia = Convert.ToDateTime("2050-11-25");
            limitePerfilRisco.dtFimVigencia = Convert.ToDateTime("2050-12-25");
            limitePerfilRisco.vlrLimiteMin = 100;
            limitePerfilRisco.vlrLimiteMax = 100;
            limitePerfilRisco.codTipoFiltro = 9;
            limitePerfilRisco.codSubTipoFiltro = 2;
            limitePerfilRisco.dtAlteracao = DateTime.Now;
            limitePerfilRisco.codUsuarioAlteracao = 1;
            limitePerfilRisco.inExcecao = "S";
            limitePerfilRisco.inDiarioMensal = "D";

            lstLimitePerfilRisco.Add(limitePerfilRisco);            

            int ret = repositorioFabrica.Alterar(lstLimitePerfilRisco);

            Assert.IsTrue(ret > 0);
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveConsultarLimiteExcecao()
        {
            limitePerfilRisco = new LimitePerfilRisco();
            limitePerfilRisco = new LimitePerfilRisco();
            limitePerfilRisco.codLimitePerfilRisco = 218;
            limitePerfilRisco.codPerfilRisco = "AGGRESSI";
            limitePerfilRisco.dtIniVigencia = Convert.ToDateTime("2050-11-25");
            limitePerfilRisco.dtFimVigencia = Convert.ToDateTime("2050-12-25");
            limitePerfilRisco.vlrLimiteMin = 100;
            limitePerfilRisco.vlrLimiteMax = 100;
            limitePerfilRisco.codTipoFiltro = 9;
            limitePerfilRisco.codSubTipoFiltro = 2;
            limitePerfilRisco.dtAlteracao = DateTime.Now;
            limitePerfilRisco.codUsuarioAlteracao = 1;
            limitePerfilRisco.inExcecao = "S";
            limitePerfilRisco.inDiarioMensal = "D";

            repositorioFabrica = new RepositorioLimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<LimitePerfilRisco> lstRet = repositorioFabrica.SelecionarRegistro(limitePerfilRisco);

            Assert.IsTrue(lstRet.Count() > 0);
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveConsultarLimiteExcecaoPorId()
        {
            List<int> lstCodLimite = new List<int>();

            int codLimitePerfilRisco = 218;
            
            lstCodLimite.Add(codLimitePerfilRisco);

            repositorioFabrica = new RepositorioLimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<LimitePerfilRisco> lstRet = repositorioFabrica.SelecionarRegistroPorId(lstCodLimite);

            Assert.IsTrue(lstRet.Count() > 0);
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveApagarLimiteExcecao()
        {
            lstLimitePerfilRisco = new List<LimitePerfilRisco>();

            limitePerfilRisco = new LimitePerfilRisco();
            limitePerfilRisco.codLimitePerfilRisco = 218;
            limitePerfilRisco.codPerfilRisco = "AGGRESSI";
            limitePerfilRisco.dtIniVigencia = Convert.ToDateTime("2050-11-25");
            limitePerfilRisco.dtFimVigencia = Convert.ToDateTime("2050-12-25");
            limitePerfilRisco.vlrLimiteMin = 100;
            limitePerfilRisco.vlrLimiteMax = 100;
            limitePerfilRisco.codTipoFiltro = 9;
            limitePerfilRisco.codSubTipoFiltro = 2;
            limitePerfilRisco.dtAlteracao = DateTime.Now;
            limitePerfilRisco.codUsuarioAlteracao = 1;
            limitePerfilRisco.inExcecao = "S";
            limitePerfilRisco.inDiarioMensal = "D";

            lstLimitePerfilRisco.Add(limitePerfilRisco);

            repositorioFabrica = new RepositorioLimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            int ret = repositorioFabrica.Apagar(lstLimitePerfilRisco);

            Assert.IsTrue(ret > 0);            
        }
    }
}
