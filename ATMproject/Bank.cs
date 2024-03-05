using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ATMproject
{
    internal class Bank
    {
        
        private List<Client> ClientList= new List<Client>();
        private List<int> Passwords = new List<int>();
        private readonly string _filePath = "C:\\Users\\user\\source\\repos\\ItstepMidProject\\ATMproject\\JSONfile.json";
        private readonly string _passwordsFilePath = "C:\\Users\\user\\source\\repos\\ItstepMidProject\\ATMproject\\Passwords.txt";
        private readonly string _accountNumbersFilePath = "C:\\Users\\user\\source\\repos\\ItstepMidProject\\ATMproject\\accountNumbers.txt";
       

        public void start()
        {
            Console.WriteLine("welcome to our online-mibi-bank OMB");
            Client client = ReceiveClient();
            if (client == null )
            {
                Console.WriteLine("Operation failed");
            } else
            {
                Console.WriteLine("please select what kind of operation you would like to perform: 1 - add money on your balance; 2 - send money to another person; 3 - cash withdrawal; 4 - see your balance; 5 - see your balance history");
                int answer = int.Parse(Console.ReadLine());
                while(answer != 1 && answer != 2 && answer != 3 && answer != 4 && answer != 5)
                {
                    Console.WriteLine("please select what kind of operation you would like to perform: 1 - add money on your balance; 2 - send money to another person; 3 - cash withdrawal;4 - see your balance; 5 - see your balance history");
                    answer = int.Parse(Console.ReadLine());
                }
                string receiverIdtoReplace = string.Empty;
                if (!ClientList.Contains(client))
                {
                    while (answer != 1)
                    {
                        Console.WriteLine("you are visiting our bank for the first time. First you have to add money to your account in order to do other operations. please enter 1");
                        answer = int.Parse(Console.ReadLine());
                    }
                }
                if (answer == 1)
                {
                    Console.WriteLine($"enter the amount of money you would like to add on your balance.(there is {client.Account.Money.Balance}{client.Account.Money.Currency} on your account)");
                    int amount = int.Parse(Console.ReadLine());
                    Console.WriteLine($"please note that currency that you have chosen on your account is: {client.Account.Money.Currency}");
                    Money money = new Money(client.Account.Money.Currency, amount);
                    client.AddOnBalance(money);
                } else if (answer == 2)
                {
                    Console.WriteLine("enter the ID of a person to whom you are making transaction");
                    string receiverId = Console.ReadLine();
                    while (true)
                    {
                        if (receiverId.Length == 11)
                        {
                            if (ClientList.Any(client => client.PersonalId == receiverId))
                            {
                                break;
                            } else
                            {
                                Console.WriteLine("there is no person with that ID on our system. Please try again.");
                                receiverId = Console.ReadLine();
                            }
                        } else
                        {
                            Console.WriteLine("Id should countain 11 digit. Please try again");
                            receiverId = Console.ReadLine();
                        }
                    }
                    Client receiver = ClientList.FirstOrDefault(client => client.PersonalId == receiverId);
                    
                    Console.WriteLine($"enter the amount of money you would like to send person named:{receiver.Name} {receiver.SurName}. with currency: {receiver.Account.Money.Currency}. (there is {client.Account.Money.Balance}{client.Account.Money.Currency} on your account)");
                    int amount = int.Parse(Console.ReadLine());
                    Money money = new Money(receiver.Account.Money.Currency, amount);
                    client.CashTransaction(receiver, money);
                    receiverIdtoReplace = receiver.PersonalId;

                } else if(answer == 3)
                {
                    Console.WriteLine($"enter the amount of money you would like to withdraw on your balance. (there is {client.Account.Money.Balance}{client.Account.Money.Currency} on your account) ");
                    int amount = int.Parse(Console.ReadLine());
                    Console.WriteLine($"please note that currency that you have chosen on your account is: {client.Account.Money.Currency}");
                    Money money = new Money(client.Account.Money.Currency, amount);
                    client.CashWithdrawal(money);
                } else if (answer == 4)
                {
                    Console.WriteLine($"your balance is: {client.Account.Money.Balance}{client.Account.Money.Currency}");
                    client.BalanceCHeck();
                } else if ( (answer == 5))
                {
                    client.SeeOperationHystory();
                }
                


                if (!ClientList.Any(c => c.PersonalId == client.PersonalId))
                {
                    AddNewClient(client);
                } else if (ClientList.Any(c => c.PersonalId == client.PersonalId) && answer == 1)
                {
                    Replace(client);   
                } else if (ClientList.Any(c => c.PersonalId == client.PersonalId) && answer == 2)
                {
                    Replace(client);
                    Replace(ClientList.FirstOrDefault(client => client.PersonalId == receiverIdtoReplace));
                } else if (ClientList.Any(c => c.PersonalId == client.PersonalId) && answer == 3)
                {
                    Replace(client);
                }

            }
        }
        private void AppendTextToFile(string text)
        {

            using (StreamWriter writer = new StreamWriter(_passwordsFilePath, true))
            {
                writer.WriteLine(text);
            }
        }

        private List<int> ReadPasswordsFromFile()
        {
            List<int> passwords = new List<int>();

            using (StreamReader reader = new StreamReader(_passwordsFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    passwords.Add(int.Parse(line));
                }
            }

            return passwords;
        }
        private static string ToJson(Client client)
        {
            string jsonObject = JsonSerializer.Serialize(client);
            return jsonObject;
        }

        public void AddNewClient(Client client)
        {
            if (ClientList.Any())
            {
                client.ClientId = ClientList.Max(x => x.ClientId) + 1;
            }
            else
            {
                client.ClientId = 1;
            }

            var result = ToJson(client);
            Save(result);
        }
        public void Save(string input) 
        {
            Client newCustomer = JsonSerializer.Deserialize<Client>(input); 
            ClientList.Add(newCustomer);
            string newjson = JsonSerializer.Serialize(ClientList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, newjson);

        }
        public void Replace(Client  client) 
        {
            string input = ToJson(client);
            Client newCustomer = JsonSerializer.Deserialize<Client>(input);
            int index = ClientList.FindIndex(c => c.PersonalId == client.PersonalId);
            ClientList[index] = client;
            string newjson = JsonSerializer.Serialize(ClientList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, newjson);

        }
        private int GenerateUniquePassword()
        {
            Random random = new Random();
            int randomPassword = random.Next(1000, 10000);
            Passwords = ReadPasswordsFromFile();
            while (Passwords.Contains(randomPassword))
            {
                randomPassword = random.Next(1000, 10000);
            }
            
            Passwords.Add(randomPassword);
            return randomPassword;
        }
       
        private static List<Client> Parse(string _filePath) 
        {
            string JSONtoString = File.ReadAllText(_filePath);

            if (string.IsNullOrEmpty(JSONtoString))
            {
                return new List<Client>();
            }

            List<Client> result = JsonSerializer.Deserialize<List<Client>>(JSONtoString);

            if (result == null)
            {
                throw new FormatException("Invalid format while deserialization");
            }

            return result;
        }
        private Client CreateNewClient(string name, string surname)
        {
            Console.WriteLine("please enter your ID(should be 11 digit long):");
            string id = Console.ReadLine();
            
            while (true)
            {
                if (id.Length == 11)
                {
                    if (ClientList.Any(client => client.PersonalId == id))
                    {
                        Console.WriteLine("This ID belongs to someone else, you should input your unique ID. Please try again");
                        id = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("ID should contain 11 letters:");
                    id = Console.ReadLine();
                }
            }

            Console.WriteLine("Okay, let start by creating your account:");
            Console.WriteLine("enter in what currency you want your account to be('gel','usd','euro'):");
            string currency = Console.ReadLine();
            while (currency.ToLower() != "gel" && currency.ToLower() != "usd" && currency.ToLower() != "euro")
            {
                Console.WriteLine("enter in what currency you want your account to be('gel','usd','euro'):");
                currency = Console.ReadLine();
            }

            Money money = new Money(currency, 0);
            
            string accountNumber = GenerateUniqueAccountNumber();
            Account account = new Account(accountNumber, money);

            Console.WriteLine($"Account created successfully! your account number is: {accountNumber}");
            int uniquePassword = GenerateUniquePassword();
            Console.WriteLine($"your unique password is: {uniquePassword}");
            AppendTextToFile(uniquePassword.ToString()); 
            return new Client(name, surname, id, uniquePassword, account);
        }

        private string GenerateUniqueAccountNumber()
        {
            Random random = new Random();
            string accountNumber;
            do
            {
                accountNumber = "";
                for (int i = 0; i < 21; i++)
                {
                    accountNumber += random.Next(10); 
                }
            } while (!IsUnique(accountNumber));

            using (StreamWriter writer = File.AppendText(_accountNumbersFilePath))
            {
                writer.WriteLine(accountNumber);
            }

            return accountNumber;
        }

        private bool IsUnique(string accountNumber)
        {
            if (!File.Exists(_accountNumbersFilePath))
            {
                return true; 
            }

            string[] existingAccountNumbers = File.ReadAllLines(_accountNumbersFilePath);

            foreach (string existingNumber in existingAccountNumbers)
            {
                if (existingNumber == accountNumber)
                {
                    return false; 
                }
            }

            return true; 
        }
        private Client ReceiveClient()
        {
            Console.WriteLine("please enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("please enter your surname");
            string surname = Console.ReadLine();
            ClientList = Parse(_filePath); 
            
            if (!ClientList.Any()) 
            {
                return CreateNewClient(name, surname);
            } else
            { 
                
                if (ClientList.Any(client => client.Name == name && client.SurName == surname))
                {
                    Console.WriteLine($"the client named {name} {surname} is already exists. If you are visiting the bank for the first time enter 1. in other case, enter 2");
                    int answer = int.Parse(Console.ReadLine());
                    while (answer != 1 && answer != 2)
                    {
                        Console.WriteLine("please enter 1 if you are visiting our bank for the first time, 2 otherwise.any other input can't be accebted");
                    }
                    if (answer == 1)
                    {
                        return CreateNewClient(name, surname);
                    }
                    else
                    {
                        int incorrectPasswordCounter = 0; 
                        Console.WriteLine("please enter your ID(should be 11 digit long):");
                        string id = Console.ReadLine();
                        bool cycleStopper = true;
                        Passwords = ReadPasswordsFromFile();
                        while (cycleStopper)
                        {
                            if (id.Length == 11)
                            {
                                if (ClientList.Any(client => client.PersonalId == id))
                                {

                                    Client clientWithGivenId = ClientList.FirstOrDefault(client => client.PersonalId == id);
                                    if(clientWithGivenId.Name == name && clientWithGivenId.SurName == surname) 
                                    {
                                        Console.WriteLine("please enter your unique password(4 digit)");
                                        int pass = int.Parse(Console.ReadLine());
                                        if (incorrectPasswordCounter >= 3) 
                                        {
                                            Console.WriteLine("Your password has been entered incorrectly three times. You cant enter...");
                                            return null;
                                        }
                                        else if (Passwords.Contains(pass) && clientWithGivenId.Password == pass && incorrectPasswordCounter < 3) 
                                        {
                                            Console.WriteLine("correct password! thank you for your patience");
                                            return ClientList.FirstOrDefault(client => client.PersonalId == id);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Password is incorrect. Please try again");
                                            incorrectPasswordCounter++;
                                        }
                                    } else
                                    {
                                        Console.WriteLine("This ID belongs to someone else, you should input your unique ID. Please try again");
                                        id = Console.ReadLine();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("this ID is not registered. try again");
                                    id = Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("ID should contain 11 letters:");
                                id = Console.ReadLine();
                            }
                        }
                        

                        if (incorrectPasswordCounter >= 3) { return null; } 
                                                                        
                    }
                }
                else
                {
                    return CreateNewClient(name, surname);
                }
                
            }
            
            
            return null;
        }
    }
}
