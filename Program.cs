using System;
using System.Collections.Generic;

namespace spellcheck
{
    class Program
    {
        static HashSet<string> getWords()
        {
            string[] allWords = System.IO.File.ReadAllLines(@"words.txt");
            return new HashSet<string>(allWords);
        }

        static void FindMatches(string word, HashSet<string> dictionary)
        {
            int maxFinds = 5;
            var foundWords = new HashSet<string>();
            IEnumerable<string> edits = Permutations.GetEdits(word, 
                                                    2,
                                                    Permutations.GetDeletions,
                                                    Permutations.GetSwaps,
                                                    Permutations.GetInsertions,
                                                    Permutations.GetReplacements);
            foreach(string edit in edits)
            {
                if(dictionary.Contains(edit) && !foundWords.Contains(edit))
                {
                    Console.Out.WriteLine(edit);
                    foundWords.Add(edit);
                    if(foundWords.Count == maxFinds)
                    {
                        return;
                    }
                }
            }

        }

        static void Main(string[] args)
        {
            string word = args[0];
            HashSet<string> dictionary = getWords();
            if(dictionary.Contains(word)) {
                Console.Out.WriteLine("Correct!");
                return;
            }
            FindMatches(word, dictionary);
        }
    }
}
