using System.Collections.Generic;

namespace abacanet.diamond.domain.model
{
    public class ProfitAndLossModel : EntityDomainModel
    {
        public int Number { get; set; }
        public string Text { get; set; }
        public decimal Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public IList<ProfitAndLossModel> Children { get; set; }
        public ProfitAndLossModel Parent { get; set; }

        public ProfitAndLossModel()
        {
            Children = new List<ProfitAndLossModel>();
        }
    }
}
