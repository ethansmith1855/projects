using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PracticeTyping
{
    class Program
    {

        public static List<string> Words = new List<string>();
        public static Dictionary<int, string> Sentences = new Dictionary<int, string>();

        public static int sentencesCount = 0;

        static void Main(string[] args)
        {
            ManipulateTextFile();
            StartingTest();
        }

        static void StartingTest()
        {

            int errors = 0;
            int typerX = 0;

            for (int i = 0; i < 1; i++)
            {
                SentenceMaker();
            }

            foreach (var sentence in Sentences.Values)
            {
                Console.Clear();
                typerX = 0;
                foreach (var letter in sentence)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write(sentence);
                    Console.SetCursorPosition(typerX, 1);
                    ConsoleKeyInfo input = Console.ReadKey(true);
                    if (input.KeyChar == letter)
                    {
                        Console.Write(letter);
                    }
                    else
                    {
                        errors++;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(input.KeyChar);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    typerX++;
                }
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"You mad {errors} errors.");

            Console.ReadKey();
        }

        static void SentenceMaker()
        {
            Random rnd = new Random();

            int sentenceLength = rnd.Next(7, 14);
            int count = 0;
            List<string> shuffle = new List<string>();
            Shuffle(Words);
            string sentence = "";

            foreach (var word in Words)
            {
                if (sentenceLength >= count)
                {
                    sentence += word;
                    if (sentenceLength != count)
                    {
                        sentence += " ";
                    }
                    else
                    {
                        sentence += ".";
                    }
                    count++;
                }
            }
            sentencesCount++;
            Sentences.Add(sentencesCount, sentence);
        }

        static void Shuffle(List<string> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        static void ManipulateTextFile()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\PracticeTyping\PracticeTyping\WordFile.txt");
            string word = "";
            foreach (var letter in text)
            {
                if (letter != '\n')
                {
                    word += letter;
                }
                else
                {
                    word = word.Substring(0, word.Length - 1);
                    Words.Add(word);
                    word = "";
                }
            }
        }
    }
}
