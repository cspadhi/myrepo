using EFEndToEnd.Business.Models;
using System;
using System.Collections.Generic;
using EFEndToEnd.Common;

namespace EFEndToEnd.Business.Services
{
    public interface IContactManagementService : IDisposable
    {
        IEnumerable<ContactType> GetContactTypes(ICriteria criteria);

        IEnumerable<ContactType> GetAllContactTypes();

        int GetTotalContactTypes();

        ContactType CreateContactType(ContactType contactType);

        ContactType GetContactType(int id);

        ContactType UpdateContactType(ContactType contactType);

        ContactType DeleteContactType(ContactType contactType);


        IEnumerable<ContactInfo> GetContacts(ICriteria criteria);

        int GetTotalContacts();

        Contact CreateContact(Contact contact);

        Contact GetContact(int id);

        Contact UpdateContact(Contact contact);

        Contact DeleteContact(Contact contact);
    }
}
