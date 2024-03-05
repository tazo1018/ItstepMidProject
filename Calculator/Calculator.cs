using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Calculator
    {
        public Number number1 = new Number();
        public Number number2 = new Number();
        public string operation;
       
        public void start()
        {
            string wishToContinue = string.Empty;
            while (wishToContinue != "no")
            {
                Console.WriteLine("enter the first number");
                try
                {
                    number1.Value = float.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }
                Console.WriteLine("enter the operation(-,+,* or /)");
                operation = Console.ReadLine();
                while (true)
                {
                    if (operation == "-" || operation == "+" || operation == "/" || operation == "*")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("please input the valid operation");
                        operation = Console.ReadLine();
                    }
                }
                Console.WriteLine("enter the second number");
                try
                {
                    number2.Value = float.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }
                while (operation == "/" && number2.Value == 0)
                {
                    Console.WriteLine("You can't divide by zero. Please enter a non-zero second number:");
                    number2.Value = float.Parse(Console.ReadLine());
                }

                if (operation == "-")
                {
                    Console.WriteLine($"result is: {(number1 - number2).Value}");
                }
                else if (operation == "+")
                {
                    Console.WriteLine($"result is: {(number1 + number2).Value}");
                }
                else if (operation == "*")
                {
                    Console.WriteLine($"result is: {(number1 * number2).Value}");
                }
                else if (operation == "/")
                {
                    Console.WriteLine($"result is: {(number1 / number2).Value}");
                }
                Console.WriteLine("do you wish to continue calculating? (type 'yes' or 'no')");
                wishToContinue = Console.ReadLine();
                while (wishToContinue != "no" && wishToContinue != "yes") {
                    Console.WriteLine("please enter only 'yes' or 'no' for an answer");
                    Console.WriteLine("do you wish to continue calculating? (type 'yes' or 'no')");
                    wishToContinue = Console.ReadLine();
                }
            }
            
        }
    }
}
