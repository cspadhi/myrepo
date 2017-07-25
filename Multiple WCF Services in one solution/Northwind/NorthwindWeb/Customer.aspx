<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="NorthwindWeb.Customer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:70%; margin-top:50px; "   >
            <tr>
                <td style="width:15%; margin-right:5px; padding-right:5px;"    >
                    Enter Customer ID :
                </td>
                <td style="width:60%; float:left;">
                    <asp:TextBox ID="txtCustomerID" runat="server"></asp:TextBox> 
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnGetData" runat="server" Text="Get Customer Data" 
                        onclick="btnGetData_Click"  />  
                </td>
            </tr>
            <tr>
                <td>
                    Customer Name :
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label> 
                </td>
            </tr>
            <tr>
                <td>
                    Customer City :
                </td>
                <td>
                    <asp:Label ID="lblCity" runat="server"></asp:Label> 
                </td>
            </tr>
            <tr>
                <td>
                    Customer Count :
                </td>
                <td>
                    <asp:Label ID="lblCount" runat="server"></asp:Label> 
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
