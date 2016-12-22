using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;

namespace ubs.brasil.qualitycontrol.test.dominio.core
{
    [TestClass]
    public class ProcessamentoManualTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IOperacao<Processamento> operacoesFabrica;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("PROCESSAMENTO MANUAL"), TestMethod]
        public void DeveExecutarProcessamentoManualMOCK()
        {
            List<Processamento> lstProcessamento = new List<Processamento>();

            Processamento processamento = new Processamento();
            processamento.codUsuario = 1;
            processamento.dtResultado = DateTime.Now;
            processamento.codCarteira = null;
            //processamento.inPeriodoProcessamento = "D"; //D: diario, M: mensal
            processamento.inTipoExecucao = "M"; //M: manual, A: automático

            lstProcessamento.Add(processamento);

            operacoesFabrica = new ProcessamentoManual(_mockRepositorioModeloQC.Object, _mockRepositorioModeloWM_DB.Object);

            Assert.IsTrue(operacoesFabrica.Gravar(lstProcessamento) > 0);
        }

        [TestCategory("PROCESSAMENTO MANUAL"), TestMethod]
        public void DeveExecutarProcessamentoManual()
        {
            List<Processamento> lstProcessamento = new List<Processamento>();

            Processamento processamento = new Processamento();
            processamento.codUsuario = 1;
            processamento.dtResultado = DateTime.Now;
            processamento.codCarteira = null;
            processamento.inTipoExecucao = "M";

            lstProcessamento.Add(processamento);

            operacoesFabrica = new ProcessamentoManual(_repositorioModeloQC, _repositorioModeloWM_DB);

            Assert.IsTrue(operacoesFabrica.Gravar(lstProcessamento) > 0);
        }
    }
}
