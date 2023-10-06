using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMatcher___Lab5
{
    class WordMatcher
    {
        public List<MatchedWord> Match(string[] scrambledWords, string[] wordList) //used to store the matched pair of words. 
        {
            List<MatchedWord> matchedWords = new List<MatchedWord>();

            foreach (string scrambledWord in scrambledWords) 
            {
                foreach (string word in wordList)
                {
                    if (scrambledWord.Equals(word))                     //used to compare each word between arrays
                    {
                        MatchedWord matchedWord = BuildMatchedWord(scrambledWord, word);
                        matchedWords.Add(matchedWord);
                    }
                }
            }

            MatchedWord BuildMatchedWord(string scrambledWord, string word) //if the words betweeen scramble array and wordlist are matched they go in here. 
            {
                MatchedWord matchedWord = new MatchedWord();
                matchedWord.ScrambledWord = scrambledWord;
                matchedWord.Word = word;
                return matchedWord;
                //return new MatchedWord();  // Delete this line when done.
            }

            return matchedWords;
        }
    }
}
