using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using abacanet.diamond.infrastructure.files;

namespace abacanet.diamond.infrastructure.tests.Files
{
    [TestClass]
    public class ExcelReaderTests
    {
        private const string fileName = "SchoolAProfitLoss.xlsx";

        public string File { get; set; }
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        [DeploymentItem(@"Fixtures\SchoolAProfitLoss.xlsx")]
        public void should_read_root_projects()
        {
            var expected = new string[]
            {
                "1500 DONATIONS",
                "Total 1500 DONATIONS",
                "1990 Miscellaneous",
                "Total 1990 Miscellaneous",
                "GENERAL FUND",
                "Total GENERAL FUND"
            };

            var file = Path.Combine(TestContext.DeploymentDirectory, fileName);
            var reader = new ExcelReader(file);

            var position = reader.Read();

            position.Children.Should().Contain(v => expected.Contains(v.Text));
        }

        [TestMethod]
        [DeploymentItem(@"Fixtures\SchoolAProfitLoss.xlsx")]
        public void should_read_children_from_first_projetct()
        {
            var expected = new string[]
            {
                "1500.1700 AZ. TAX CREDIT",
                "Total 1500.1700 AZ. TAX CREDIT",
            };

            var file = Path.Combine(TestContext.DeploymentDirectory, fileName);
            var reader = new ExcelReader(file);

            var position = reader.Read();

            var item = position.Children.FirstOrDefault(c => c.Text.Equals("1500 DONATIONS"));

            item.Children.Should().Contain(v => expected.Contains(v.Text));
        }

        [TestMethod]
        [DeploymentItem(@"Fixtures\SchoolAProfitLoss.xlsx")]
        public void should_read_children_from_first_sub_projects()
        {
            var expected = new string[]
            {
                "1791 tax credit activity fee",
                "1792 tax credit activity fee2"
            };

            var file = Path.Combine(TestContext.DeploymentDirectory, fileName);
            var reader = new ExcelReader(file);

            var firstLevel = reader.Read();

            var secondLevel = firstLevel.Children.FirstOrDefault(c => c.Text.Equals("1500 DONATIONS"));
            var thirdLevel = secondLevel.Children.FirstOrDefault(c => c.Text.Equals("1500.1700 AZ. TAX CREDIT"));

            thirdLevel.Children.Should().Contain(v => expected.Contains(v.Text));
        }

        [TestMethod]
        [DeploymentItem(@"Fixtures\SchoolAProfitLoss.xlsx")]
        public void should_read_value()
        {
            var expected = 1234.00m;

            var file = Path.Combine(TestContext.DeploymentDirectory, fileName);
            var reader = new ExcelReader(file);

            var firstLevel = reader.Read();

            var secondLevel = firstLevel.Children.FirstOrDefault(c => c.Text.Equals("1500 DONATIONS"));
            var thirdLevel = secondLevel.Children.FirstOrDefault(c => c.Text.Equals("1500.1700 AZ. TAX CREDIT"));
            var value = thirdLevel.Children
                .Where(c => c.Text.Equals("1792 tax credit activity fee2"))
                .Select(c => c.Value)
                .FirstOrDefault();

            value.Should().Be(expected);
        }
    }
}
