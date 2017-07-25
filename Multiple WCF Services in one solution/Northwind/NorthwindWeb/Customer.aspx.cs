using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NorthwindWeb.CustomerServiceRef; 

namespace NorthwindWeb
{
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            int customerID = 0;
            int.TryParse(txtCustomerID.Text, out customerID);

            using (CustomerServiceClient client = new CustomerServiceClient("WSHttpBinding_ICustomerService"))
            {                
                lblName.Text = client.GetCustomerName(customerID);
                lblCity.Text = client.GetCustomerCity(customerID);
                lblCount.Text = client.GetCustomerCount().ToString();
            }
        }
    }
}