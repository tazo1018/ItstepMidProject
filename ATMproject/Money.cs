using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMproject
{

    public class Money
    {
        public string Currency { get; set; }
        public int Balance { get; set; }
        public Money()
        {

        }
        public Money(string currency, int balance)
        {
            if (currency.ToString().Length == 3)
            {
                Currency = currency;
            }
            else { Console.WriteLine("currency should contain 3 digits"); }
            if (balance >= 0)
            {
                Balance = balance;
            }
            else { Console.WriteLine("balance should be positive"); }
        }
        public Money(string currency, int balance, string othercurrency)
        {
            if (currency.ToString().Length == 3)
            {
                Currency = currency;
            }
            else { Console.WriteLine("currency should contain 3 digits"); }
            if (balance >= 0)
            {
                Balance = balance;
            }
            else { Console.WriteLine("balance should be positive"); }
        }
        public static Money operator -(Money first, Money second)
        {
            Money result = new Money();
            if (first.Currency == second.Currency)
            {
                result.Currency = second.Currency;
                if (first.Balance >= second.Balance)
                {
                    result.Balance = first.Balance - second.Balance;
                }
                else { Console.WriteLine("can't be substracted"); }
            }
            else
            {
                Console.WriteLine("they are not on a same currency");
            }
            return result;
        }

        public static Money operator +(Money first, Money second)
        {
            Money result = new Money();
            if (first.Currency == second.Currency)
            {
                result.Currency = second.Currency;
                result.Balance = first.Balance + second.Balance;
            }
            else
            {
                Console.WriteLine("They are not the same Currency"); 
            }
            return result;
        }

        public static Money operator *(Money first, Money second)
        {
            Money result = new Money();
            if (first.Currency == second.Currency)
            {
                result.Currency = second.Currency;
                result.Balance = first.Balance * second.Balance;
            }
            else
            {
                Console.WriteLine("They are not the same Currency");
            }
            return result;
        }

        public static Money operator /(Money first, Money second)
        {
            Money result = new Money();
            if (first.Currency == second.Currency)
            {
                result.Currency = second.Currency;
                result.Balance = first.Balance / second.Balance;
            }
            else
            {
                Console.WriteLine("They are not the same Currency");
            }
            return result;
        }

        public static bool operator <(Money first, Money second)
        {
            if (first.Currency == second.Currency)
            {
                return first.Balance < second.Balance;
            }
            else
            {
                Console.WriteLine("They are not the same Currency");
                return false;
            }

        }

        public static bool operator >(Money first, Money second)
        {
            if (first.Currency == second.Currency)
            {
                return first.Balance > second.Balance;
            }
            else
            {
                Console.WriteLine("They are not the same Currency");
                return false;
            }

        }

        public static bool operator ==(Money first, Money second)
        {
            if (first.Currency == second.Currency)
            {
                return first.Balance == second.Balance;
            }
            else
            {
                Console.WriteLine("They are not the same Currency");
                return false;
            }

        }

        public static bool operator !=(Money first, Money second)
        {
            if (first.Currency == second.Currency)
            {
                return first.Balance != second.Balance;
            }
            else
            {
                Console.WriteLine("They are not the same Currency");
                return false;
            }

        }
    }

}
