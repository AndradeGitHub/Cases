using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace abacanet.diamond.webapi.Models
{
    public class UserInviteViewModel
    {
        public int Id { get; set; }
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
        public string Url { get; set; }
    }
}