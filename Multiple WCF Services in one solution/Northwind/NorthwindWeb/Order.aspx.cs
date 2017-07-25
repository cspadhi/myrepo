using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NorthwindWeb.OrderServiceRef;  

namespace NorthwindWeb
{
    public partial class Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            int orderID = 0;
            int.TryParse(txtOrderID.Text, out orderID);

            using (OrderServiceClient client = new OrderServiceClient("WSHttpBinding_IOrderService"))
            {
                lblOrderAmount.Text = client.GetOrderAmount(orderID);
                lblDate.Text = client.GetOrderDate(orderID);
                lblShip.Text = client.GetShipCountry(orderID);
            }
        }
    }
}