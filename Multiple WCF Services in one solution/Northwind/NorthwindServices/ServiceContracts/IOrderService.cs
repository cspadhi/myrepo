using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace NorthwindServices.ServiceContracts
{
    [ServiceContract(Namespace = "http://dotnetmentors.com/services/order")]
    public interface IOrderService
    {
        [OperationContract] 
        string GetOrderDate(int orderID);
        
        [OperationContract] 
        string GetOrderAmount(int orderID);

        [OperationContract] 
        string GetShipCountry(int orderID);
    }
}
