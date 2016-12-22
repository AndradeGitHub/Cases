namespace abacanet.diamond.domain.model.Mappers
{
    public class COA
    {
        public int Code { get; set; }
        public COAItem Project { get; set; }
        public COAItem Program { get; set; }
        public COAItem Function { get; set; }
        public COARange Object { get; set; }
    }
}
