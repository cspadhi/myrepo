using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFAuthentication.Models
{
    public class AccountModel
    {
        private List<Account> accounts = new List<Account>();

        public AccountModel()
        {
            accounts.Add(new Account() { UserName = "acc1", Password = "123" });
            accounts.Add(new Account() { UserName = "acc2", Password = "123" });
            accounts.Add(new Account() { UserName = "acc3", Password = "123" });
        }

        public bool Login(string userName, string password)
        {
            return accounts.Count(a => a.UserName.Equals(userName) && a.Password.Equals(password)) > 0;
        }
    }
}