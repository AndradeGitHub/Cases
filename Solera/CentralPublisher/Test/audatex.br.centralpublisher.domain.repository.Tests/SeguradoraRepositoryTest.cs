using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using audatex.br.centralpublisher.domain.repository;
using audatex.br.centralpublisher.infrastructure.persistence;

namespace audatex.br.centralpublisher.domain.repository.Tests
{
    [TestClass]
    public class SeguradoraRepositoryTest
    {
        [TestMethod]
        public void GetSeguradora_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var seguradoraRepository = new SeguradoraRepository(unitOfWork);

            var lstSeguradoras = seguradoraRepository.GetSeguradoras();
            Assert.IsNotNull(lstSeguradoras);
        }
    }
}
