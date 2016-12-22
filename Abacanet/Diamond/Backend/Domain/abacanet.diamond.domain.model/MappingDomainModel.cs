using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace abacanet.diamond.domain.model
{
    public class MappingDomainModel : EntityDomainModel
    {
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
