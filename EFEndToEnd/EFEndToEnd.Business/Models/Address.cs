using System.ComponentModel.DataAnnotations;

namespace EFEndToEnd.Business.Models
{
    public class Address
    {
        [StringLength(50)]
        [Display(Name = "Street")]
        public virtual string Street { get; set; }

        [StringLength(50)]
        [Display(Name = "City")]
        public virtual string City { get; set; }

        [StringLength(2)]
        [Display(Name = "State")]
        public virtual string State { get; set; }

        [StringLength(10)]
        [Display(Name = "Zip code")]
        public virtual string Zip { get; set; }
    }
}
