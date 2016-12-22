using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace abacanet.diamond.domain.model
{
    public class UserInviteDomainModel : EntityDomainModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public DateTime RequestDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        [NotMapped]
        public string Url { get; set; }
    }
}
