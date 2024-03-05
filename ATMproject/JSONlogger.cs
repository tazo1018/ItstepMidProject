using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMproject
{
    internal class JSONlogger
    {
        private readonly string _FilePath = "C:\\Users\\user\\source\\repos\\ItstepMidProject\\ATMproject\\JSONlog.json";
        public void AddOnBalanceJSON(Client client, Money money)
        {
            string logMessage = $"client {client.Name} {client.SurName}(with personal ID {client.PersonalId}) added {money.Balance}{money.Currency} on his/her account. {DateTime.Now}";

            try
            {
                File.AppendAllText(_FilePath, logMessage + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        public void CashWithdrawalLog(Client client, Money money)
        {
            string logMessage = $"client {client.Name} {client.SurName} (with personal ID: {client.PersonalId}) withdrawed {money.Balance}{money.Currency} from his/her account. {DateTime.Now}";

            try
            {
                File.AppendAllText(_FilePath, logMessage + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        public void CashTransactionLog(Client from, Client to, Money money)
        {
            string logMessage = string.Empty;
            if (from.Name == to.Name && from.SurName == to.SurName) {
                logMessage = $"client {from.Name} {from.SurName} (with personal ID : {from.PersonalId}) transferred {money.Balance}{money.Currency} on {to.Name} {to.SurName} (with personal ID: {to.PersonalId}) 's account. {DateTime.Now}";
            } else
            {
                logMessage = $"client {from.Name} {from.SurName} transferred {money.Balance}{money.Currency} on {to.Name} {to.SurName} 's account.  {DateTime.Now}";
            }
            

            try
            {
                File.AppendAllText(_FilePath, logMessage + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        public void BalanceCHeckLog(Client client)
        {
            string logMessage = $"client {client.Name} {client.SurName} (with personal ID {client.PersonalId}) checked his/her remaining account balance. {DateTime.Now} ";

            try
            {
                File.AppendAllText(_FilePath, logMessage + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

    }
}
