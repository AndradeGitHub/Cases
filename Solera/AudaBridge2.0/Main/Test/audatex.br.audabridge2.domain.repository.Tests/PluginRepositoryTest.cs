using Microsoft.VisualStudio.TestTools.UnitTesting;


using audatex.br.audabridge2.domain.model;
using audatex.br.audabridge2.domain.model.enumerator;
using audatex.br.audabridge2.infrastructure.persistence;

namespace audatex.br.audabridge2.domain.repository.Tests
{
    [TestClass]
    public class PluginRepositoryTest
    {
        [TestCategory("DATABASE"), TestMethod]
        public void GetPluginRepository_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var pluginRepository = new PluginRepository(unitOfWork);

            PluginModel pluginModel = new PluginModel();
            pluginModel.Tomada = (int)EnumTomada.Tomada1;
            pluginModel.Seguradora = new SeguradoraModel();
            pluginModel.Seguradora.Nome = "Bradesco";

            var result = pluginRepository.GetPlugin(pluginModel);

            Assert.IsNotNull(result);
        }
    }
}
