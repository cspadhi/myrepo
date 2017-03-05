using EFEndToEnd.Business.Models;
using EFEndToEnd.Business.Services;
using EFEndToEnd.DataAccess.Repository;
using StructureMap;

namespace EFEndToEnd.Business.Mappings
{
    public static class Mapper
    {
        public static void CreateMaps()
        {
            AutoMapper.Mapper.CreateMap<Data.ContactType, ContactType>();
            AutoMapper.Mapper.CreateMap<Data.ContactInfo, ContactInfo>();

            AutoMapper.Mapper.CreateMap<Data.Address, Address>();
            AutoMapper.Mapper.CreateMap<Address, Data.Address>();

            AutoMapper.Mapper.CreateMap<ContactType, Data.ContactType>().ForMember(_ => _.Contacts, _ => _.Ignore());

            AutoMapper.Mapper.CreateMap<Data.Contact, Contact>();
            AutoMapper.Mapper.CreateMap<Contact, Data.Contact>()
                .ForMember(_ => _.Companies, _ => _.Ignore())
                .ForMember(_ => _.ContactType, _ => _.Ignore());

        }

        public static void CreateServiceMaps()
        {
            ObjectFactory.Configure(factory =>
            {
                factory.For<IContactManagementRepository>().Use<ContactManagementRepository>();
                factory.For<IContactManagementService>().Use<ContactManagementService>();
            });
        }

        public static void Verify()
        {
#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

    }
}
