using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTyping
{
    class Program
    {

        public static List<string> Words = new List<string>();
        public static Dictionary<int, string> Sentences = new Dictionary<int, string>();

        public static int sentencesCount = 0;

        static void Main(string[] args)
        {
            AddWords();
            SentenceMaker();
            SentenceMaker();
            SentenceMaker();
            SentenceMaker();
        }

        static void SentenceMaker()
        {
            Random rnd = new Random();

            int sentenceLength = rnd.Next(6, 12);
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

        static void AddWords()
        {
            Words.Add("the");
            Words.Add("is");
            Words.Add("happy");
            Words.Add("sad");
            Words.Add("bad");
            Words.Add("hello");
            Words.Add("why");
            Words.Add("when");
            Words.Add("where");
            Words.Add("how");
            Words.Add("cow");
            Words.Add("can");
            Words.Add("very");
            Words.Add("mad");
            Words.Add("sand");
            Words.Add("beach");
            Words.Add("water");
            Words.Add("milk");
            Words.Add("cherry");
            Words.Add("berry");
            Words.Add("school");
            Words.Add("work");
            Words.Add("car");
            Words.Add("truck");
            Words.Add("trench");//25
        }

    }
}
