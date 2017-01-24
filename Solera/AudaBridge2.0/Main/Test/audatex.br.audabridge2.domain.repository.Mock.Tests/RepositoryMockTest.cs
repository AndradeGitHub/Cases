using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using audatex.br.audabridge2.domain.model;
using audatex.br.audabridge2.infrastructure.persistence;

namespace audatex.br.audabridge2.domain.repository.Mock.Tests
{
    [TestClass]
    public class RepositoryMockTest
    {
        private readonly string _mongoDbConnectionString = ConfigurationManager.ConnectionStrings["MongoDbConnectionString"].ConnectionString;
        private readonly string _mongoDbDatabaseName = ConfigurationManager.AppSettings["MongoDbDatabaseName"];

        [TestCategory("MOCK"), TestMethod]
        public void AddRepositoryMOCK_SUCESS()
        {
            var integracaoModel = new IntegracaoModel();

            var mockContext = new Mock<UnitOfWork>();            
            mockContext.Setup(e => e.Set<IntegracaoModel>().Add(integracaoModel)).Verifiable();

            var repositoryMock = new Repository<IntegracaoModel>(mockContext.Object);
            repositoryMock.Add(integracaoModel);

            mockContext.Verify(x => x.Set<IntegracaoModel>().Add(integracaoModel), Times.Once());
        }

        [TestCategory("MOCK"), TestMethod]
        public void DeleteRepositoryMOCK_SUCESS()
        {
            var integracaoModel = new IntegracaoModel();
            
            var mockContext = new Mock<UnitOfWork>();
            mockContext.Setup(x => x.Set<IntegracaoModel>().Remove(integracaoModel)).Verifiable();

            var repository = new Repository<IntegracaoModel>(mockContext.Object);
            repository.Delete(integracaoModel);

            mockContext.Verify(x => x.Set<IntegracaoModel>().Remove(integracaoModel), Times.Once());
        }

        [TestCategory("MOCK"), TestMethod]
        public void DeleteAsyncRepositoryMOCK_SUCESS()
        {
            var integracaoEx = new Mock<IntegracaoExceptionModel>();
            
            var _repositoryBase = new Mock<RepositoryBase>(_mongoDbConnectionString, _mongoDbDatabaseName);            
            var _repositoryIntegracaoException = new Mock<Repository<IntegracaoExceptionModel>>(_repositoryBase.Object.CreateDataBase(), "IntegracaoException");            

            var result = _repositoryIntegracaoException.Object.DeleteAsync(integracaoEx.Object);

            _repositoryBase.Verify();            
        }

        [TestCategory("MOCK"), TestMethod]
        public void UpdateAsyncRepositoryMOCK_SUCESS()
        {
            var integracaoEx = new Mock<IntegracaoExceptionModel>();

            var _repositoryBase = new Mock<RepositoryBase>(_mongoDbConnectionString, _mongoDbDatabaseName);
            var _repositoryIntegracaoException = new Mock<Repository<IntegracaoExceptionModel>>(_repositoryBase.Object.CreateDataBase(), "IntegracaoException");

            var result = _repositoryIntegracaoException.Object.UpdateAsync(integracaoEx.Object);

            _repositoryBase.Verify();
        }
    }
}
