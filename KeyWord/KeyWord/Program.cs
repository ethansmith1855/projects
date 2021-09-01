using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyWord
{
    class Program
    {

        //DOES NOT WORK WITH PUNCTUATION            ## WORKING ON IT

        public static List<string> keyWordList = new List<string>();

        static void Main(string[] args)
        {
            AskForKeyWords();
            Clear();
            Console.WriteLine("What would you like to scan?");   // This is what im checking for keywords in
            string input = Console.ReadLine();
            SearchForKeyWord(input);
        }

        static void AskForKeyWords()   //Getting KeyWords
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
                    word = "";
                }
            }
        }

        static void WriteWord(string word)  //writes each word indivitually
        {
            bool keyWordFound = false;
            foreach (var keyWord in keyWordList)
            {
                keyWordFound = false;

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
                Console.Write(word);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write(word);
            }
            Console.Write(" ");
        }

        static void SearchForKeyWord(string input)  //makes words to search from
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
            Console.ReadKey();
        }

        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

    }
}
