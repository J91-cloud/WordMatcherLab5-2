using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordMatcher___Lab5;

namespace WordMatcher___Lab5
{
    class Program
    {
        /*
         
         so when you make a change you got to "Git Changes" , write a comment about the change and then "Commit All".

        To pull the new changes you go to "View" , "Git repository" and then you "pull"
         */
        // Authors: Jessy and Hristo

        // Hristo try to Pull this

        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();

        static void Main(string[] args)
        {
            
            try
            {
                bool validInput = false;
                Console.WriteLine("Before we begin, do you want to be served in English? Yes/No");
                string answer = Console.ReadLine();

                while (!validInput)
                {
                    if (answer.Equals("yes", StringComparison.OrdinalIgnoreCase)) // this class is for, case does not matter, found online 
                    {

                        Console.WriteLine("Enter scrambled word(s) manually or as a file: F - file / M - manual");
                        
                        WordMatcher___Lab5.Properties.strings.FCase.ReverseString();

                        string option;
                        do
                        {
                            option = Console.ReadLine()?.ToUpper();
                            if (option == "F")
                            {
                                Console.WriteLine("Enter the full path including the file name: ");
                                ExecuteScrambledWordsInFileScenario();
                                break;
                            }
                            else if (option == "M")
                            {
                                Console.WriteLine(Properties.strings.MCase);
                                string userInput = Console.ReadLine();

                                List<string> matchedWords = ExecuteScrambledWordsManualEntryScenario(userInput);

                                Console.WriteLine("Matched words:");
                                foreach (string word in matchedWords)
                                {
                                    Console.WriteLine(word);
                                }
                                break;


                            }

                            else
                            {
                                Console.WriteLine("The entered option was not recognized, please try again with the same prompt");
                            }

                            
                        } while (true);

                        
                        validInput = true;
                    }
                    else if (answer.Equals("no", StringComparison.OrdinalIgnoreCase))
                    {
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-CA");
                        Console.WriteLine(Properties.strings.Instructions);

                        string option;
                        do
                        {
                            option = Console.ReadLine()?.ToUpper();
                            if (option == "F")
                            {
                                Console.WriteLine(Properties.strings.FCase);
                                ExecuteScrambledWordsInFileScenario();
                                break;
                            }
                            else if (option == "M")
                            {
                                Console.WriteLine(Properties.strings.MCase);
                                string userInput = Console.ReadLine();

                                List<string> matchedWords = ExecuteScrambledWordsManualEntryScenario(userInput);

                                Console.WriteLine(Properties.strings.MatchedWords);
                                foreach (string word in matchedWords)
                                {
                                    Console.WriteLine(word);
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine(Properties.strings.Error);
                            }
                        } while (true);

                        validInput = true;
                        
                    }
                    else
                    {
                        Console.WriteLine("Wrong input. Please try again.");
                        answer = Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The program will be terminated. " + ex.Message);
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

            string[] wordList = _fileReader.Read("wordlist.txt"); // Load words from the file

            for (int i = 0; i < words.Length; i++)
            {
                char[] sortedWord = words[i].ToCharArray();
                Array.Sort(sortedWord);

                for (int j = 0; j < wordList.Length; j++)
                {
                    char[] sortedOtherWord = wordList[j].ToCharArray();
                    Array.Sort(sortedOtherWord);

                    if (sortedWord.Length == sortedOtherWord.Length && AreArraysEqual(sortedWord, sortedOtherWord))
                    {
                        matchedWords.Add(words[i]);
                        matchedWords.Add(wordList[j]);
                        break;
                    }
                }
            }

            return matchedWords;
        }


        private static bool AreArraysEqual(char[] array1, char[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            //read the list of words from the system file. 
           // string[] wordList = _fileReader.Read("wordlist.txt");
           Console.WriteLine("Enter a file path to compare with");
            var file2 = Console.ReadLine();
            string[] wordList = _fileReader.Read(file2);



            //call a word matcher method to get a list of structs of matched words.
            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            // Display the matched words
            Console.WriteLine("Matched Words:");
            foreach (var matchedWord in matchedWords)
            {
                Console.WriteLine($"Scrambled Word: {matchedWord.ScrambledWord}, Word: {matchedWord.Word}");

            }
        }
    }
}
