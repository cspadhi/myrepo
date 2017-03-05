using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using WCFAuthentication.Models;

namespace WCFAuthentication.App_Code.Authentication
{
    public class CustomValidator : UserNamePasswordValidator
    {
        //throw new SecurityTokenException();
        public override void Validate(string userName, string password)
        {
            AccountModel am = new AccountModel();
            if (am.Login(userName, password)) return;

            throw new SecurityTokenException("Invalid Creds");
        }
    }
}