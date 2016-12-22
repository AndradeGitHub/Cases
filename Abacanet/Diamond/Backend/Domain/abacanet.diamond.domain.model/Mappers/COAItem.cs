using System.Collections.Generic;

namespace abacanet.diamond.domain.model.Mappers
{
    public class COAItem
    {
        public List<string> Texts { get; set; }
        public List<int> Numbers { get; set; }

        public COAItem()
        {
            Texts = new List<string>();
            Numbers = new List<int>();
        }
    }
}
