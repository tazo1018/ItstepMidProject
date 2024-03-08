using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hangman
{
    internal class Hangman
    {
        private List<Player> PlayerList { get; set; } = new List<Player>();
        private readonly List<string> words = new List<string>
        {
            "apple", "banana", "orange", "grape", "kiwi",
            "strawberry", "pineapple", "blueberry", "peach", "watermelon"
        };
        public readonly string _filePath = "C:\\Users\\user\\source\\repos\\ItstepMidProject\\Hangman\\XMLFile1.xml";

        public void start()
        {
            DrawHangMan(7);
            Console.WriteLine("Welcome player. Hope you are ready to play world-famous game - Hangman. Lets start, good luck!");
            var player = createPlayer();
            bool guessedOrNot = false;
            string wordToguess = generateWordToGuess();
            string hiddenWordToGuess = HiddenWordToGuess(wordToguess);
            string resultAfterGuessing = wordToguess;
            List<string> guessedLetterArray = new List<string>();
            Console.WriteLine($"The word you need to guess has {wordToguess.Length} letters!");
            Console.WriteLine(hiddenWordToGuess);
            int numberOfMistakes = 0;
            int i;
            for (i = 0; i < 6; i++)
            {
                Console.WriteLine("press 1 to guess only one letter, press 2 to guess the whole word and get extra points!()");
                
                
                int choice = 0; 
                while(choice != 1 && choice != 2)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                        if(choice != 1 && choice != 2)
                        {
                            Console.WriteLine("Invalid input! Please press 1 to guess only one letter, press 2 to guess the whole word and get extra points!().");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input! Please press 1 to guess only one letter, press 2 to guess the whole word and get extra points!().");
                    }
                }
                

                if (choice == 1)
                {
                    string guessedLetter = player.GuessSingleLetter();
                    if (guessedLetterArray.Contains(guessedLetter))
                    {
                        Console.WriteLine("you already guessed that letter...");
                        i--;
                        Console.WriteLine($"Letters you tried to guess: {string.Join(", ", guessedLetterArray)}, {guessedLetter}");
                    } else
                    {
                       
                        var myTuple = revealHiddenLetter(guessedLetter, wordToguess, hiddenWordToGuess);
                        resultAfterGuessing = myTuple.Item1;
                        if (!myTuple.Item2)
                        {
                            numberOfMistakes++;
                        }

                        DrawHangMan(numberOfMistakes);
                        Console.WriteLine($"Letters you tried to guess: {string.Join(", ", guessedLetterArray)}, {guessedLetter}");

                        
                        hiddenWordToGuess = resultAfterGuessing;
                        guessedLetterArray.Add(guessedLetter);
                        if(resultAfterGuessing.Equals(wordToguess, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("congrats, you won the game");
                            player.Point += 1;
                            guessedOrNot = true;
                            break;
                        }
                        Console.WriteLine(resultAfterGuessing);
                    }
                    
                } else if (choice == 2)
                {
                    string guessedWord = player.GuesstheWord();
                    while(guessedWord.Length != wordToguess.Length){
                  
                        Console.WriteLine($"the word should contain {wordToguess.Length} letters.");
                        guessedWord = player.GuesstheWord();
                    } 
                    
                    if (!(guessedWord.Equals(wordToguess, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine("You lost...");
                        guessedOrNot = true;
                        DrawHangMan(6);
                        break;
                    } else
                    {
                        Console.WriteLine("Congrats, you guessed the whole word!");
                        player.Point += (6 - i);
                        guessedOrNot = true;
                        break;
                        
                    }
                }

            }
            if (guessedOrNot || (resultAfterGuessing.Equals(HiddenWordToGuess(wordToguess), StringComparison.OrdinalIgnoreCase)))
            {
                
            } else
            {
                Console.WriteLine("Now, its time to guess the whole word...");
                Console.WriteLine(resultAfterGuessing);
                string guessedWord = player.GuesstheWord();
                while (guessedWord.Length != wordToguess.Length)
                {
                    Console.WriteLine($"the word should contain {wordToguess.Length} letters.");
                    guessedWord = player.GuesstheWord();
                }
                if (guessedWord.Equals(wordToguess, StringComparison.OrdinalIgnoreCase))
                {
                    player.Point += 1;
                    Console.WriteLine("Congrats, you won the game!");
                } else
                {
                    DrawHangMan(6);
                    Console.WriteLine("You lost...");
                }
                
            }
            
            var existingGamer = PlayerList.FirstOrDefault(p => p.PlayerId == player.PlayerId);
            if (existingGamer == null)
            {
                PlayerList.Add(player);
            }

            var sortedPlayerList = PlayerList.OrderByDescending(gamer => gamer.Point).ToList();
            DeleteXmlContent(_filePath);

            WritePlayerListToXml(sortedPlayerList, _filePath);    
        }

        private string generateWordToGuess()
        {
            Random random = new Random();
            int randomIndex = random.Next(words.Count);
            return words[randomIndex];
        }

        private string HiddenWordToGuess(string wordToGuess)
        {
            string hidden = string.Empty;
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                hidden = hidden + "*";
            }
            return hidden;
        }
        private (string, bool) revealHiddenLetter(string letter,string wordToGuess, string hiddenWord)
        {
            string result = string.Empty;
            bool hasGuessedRigth = false;

            string[] hiddenLetterArray = hiddenWord.Select(c => c.ToString()).ToArray(); 
            string[] letterArray = wordToGuess.Select(c => c.ToString()).ToArray(); 
            string hiddenWordForComparing = hiddenWord;
            for (int i = 0 ;i < letterArray.Length; i++)
            {
                if (letterArray[i] == letter)
                {
                    hiddenLetterArray[i] = letter;
                }
            }
            result = string.Join("", hiddenLetterArray);
            if (result.Equals(hiddenWordForComparing,StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Sorry, the letter you chose does not belong to this word...");
                hasGuessedRigth = false;
            }
            else
            {
                Console.WriteLine("Congrats, you guessed it right!");
                hasGuessedRigth = true;

            }

            return (result, hasGuessedRigth);

        }

        static void DeleteXmlContent(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            doc.DocumentElement.RemoveAll();

            doc.Save(filePath);
        }

        static void WritePlayerListToXml(List<Player> playerList, string filePath)
        {
            var sortedPlayerList = playerList.OrderByDescending(player => player.Point);

            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("players");
            doc.AppendChild(root);

            foreach (var player in sortedPlayerList)
            {
                XmlElement playerElement = doc.CreateElement("player");
                XmlElement idElement = doc.CreateElement("ID");
                idElement.InnerText = player.PlayerId.ToString();
                playerElement.AppendChild(idElement);
                XmlElement nameElement = doc.CreateElement("name");
                nameElement.InnerText = player.Name;
                playerElement.AppendChild(nameElement);

                XmlElement surnameElement = doc.CreateElement("surname");
                surnameElement.InnerText = player.Surname;
                playerElement.AppendChild(surnameElement);

                XmlElement pointElement = doc.CreateElement("point");
                pointElement.InnerText = player.Point.ToString();
                playerElement.AppendChild(pointElement);

                root.AppendChild(playerElement);
            }

            doc.Save(filePath);
        }
        public static List<Player> LoadPlayersFromXml(string filePath)
        {
            List<Player> players = new List<Player>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNodeList playerNodes = doc.SelectNodes("/players/player");

                foreach (XmlNode playerNode in playerNodes)
                {
                    string name = playerNode.SelectSingleNode("name").InnerText;
                    string surname = playerNode.SelectSingleNode("surname").InnerText;
                    int points = int.Parse(playerNode.SelectSingleNode("point").InnerText);
                    int id = int.Parse(playerNode.SelectSingleNode("ID").InnerText);

                    Player player = new Player(id, name, surname);
                    player.Point = points;

                    players.Add(player);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading players from XML: {ex.Message}");
            }

            return players;
        }
        private Player createPlayer()
        {
            Console.WriteLine("please enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("please enter your surname");
            string surname = Console.ReadLine();
            PlayerList = LoadPlayersFromXml(_filePath);
            foreach (var gamer in PlayerList)
            {
                if (gamer.Name == name && gamer.Surname == surname)
                {
                    Console.WriteLine($"the player named {name} {surname} is already exists. If you are playing the game first time enter 1. in other case, enter 2");
                    int answer = int.Parse(Console.ReadLine());
                    if (answer == 1)
                    {
                        var maxId1 = PlayerList.Max(x => x.PlayerId);
                        return new Player(maxId1 + 1, name, surname);
                    }
                    else
                    {
                        Console.WriteLine("please enter your ID:");
                        int id = int.Parse(Console.ReadLine());
                        bool hasGamerWithId = PlayerList.Any(g => g.PlayerId == id && g.Name == name && g.Surname == surname);
                        while (!hasGamerWithId)
                        {
                            Console.WriteLine("this ID belongs to other person. please enter your correct ID:");
                            id = int.Parse(Console.ReadLine());
                            hasGamerWithId = PlayerList.Any(g => g.PlayerId == id && g.Name == name && g.Surname == surname);
                        }

                        return PlayerList.FirstOrDefault(g => g.PlayerId == id);
                    }
                }
            }
            var maxId = PlayerList.Max(x => x.PlayerId);
            return (new Player(maxId + 1, name, surname));
        }

        static void readingLinesFromTxt(int start, int end)
        {
            string[] allLines = File.ReadAllLines("C:\\Users\\user\\source\\repos\\ItstepMidProject\\Hangman\\TextFile1.txt");

            for (int k = start; k < end; k++) 
            {
                Console.WriteLine(allLines[k]);
            }
        }
        static void DrawHangMan(int numberOfMistakes)
        {
            string[] allLines = File.ReadAllLines("C:\\Users\\user\\source\\repos\\ItstepMidProject\\Hangman\\TextFile1.txt");

            switch (numberOfMistakes)
            {

                case 0: break;
                case 1:
                    readingLinesFromTxt(1,10);
                    break;
                case 2:
                    readingLinesFromTxt(11, 22);
                    break;
                case 3:
                    readingLinesFromTxt(23, 33);
                    break;
                case 4:
                    readingLinesFromTxt(33, 44);
                    break;
                case 5:
                    readingLinesFromTxt(44, 55);

                    break;
                case 6:
                    readingLinesFromTxt(55, 66);
                    break;
                case 7:
                    readingLinesFromTxt(68, 76);
                    break;
                default:
                    Console.WriteLine("you can't try more...");
                    break;
            }
            
        }
    }
}
