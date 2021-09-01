using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyWord
{
    class Program
    {

        public static List<string> keyWordList = new List<string>();
        public static List<string> NotUsedKeyWords = new List<string>(); 

        static void Main(string[] args)
        {
            AskForKeyWords();
            Clear();
            Console.WriteLine("What would you like to scan?");   // This is what im checking for keywords in
            string input = Console.ReadLine();
            SearchForKeyWord(input);
        }

        static void AskForKeyWords()
        {
            Console.WriteLine("What key words are you looking for?");
            Console.WriteLine("Use spaces to separate them.");
            Console.WriteLine();
            string keyWords = Console.ReadLine();
            keyWords += " ";    //Adds a space for last word
            string word = "";
            foreach (var letter in keyWords)
            {
                if (letter != ' ')
                {
                    word += letter;
                }
                else
                {
                    keyWordList.Add(word);
                    NotUsedKeyWords.Add(word);
                    word = "";
                }
            }
        }

        static void WriteWord(string word)
        {
            string wordCopy = "";
            bool keyWordFound = false;
            foreach (var keyWord in keyWordList)
            {
                keyWordFound = false;

                wordCopy = word;

                foreach (var letter in word)
                {
                    if (Char.IsLetter(letter) == false)
                    {
                        word = word.Remove(word.Length - 1, 1);
                    }
                }

                if (word == keyWord)
                {
                    keyWordFound = true;
                    break;
                }
                else
                {
                    keyWordFound = false;
                }
            }
            if (keyWordFound == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(wordCopy);
                Console.ForegroundColor = ConsoleColor.White;
                NotUsedKeyWords.Remove(word);
            }
            else
            {
                Console.Write(wordCopy);
            }
            Console.Write(" ");
        }

        static void SearchForKeyWord(string input)
        {
            Clear();
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            string word = "";
            input += " ";       // for last word
            foreach (var letter in input)
            {
                if (letter != ' ')
                {
                    word += letter;
                }
                else
                {
                    WriteWord(word);
                    word = "";
                }
            }

            //not used list

            Console.WriteLine("\n\n\n");
            if (NotUsedKeyWords.Count != 0)
            {
                Console.WriteLine("These keywords were not used");
            }
            else
            {
                Console.WriteLine("All keywords were used");
            }
            Console.WriteLine();
            foreach (var keyWord in NotUsedKeyWords)
            {
                Console.Write(keyWord);
                Console.Write(" ");
            }

            Console.ReadKey();
        }

        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

    }
}
