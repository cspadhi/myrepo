using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace NorthwindServices.ServiceContracts
{
    [ServiceContract(Namespace = "http://dotnetmentors.com/services/customer")]
    public interface ICustomerService
    {
        [OperationContract]
        string GetCustomerName(int CustomerID);

        [OperationContract]
        string GetCustomerCity(int CustomerID);

        [OperationContract]
        int GetCustomerCount();
    }
}
