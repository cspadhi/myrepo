using System.Collections.Generic;

namespace EFEndToEnd.Data
{
    public class ContactType
    {
        public int ContactTypeId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
