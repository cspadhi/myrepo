using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NorthwindBackOffice.CustomerServiceRef;
using NorthwindBackOffice.OrderServiceRef;
using System.Xml.Linq;    

namespace NorthwindBackOffice
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowCustomerDetails();
            Console.WriteLine(); 
            ShowOrderDetails();
            Console.Read(); 
        }

        private static void ShowCustomerDetails()
        {
            string customerCity = string.Empty;
            string customerName = string.Empty;
            int customerCount = 0;

            using (CustomerServiceClient client = new CustomerServiceClient("NetNamedPipeBinding_ICustomerService"))
            {
                 customerCity = client.GetCustomerCity(2);
                 customerName = client.GetCustomerName(2);
                 customerCount = client.GetCustomerCount();   
            }

            Console.WriteLine("******* Customer Data **************");
            Console.WriteLine("Customer Name : " + customerName);   
            Console.WriteLine("Customer City : " + customerCity);
            Console.WriteLine("Total customer count : " + customerCount.ToString());    
        }

        private static void ShowOrderDetails()
        {
            string orderAmount = string.Empty;
            string orderDate = string.Empty;
            string orderShip = string.Empty;

            using (OrderServiceClient client = new OrderServiceClient("NetNamedPipeBinding_IOrderService"))
            {
                orderAmount = client.GetOrderAmount(10250);
                orderDate = client.GetOrderDate(10250);
                orderShip = client.GetShipCountry(10250);
            }

            Console.WriteLine("******* Order Data **************");
            Console.WriteLine("Order Amount : " + orderAmount);
            Console.WriteLine("Order Date : " + orderDate);
            Console.WriteLine("Ship Country : " + orderShip);
        }
    }
}
