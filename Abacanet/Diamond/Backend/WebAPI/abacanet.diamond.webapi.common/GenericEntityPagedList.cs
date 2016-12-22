using System.Collections.Generic;

namespace abacanet.diamond.webapi.common
{
    public class GenericEntityPagedList<T> where T : class
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public int ActualPage { get; set; }
        public IEnumerable<T> Entity { get; set; }
    }
}