using System.Collections.Generic;
using EFEndToEnd.Common;
using EFEndToEnd.Data;
using EFEndToEnd.DataAccess.Repository.Core;

namespace EFEndToEnd.DataAccess.Repository
{
    public interface IContactManagementRepository : IWriteRepository
    {
        IEnumerable<ContactType> GetContactTypes(ICriteria criteria);

        IEnumerable<ContactInfo> GetContacts(ICriteria criteria);

        int GetTotalContactTypes();

        ContactType GetContactType(int id);
        
        Contact GetContact(int id);

        int GetTotalContacts();

        IEnumerable<ContactType> GetAllContactTypes();
    }
}
