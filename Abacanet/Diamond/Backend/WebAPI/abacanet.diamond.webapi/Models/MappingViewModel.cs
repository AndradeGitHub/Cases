using System;
using System.Collections.Generic;

namespace abacanet.diamond.webapi.Models
{
    public class MappingViewModel
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public string Project { get; set; }
        public string Program { get; set; }
        public string Function { get; set; }
        public string Object { get; set; }
        public string AFRNumber { get; set; }
        public string TransactionType { get; set; }
        public string Notes { get; set; }
    }
}
