using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMproject
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public Money Money { get; set; }

        public Account() { }

        public Account(string accountnum, Money money)
        {
            Money = money;
            if (accountnum.Length == 21)
            {
                AccountNumber = accountnum;
            }
            else { Console.WriteLine("Account shoud contain 21 digits"); }

        }
    }
}
