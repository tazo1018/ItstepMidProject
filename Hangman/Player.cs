using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Player
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Point = 0;
        public int PlayerId = -1;
        private static int nextCustomerId = 1;

        public Player()
        {
            PlayerId = nextCustomerId++;
        }
        public Player(string name, string surname)
        {
            PlayerId = nextCustomerId++;
            Name = name;
            Surname = surname;
        }

        public Player(int id,string name, string surname)
        {
            PlayerId = id;
            Name = name;
            Surname = surname;
        }

        public string GuessSingleLetter()
        {
            Console.WriteLine("enter a letter you would like to guess");
            string answer = Console.ReadLine();
            while (answer.Length != 1)
            {
                Console.WriteLine("You need to guess only one letter!");
                answer = Console.ReadLine();
            }
            return answer;
        }

        public string GuesstheWord()
        {
            Console.WriteLine("enter a word you would like to guess");
            return Console.ReadLine();
        }
    }
}
