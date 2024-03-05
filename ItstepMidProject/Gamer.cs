using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    internal class Gamer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Point = 0;
        public int GamerId = -1 ;
        private static int nextCustomerId = 1;

        public Gamer()
        {
            GamerId = nextCustomerId++;
        }
        public Gamer(string name, string surname)
        {
            GamerId = nextCustomerId++;
            Name = name;
            Surname = surname;
        }


        public int takeAGuess()
        {
            Console.WriteLine("Enter a number you would like to guess:");

            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
                // Recursively call takeAGuess() until valid input is entered
                return takeAGuess();
            }
        }


    }
}
