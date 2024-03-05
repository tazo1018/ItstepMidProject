using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ATMproject
{

    public class Client
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string PersonalId { get; set; }
        public int ClientId { get; set; }     
        public Account Account { get; set; }
        public int Password { get; set; }
        public Client(string name, string surname, string personalId,string clientId, int password, Account account)
        {

            Name = name;
            SurName = surname;
            if (personalId.Length == 11)
            {
                PersonalId = personalId;
            }
            else { Console.WriteLine("Client ID should countain 11 digits"); }
            ClientId = 0;
            Password = password;
            Account = account;
        }
        public Client(string name, string surname, string personalId, int password, Account account)
        {

            Name = name;
            SurName = surname;
            if (personalId.Length == 11)
            {
                PersonalId = personalId;
            }
            else { Console.WriteLine("Client ID should countain 11 digits"); }
            ClientId = 0;
            Password = password;
            Account = account;
        }

        public Client()
        {
            
        }
        public void AddOnBalance(Money amount)
        {

            Account.Money += amount;
            JSONlogger logger = new JSONlogger();
            logger.AddOnBalanceJSON(this, amount);

        }
        public void CashTransaction(Client client, Money amountOfCash)
        {
            if (Account.Money.Balance <= 0 || amountOfCash > Account.Money)
            {
                Console.WriteLine("this client cant make transaction due tue lack of money");
            }
            else
            {
                Account.Money -= amountOfCash;
                client.Account.Money += amountOfCash;

                JSONlogger logger = new JSONlogger();
                logger.CashTransactionLog(this, client, amountOfCash); 
            }

        }
        public void CashWithdrawal(Money amount)
        {
            if (Account.Money.Balance <= 0 || amount > Account.Money)
            {
                Console.WriteLine("you can't withdraw cash due tue lack of money");
            }
            else
            {
                Account.Money -= amount;
                JSONlogger logger = new JSONlogger();
                logger.CashWithdrawalLog(this, amount); 

            }

        }

        public void BalanceCHeck()
        {
            JSONlogger logger = new JSONlogger();
            logger.BalanceCHeckLog(this);
        }

        public void SeeOperationHystory()
        {
            string filePath = "C:\\Users\\user\\source\\repos\\ItstepMidProject\\ATMproject\\JSONlog.json";
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(this.PersonalId))
                    {
                        lines.Add(line);
                    }
                }
            }

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

    }
}
