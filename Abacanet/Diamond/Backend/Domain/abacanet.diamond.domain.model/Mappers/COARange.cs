using System.Collections.Generic;

namespace abacanet.diamond.domain.model.Mappers
{
    public class COARange
    {
        public List<string> Texts { get; set; }
        public int InicialNumber { get; set; }
        public int FinalNumber { get; set; }

        public COARange()
        {
            Texts = new List<string>();
        }
    }
}
