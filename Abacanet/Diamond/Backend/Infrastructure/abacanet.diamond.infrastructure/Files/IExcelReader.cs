using abacanet.diamond.domain.model;

namespace abacanet.diamond.infrastructure.files
{
    public interface IExcelReader
    {
        ProfitAndLossModel Read();
    }
}
