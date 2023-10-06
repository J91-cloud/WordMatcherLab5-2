using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WordMatcher___Lab5
{
    class Program
    {
        // Authors: Jessy and Hristo

        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Before we begin do you want to be served in English, Yes/No");
                String answer = Console.ReadLine();

                if (answer == "yes")
                {
                    Console.WriteLine("Enter scrambled word(s) manually or as a file: F - file / M - manual");

                    String option = Console.ReadLine() ?? throw new Exception("String is empty");

                    switch (option.ToUpper())
                    {
                        case "F":
                            Console.WriteLine("Enter full path including the file name: ");
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        case "M":

                            Console.WriteLine(Properties.strings.MCase);
                            string userInput = Console.ReadLine();

                            List<string> matchedWords = ExecuteScrambledWordsManualEntryScenario(userInput);

                            Console.WriteLine("Matched words:");
                            foreach (string word in matchedWords)
                            {
                                Console.WriteLine(word);
                            }

                            break;
                        default:
                            Console.WriteLine("The entered option was not recognized.");
                            break;
                    }

                    Console.ReadLine();


                }
                else if (answer == "no")
                {
                    {
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-CA");
                        Console.WriteLine(Properties.strings.Instructions);

                        String option = Console.ReadLine() ?? throw new Exception("String is empty");

                        switch (option.ToUpper())
                        {
                            case "F":
                                Console.WriteLine(Properties.strings.FCase);
                                ExecuteScrambledWordsInFileScenario();
                                break;
                            case "M":
                                Console.WriteLine(Properties.strings.MCase);
                                string userInput = Console.ReadLine();

                                List<string> matchedWords = ExecuteScrambledWordsManualEntryScenario(userInput);

                                Console.WriteLine(Properties.strings.MatchedWords);
                                foreach (string word in matchedWords)
                                {
                                    Console.WriteLine(word);
                                }
                               
                                break;
                            default:
                                Console.WriteLine(Properties.strings.Error);
                                break;
                        }
                    }

                    Console.ReadLine();


                }
                else Console.WriteLine("Wrong");

                


            }
            catch (Exception ex)
            {
                Console.WriteLine("The program will be terminated." + ex.Message);

            }
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            var filename = Console.ReadLine();
            string[] scrambledWords = _fileReader.Read(filename);
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static List<string> ExecuteScrambledWordsManualEntryScenario(string userInput)
        {
            string[] words = userInput.Split(',');

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }

            List<string> matchedWords = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = i + 1; j < words.Length; j++)
                {
                    if (words[i].Length == words[j].Length && words[i].SequenceEqual(words[j]))
                    {
                        matchedWords.Add(words[i]);
                        break;
                    }
                }
            }

            return matchedWords;

        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            //read the list of words from the system file. 
            string[] wordList = _fileReader.Read("wordlist.txt");

            //call a word matcher method to get a list of structs of matched words.
            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);
        }
    }
}
