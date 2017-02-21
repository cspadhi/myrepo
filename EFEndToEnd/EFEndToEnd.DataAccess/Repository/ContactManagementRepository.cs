using System.Collections.Generic;
using System.Linq;
using EFEndToEnd.Common;
using EFEndToEnd.Data;
using EFEndToEnd.DataAccess.Repository.Core;

namespace EFEndToEnd.DataAccess.Repository
{
    public class ContactManagementRepository : WriteRepository<Context>, IContactManagementRepository
    {
        public IEnumerable<ContactType> GetContactTypes(ICriteria criteria)
        {
            IQueryable<ContactType> query = Context.ContactTypes;
            if (criteria.IsSearch)
            {
                var value = criteria.GetFieldData(criteria.FilterColumn);
                query = query.Where(one => one.Name.Contains(value));
            }
            if (criteria.SortColumn == "Name" && criteria.SortOrder == "asc")
            {
                query = query.OrderBy(one => one.Name);
            }
            else if (criteria.SortColumn == "Name" && criteria.SortOrder == "desc")
            {
                query = query.OrderByDescending(one => one.Name);
            }
            else
                query = query.OrderBy(one => one.Name);
            query = query.Skip((criteria.PageIndex - 1) * criteria.PageSize).Take(criteria.PageSize);
            return query;
        }

        public IEnumerable<ContactType> GetAllContactTypes()
        {
            return Context.ContactTypes.OrderBy(one=>one.Name);
        }

        public IEnumerable<ContactInfo> GetContacts(ICriteria criteria)
        {
            IQueryable<Contact> query = Context.Contacts;
            if (criteria.IsSearch)
            {
                var value = criteria.GetFieldData(criteria.FilterColumn);
                if (criteria.FilterColumn == "LastName")
                {
                    query = query.Where(one => one.LastName.Contains(value));
                }
                else if (criteria.FilterColumn == "FirstName")
                {
                    query = query.Where(one => one.FirstName.Contains(value));
                }
                
            }
            if (criteria.SortColumn == "FirstName" && criteria.SortOrder == "asc")
            {
                query = query.OrderBy(one => one.FirstName);
            }
            else if (criteria.SortColumn == "FirstName" && criteria.SortOrder == "desc")
            {
                query = query.OrderByDescending(one => one.FirstName);
            }
            else if (criteria.SortColumn == "LastName" && criteria.SortOrder == "asc")
            {
                query = query.OrderBy(one => one.LastName);
            }
            else if (criteria.SortColumn == "LastName" && criteria.SortOrder == "desc")
            {
                query = query.OrderByDescending(one => one.LastName);
            }
            else
                query = query.OrderBy(one => one.LastName);
            query = query.Skip((criteria.PageIndex - 1) * criteria.PageSize).Take(criteria.PageSize);
            return query.Select(one=>new ContactInfo
                {
                    ContactId = one.ContactId,
                    FirstName = one.FirstName,
                    LastName = one.LastName
                });
        }

        public int GetTotalContactTypes()
        {
            return Context.ContactTypes.Count();
        }

        public ContactType GetContactType(int id)
        {
            return Context.ContactTypes.FirstOrDefault(one => one.ContactTypeId == id);
        }

        public Contact GetContact(int id)
        {
            return Context.Contacts.FirstOrDefault(one => one.ContactId == id);
        }

        public int GetTotalContacts()
        {
            return Context.Contacts.Count();
        }
    }
}