using audatex.br.centralpublisher.domain.model;
using audatex.br.centralpublisher.infrastructure.persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralpublisher.domain.repository.Tests
{
    [TestClass]
    public class PedidoStatusRepositoryTest
    {
        [TestMethod]
        public void GetPedidosRecebidos_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var PedidoStatusRepository = new PedidoStatusRepository(unitOfWork);

            var lstPedidosRecebidos = PedidoStatusRepository.GetPedidoStatusDisponivelPorOperacao(EnumTipoOperacaoModel.ConfPedidos03);
            Assert.IsNotNull(lstPedidosRecebidos);
        }       
    }
}
