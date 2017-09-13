using System;
using System.Collections.Generic;

namespace spellcheck
{
    class Permutations
    {

        public static IEnumerable<string> GetDeletions(string word)
        {
            for(int position = 0; position < word.Length; position++)
            {
                yield return word.Substring(0, position) + word.Substring(position + 1);
            }
        }

        public static IEnumerable<string> GetSwaps(string word)
        {
            for(int position = 0; position < word.Length - 1; position++)
            {
                foreach(char c in alphabet)
                {                    
                    yield return word.Substring(0, position) + word[position + 1] + word[position] + word.Substring(position + 2);
                }
            }
        }

        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static IEnumerable<string> GetInsertions(string word)
        {
            for(int position = 0; position < word.Length; position++)
            {
                foreach(char c in alphabet)
                {
                    yield return word.Substring(0, position) + c + word.Substring(position);
                }
            }
        }

        public static IEnumerable<string> GetReplacements(string word)
        {
            for(int position = 0; position < word.Length; position++)
            {
                foreach(char c in alphabet)
                {
                    yield return word.Substring(0, position) + c + word.Substring(position + 1);
                }
            }
        }

        public static IEnumerable<string> GetEdits(string word, 
                                                   int distance, 
                                                   params Func<string, IEnumerable<string>>[] methods)
        {
            foreach(Func<string, IEnumerable<string>> method in methods)
            {
                foreach(string edit in method(word))
                {
                    if(distance <= 1)
                    {
                        yield return edit;
                    }
                    else
                    {
                        foreach(string reedit in GetEdits(edit, distance - 1, methods))
                        {
                            yield return reedit;
                        } 
                    }
                }
            }
        }
    }
}
