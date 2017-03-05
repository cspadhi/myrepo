using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Web.UI;

namespace WcfOnWebApp
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PersonService : IPersonService
    {
        [WebGet(UriTemplate = "Person({id})")]
        public Person GetPerson(string id)
        {
            return this.GetPersonObject();
        }

        [WebInvoke(UriTemplate = "Person", Method = "POST")]
        public Person InsertPerson(Person person)
        {
            return this.GetPersonObject();
        }

        [WebInvoke(UriTemplate = "Person({id})", Method = "PUT")]
        public Person UpdatePerson(string id, Person person)
        {
            return this.GetPersonObject();
        }

        [WebInvoke(UriTemplate = "Person({id})", Method = "DELETE")]
        public void DeletePerson(string id)
        {
        }

        private Person GetPersonObject()
        {
            return new Person
            {
                Id = 1,
                Name = "CSP",
                Age = 40,
                Gender = "Male"
            };
        }
    }
}