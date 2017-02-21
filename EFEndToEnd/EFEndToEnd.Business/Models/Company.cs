using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFEndToEnd.Business.Models
{
    public class Company
    {
        public Company()
        {
            Address = new Address();
        }


        public int CompanyId { get; set; }

        [StringLength(50)]
        [Display(Name = "Name")]
        public string CompanyName { get; set; }

        public Address Address { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
