using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

// Reference - http://stevemichelotti.com/restful-wcf-services-with-no-svc-file-and-no-config/

namespace WcfOnWebApp
{
    [ServiceContract]
    interface IPersonService
    {
        [OperationContract]
        Person GetPerson(string id);

        [OperationContract]
        Person InsertPerson(Person person);

        [OperationContract]
        Person UpdatePerson(string id, Person person);

        [OperationContract]
        void DeletePerson(string id);
    }
}
