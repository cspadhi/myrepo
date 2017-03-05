using System.Collections.Generic;

namespace EFEndToEnd.Data
{
    public class Contact
    {
        public Contact()
        {
            Address = new Address();
        }


        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public Address Address { get; set; }

        public int ContactTypeId { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

    }
}
