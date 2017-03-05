using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EFEndToEnd.Business.Validation;

namespace EFEndToEnd.Business.Models
{
    public class Contact
    {
        public Contact()
        {
            Address = new Address();
        }


        public int ContactId { get; set; }

        [StringLength(50)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Active Contact")]
        public bool IsActive { get; set; }

        public Address Address { get; set; }


        [Display(Name = "Type")]
        [DropDownRequired("Type is required")]
        public int ContactTypeId { get; set; }


    }
}
