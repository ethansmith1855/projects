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
        public static List<string> Nouns = new List<string>();
        public static List<string> Verbs = new List<string>();
        public static List<string> Subjects = new List<string>();
        public static List<string> Adjectives = new List<string>();
        public static List<string> Prepositions = new List<string>();
        public static Dictionary<int, string> Sentences = new Dictionary<int, string>();

        public static int letterCount = 0;
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

            for (int i = 0; i < 3; i++)
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
                    letterCount++;
                }
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"You mad {errors} errors out of {letterCount} letters.");

            Console.ReadKey();
        }

        static void SentenceMaker()
        {
            Random rnd = new Random();

            int sentenceLength = rnd.Next(7, 14);
            int count = 0;
            List<string> shuffle = new List<string>();
            ShuffleAllLists();
            string sentence = "";

            sentence += Subjects.First();
            sentence += " ";
            sentence += Verbs.First();
            sentence += " ";
            sentence += Adjectives.First();
            sentence += " ";
            sentence += Prepositions.First();
            sentence += " ";
            sentence += Nouns.First();
            sentence += ".";
            //foreach (var word in Words)
            //{
            //    if (sentenceLength >= count)
            //    {
            //        sentence += word;
            //        if (sentenceLength != count)
            //        {
            //            sentence += " ";
            //        }
            //        else
            //        {
            //            sentence += ".";
            //        }
            //        count++;
            //    }
            //}
            sentencesCount++;
            Sentences.Add(sentencesCount, sentence);
        }

        static void ShuffleAllLists()
        {
            Shuffle(Words);
            Shuffle(Nouns);
            Shuffle(Prepositions);
            Shuffle(Subjects);
            Shuffle(Adjectives);
            Shuffle(Verbs);
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

        static void ReadFile(string file, List<string> list)
        {
            string word = "";
            foreach (var letter in file)
            {
                if (letter != '\n')
                {
                    word += letter;
                }
                else
                {
                    word = word.Substring(0, word.Length - 1);
                    list.Add(word);
                    word = "";
                }
            }
        }

        static void ManipulateTextFile()
        {
            string adjectivesText = System.IO.File.ReadAllText(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\PracticeTyping\PracticeTyping\TextFiles\Adjectives.txt");
            string nounsText = System.IO.File.ReadAllText(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\PracticeTyping\PracticeTyping\TextFiles\Nouns.txt");
            string verbsText = System.IO.File.ReadAllText(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\PracticeTyping\PracticeTyping\TextFiles\Verbs.txt");
            string subjectsText = System.IO.File.ReadAllText(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\PracticeTyping\PracticeTyping\TextFiles\Subjects.txt");
            string wordsText = System.IO.File.ReadAllText(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\PracticeTyping\PracticeTyping\TextFiles\WordFile.txt");
            string prepositionsText = System.IO.File.ReadAllText(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\PracticeTyping\PracticeTyping\TextFiles\Prepositions.txt");
            ReadFile(prepositionsText, Prepositions);
            ReadFile(wordsText, Words);
            ReadFile(subjectsText, Subjects);
            ReadFile(verbsText, Verbs);
            ReadFile(nounsText, Nouns);
            ReadFile(adjectivesText, Adjectives);
        }
    }
}
