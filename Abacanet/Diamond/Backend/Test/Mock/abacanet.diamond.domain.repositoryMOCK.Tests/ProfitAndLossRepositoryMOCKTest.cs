using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using abacanet.diamond.infrastructure.persistence.interfaces;
using abacanet.diamond.domain.model;

namespace abacanet.diamond.domain.repository.Tests
{
    [TestClass]
    public class ProfitAndLossRepositoryMOCKTest
    {
        [TestMethod]
        public void should_save_profit_and_loss_item()
        {
            var expected = new ProfitAndLossModel();

            #region Mocks
            var dbSet = new Mock<IDbSet<ProfitAndLossModel>>();

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.ProfitsAndLosses).Returns(dbSet.Object);
            #endregion

            var repository = new ProfitAndLossRepository(unitOfWork.Object);
            repository.Add(expected);

            dbSet.Verify(v => v.Add(expected), Times.Once());
        }
    }
}
