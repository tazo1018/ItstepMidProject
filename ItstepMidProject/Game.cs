using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    internal class Game
    {
        
        private List<Gamer> GamerList { get; set; } = new List<Gamer>();
        public readonly string _filePath = "C:\\Users\\user\\source\\repos\\ItstepMidProject\\ItstepMidProject\\gamersList.csv";
        public void start()
        {
            Console.WriteLine("Welcome to Number Guessing Game! Good luck!");
            var gamer = createGamer();
            
            Console.WriteLine("please select mode form these three options: 'easy', 'medium', 'hard'");
            string mode = Console.ReadLine();
            
            while (!(mode.Equals("easy", StringComparison.OrdinalIgnoreCase)) && !(mode.Equals("medium", StringComparison.OrdinalIgnoreCase)) && !(mode.Equals("hard", StringComparison.OrdinalIgnoreCase)))
            {

                Console.WriteLine("please select mode from these three options: 'easy', 'medium', 'hard'");
                    mode = Console.ReadLine();
                
            }

            var tuple = GenerateNumber(mode);
            int i;
            for (i = 0; i < 10; i++)
            {
                int numberToGuess = tuple.Item1;
                int guessedByGamer = gamer.takeAGuess();

                if (numberToGuess == guessedByGamer)
                {
                    Console.WriteLine("you guessed the number");
                    gamer.Point += tuple.Item2;
                    break;
                } else if (numberToGuess > guessedByGamer)
                {
                    Console.WriteLine("number you need to guess is higher than that");
                } else
                {
                    Console.WriteLine("number you need to guess is lower than that");
                }
            }
            if (i < 10)
            {
                Console.WriteLine("congrats, you won the game!");
            } else
            {
                Console.WriteLine("you lost...");
            }
           
            var existingGamer = GamerList.FirstOrDefault(g => g.GamerId == gamer.GamerId);
            if (existingGamer == null)
            {
                GamerList.Add(gamer);
            }
            

            var sortedGamerList = GamerList.OrderByDescending(gamer => gamer.Point);
            ClearCsvFile(_filePath);

            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {

                writer.WriteLine("ID, Name, Surname, Points");

                
                foreach (var gmr in sortedGamerList)
                {
                    writer.WriteLine(ToCSV(gmr));
                }
            }
        }
        static void ClearCsvFile(string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath)){}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing CSV file: {ex.Message}");
            }
        }

        private Gamer createGamer()
        {
            Console.WriteLine("please enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("please enter your surname");
            string surname =  Console.ReadLine();
            GamerListFromCSV();
            foreach (var gamer in GamerList)
            {
                if (gamer.Name == name && gamer.Surname == surname)
                {
                    Console.WriteLine($"the player named {name} {surname} is already exists. If you are playing the game first time enter 1. in other case, enter 2");
                    int answer = int.Parse(Console.ReadLine());
                    if (answer == 1)
                    {
                        return (new Gamer(name, $"{surname}"));
                    } else
                    {
                        Console.WriteLine("please enter your ID:");
                        int id = int.Parse(Console.ReadLine());
                        bool hasGamerWithId = GamerList.Any(g => g.GamerId == id && g.Name == name && g.Surname == surname);
                        while(!hasGamerWithId)
                        {
                            Console.WriteLine("this ID belongs to other person. please enter your correct ID:");
                            id = int.Parse(Console.ReadLine());
                            hasGamerWithId = GamerList.Any(g => g.GamerId == id && g.Name == name && g.Surname == surname);
                        }
                        
                        return GamerList.FirstOrDefault(g => g.GamerId == id);
                    }
                }
            }
            return (new Gamer(name, surname));
        }
        private static string ToCSV(Gamer model) => $"{model.GamerId},{model.Name},{model.Surname},{model.Point}";

        public void GamerListFromCSV()
        {
            GamerList = File
                .ReadAllLines(_filePath)
                .Skip(1)
                .Select(Parse)
                .ToList();
        }

        private static Gamer Parse(string input)
        {
            var data = input.Split(',');

            if (data.Length != 4)
            {
                throw new FormatException("Incorrect format");
            }

            var result = new Gamer();
            result.GamerId = int.Parse(data[0]);
            result.Name = data[1];
            result.Surname = data[2];
            result.Point = int.Parse(data[3]);
            

            return result;
        }
        private (int,int) GenerateNumber(String mode)
        {
            int generatedNumber = 0;
            int pointToAdd = 0;
            if (mode.Equals("easy", StringComparison.OrdinalIgnoreCase))
            {
                EasyMode easy = new EasyMode();
                generatedNumber = easy.GenerateNumber();
                pointToAdd = easy.point;
                Console.WriteLine("number to guess lies between 1-15. good luck! ");
            } else if (mode.Equals("medium", StringComparison.OrdinalIgnoreCase))
            {
                MediumMode medium = new MediumMode();
                generatedNumber = medium.GenerateNumber();
                pointToAdd = medium.point;
                Console.WriteLine("number to guess lies between 1-25. good luck! ");
            }
            else if (mode.Equals("hard", StringComparison.OrdinalIgnoreCase))
            {
                HardMode hard = new HardMode();
                generatedNumber = hard.GenerateNumber();
                pointToAdd = hard.point;
                Console.WriteLine("number to guess lies between 1-50. good luck! ");
            }
            Console.WriteLine("the number you need to guess has chosen!");
            return (generatedNumber,pointToAdd);
        }
    }
}
