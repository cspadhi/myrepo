using AutoMapper;
using EFEndToEnd.Business.Models;
using EFEndToEnd.Common;
using EFEndToEnd.DataAccess.Repository;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace EFEndToEnd.Business.Services
{
    public class ContactManagementService : IContactManagementService
    {
        private readonly IContactManagementRepository _contactManagementRepository;

        public ContactManagementService()
        {
            _contactManagementRepository = ObjectFactory.GetInstance<IContactManagementRepository>();
           
        }

        public void Dispose()
        {
            _contactManagementRepository.Dispose();
        }

        public IEnumerable<ContactType> GetContactTypes(ICriteria criteria)
        {
            return
                _contactManagementRepository.GetContactTypes(criteria)
                                            .Select(Mapper.Map<Data.ContactType, ContactType>);
        }

        public IEnumerable<ContactType> GetAllContactTypes()
        {
            return
                _contactManagementRepository.GetAllContactTypes()
                                            .Select(Mapper.Map<Data.ContactType, ContactType>);
        }

        public int GetTotalContactTypes()
        {
            return _contactManagementRepository.GetTotalContactTypes();
        }



        public ContactType CreateContactType(ContactType contactType)
        {
            return Mapper.Map<Data.ContactType, ContactType>(_contactManagementRepository.Insert(Mapper.Map<ContactType, Data.ContactType>(contactType)));
        }

        public ContactType GetContactType(int id)
        {
            return Mapper.Map<Data.ContactType, ContactType>(_contactManagementRepository.GetContactType(id));
        }

        public ContactType UpdateContactType(ContactType contactType)
        {
            return Mapper.Map<Data.ContactType, ContactType>(_contactManagementRepository.Update(Mapper.Map<ContactType, Data.ContactType>(contactType)));
        }

        public ContactType DeleteContactType(ContactType contactType)
        {
            return Mapper.Map<Data.ContactType, ContactType>(_contactManagementRepository.Delete(Mapper.Map<ContactType, Data.ContactType>(contactType)));
        }

        public Contact CreateContact(Contact contact)
        {
            return Mapper.Map<Data.Contact, Contact>(_contactManagementRepository.Insert(Mapper.Map<Contact, Data.Contact>(contact)));
        }

        public Contact GetContact(int id)
        {
            return Mapper.Map<Data.Contact, Contact>(_contactManagementRepository.GetContact(id));
        }

        public Contact UpdateContact(Contact contact)
        {
            return Mapper.Map<Data.Contact, Contact>(_contactManagementRepository.Update(Mapper.Map<Contact, Data.Contact>(contact)));
        }

        public Contact DeleteContact(Contact contact)
        {
            return Mapper.Map<Data.Contact, Contact>(_contactManagementRepository.Delete(Mapper.Map<Contact, Data.Contact>(contact)));
        }


        public int GetTotalContacts()
        {
            return _contactManagementRepository.GetTotalContacts();
        }


        public IEnumerable<ContactInfo> GetContacts(ICriteria criteria)
        {
            return _contactManagementRepository.GetContacts(criteria)
                .Select(Mapper.Map<Data.ContactInfo, ContactInfo>); 
        }
    }
}