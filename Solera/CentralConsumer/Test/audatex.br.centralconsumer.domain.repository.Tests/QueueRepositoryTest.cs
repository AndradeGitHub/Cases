using Microsoft.VisualStudio.TestTools.UnitTesting;

using audatex.br.centralconsumer.domain.model;
using audatex.br.centralconsumer.domain.repository;
using audatex.br.centralconsumer.infrastructure.persistence;

namespace audatex.br.centralconsumer.domain.repository.Tests
{
    [TestClass]
    public class QueueRepositoryTest
    {
        [TestMethod]
        public void GetQueueRepository_SUCESS()
        {
            QueueModel queue = new QueueModel();
            queue.Operacao = (int)EnumQueueOperacao.Confirmacao;

            var unitOfWork = new UnitOfWork();
            var queueRepository = new QueueRepository(unitOfWork);

            var lstQueue = queueRepository.GetQueue(queue);
            Assert.IsNotNull(lstQueue);
        }
    }
}