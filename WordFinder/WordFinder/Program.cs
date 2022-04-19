using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;

namespace WordFinder
{
    class Program
    {

        public static string input;

        public static List<string> Words = new List<string>();
        public static List<char> Characters = new List<char>();
        public static List<char> RealLetters = new List<char>();
        public static List<string> Results = new List<string>();

        public static List<char> NoLetters = new List<char>();
        public static List<char> YesLetter = new List<char>();

        public static Dictionary<int, string> NumberedResults = new Dictionary<int, string>();
        public static List<Dictionary<int, string>> Page = new List<Dictionary<int, string>>();
        public static Dictionary<int, Dictionary<int, string>> Pages = new Dictionary<int, Dictionary<int, string>>();

        static void Main(string[] args)
        {
            OnStart();

            //SendEmail();

            bool keepGoing = true;
            while (keepGoing)
            {
                WordleInfo();
                //Results.Clear();
                //Characters.Clear();
                //RealLetters.Clear();
                //NoLetters.Clear();
                //YesLetter.Clear();
                //GetSearchResult();
                //ShowResult();
            }
        }

        static void WordleInfo()
        {
            int wordLength = 0;

            bool showRules = true;
            bool correctResponce = false;

            while (!correctResponce)
            {
                Clear();
                Console.WriteLine("Do you know how to play wordle?");
                string responce = Console.ReadLine();
                switch (responce.ToLower())
                {
                    case "no":
                        showRules = true;
                        correctResponce = true;
                        break;
                    case "yes":
                        showRules = false;
                        correctResponce = true;
                        break;
                    default:
                        Clear();
                        Console.WriteLine("Please respond with yes or no!");
                        Console.ReadKey();
                        break;
                }
            }

            if (showRules == true)
            {
                //Rules
                Clear();
                Console.WriteLine("Welcome to Wordle!");
                Console.WriteLine("Press Any Key To Continue...");
                Console.ReadKey();

                Clear();
                Console.WriteLine("The rules are simple...");
                Console.ReadKey();

                Clear();
                Console.WriteLine("When a letter is highlighted ");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Green");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(", that means that the letter is in the correct spot...");
                Console.ReadKey();

                Clear();
                Console.WriteLine("When a letter is highlighted ");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Yellow");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(", that means that the letter is in the word, but not in the right spot...");
                Console.ReadKey();

                Clear();
                Console.WriteLine("And When a letter does not have a highlight, that means that the letter is not in the word...");
                Console.ReadKey();

                Clear();
                Console.WriteLine("Lets Play!");
                Console.WriteLine("Press Any Key To Start The Game!");
                Console.ReadKey();
            }


            bool letterCountCorrect = false;

            //Get Game Info
            while (!letterCountCorrect)
            {
                Clear();
                Console.WriteLine("How many letters do you want in your word?");
                Console.WriteLine("Keep in mind, it can not be less than 3 or greater than 22.");
                string stringLetterCount = Console.ReadLine();
                if (Regex.IsMatch(stringLetterCount, @"^\d+$"))
                {
                    bool correctLength = true;

                    wordLength = Convert.ToInt32(stringLetterCount);
                    if (wordLength > 22)
                    {
                        Clear();
                        Console.WriteLine("The Number is too large!");
                        Console.ReadKey();
                        correctLength = false;
                    }
                    if (wordLength < 3)
                    {
                        Clear();
                        Console.WriteLine("The Number is too small!");
                        Console.ReadKey();
                        correctLength = false;
                    }
                    if (correctLength == true)
                    {
                        letterCountCorrect = true;
                    }


                }
                else
                {
                    Clear();
                    Console.WriteLine("Please Enter A Number!");
                    Console.ReadKey();
                }
            }

            PlayWordle(wordLength);

        }

        static void PlayWordle(int wordLength)
        {
            //Get Random Word
            Dictionary<int, string> useableWords = new Dictionary<int, string>();

            int wordCount = 0;

            foreach (var word in Words)
            {
                if (word.Length == wordLength)
                {
                    wordCount++;
                    useableWords.Add(wordCount, word);
                }
            }

            Random rnd = new Random();

            int randomWordNumber = rnd.Next(1, useableWords.Keys.Max());

            string wordle = useableWords[randomWordNumber];

            //Play Game
            Console.WriteLine(wordle);
            Console.ReadKey();
        }

        static void GetSearchResult()
        {
            //get input
            Clear();
            Console.WriteLine("Use A Hashtag To Show Unknown Letters");
            input = Console.ReadLine();

            Clear();
            Console.WriteLine("What letters does it not contain?");
            string notLetters = Console.ReadLine();

            Clear();
            Console.WriteLine("What letters does it contain?");
            string yesLetters = Console.ReadLine();

            int inputLength = input.Length;

            // contained letters in list
            foreach (var item in yesLetters)
            {
                if (item != ' ')
                {
                    YesLetter.Add(item);
                }
            }

            //put not letters in list
            foreach (var letter in notLetters)
            {
                if (letter != ' ')
                {
                    NoLetters.Add(letter);
                }
            }

            //put letters into list of chars
            foreach (var item in input)
            {
                Characters.Add(item);
                if (item != '#')
                {
                    RealLetters.Add(item);
                }
            }

            input = input.ToLower();

            int matchCount = 0;

            bool dontUse = false;

            foreach (var word in Words)
            {
                dontUse = false;
                matchCount = 0;
                if (word.Length == input.Length)
                {
                    for (int i = 0; i < inputLength; i++)
                    {
                        foreach (var letter in NoLetters)
                        {
                            if (word.Contains(letter))
                            {
                                dontUse = true;
                                break;
                            }
                        }
                        foreach (var item in YesLetter)
                        {
                            if (!word.Contains(item))
                            {
                                dontUse = true;
                                break;
                            }
                        }
                        if (input[i] != '#')
                        {
                            if (input[i] == word.ToLower()[i])
                            {
                                matchCount++;
                                if (matchCount == RealLetters.Count && dontUse == false)
                                {
                                    Results.Add(word);
                                }
                            }
                        }
                        if (RealLetters.Count == 0 && dontUse == false)
                        {
                            Results.Add(word);
                        }
                    }
                }
            }
        }

        static void ShowResult()
        {
            Clear();
            int windowHeight = Console.WindowHeight;

            string firstItem = "";
            bool isFirst = true;

            foreach (var item in Results)
            {
                if (isFirst == true)
                {
                    firstItem = item;
                    isFirst = false;
                }
                Console.WriteLine(item);
            }

            if (Results.Count <= windowHeight - 2)
            {
                //foreach (var item in Results)
                //{
                //    Console.WriteLine(item);
                //}
            }
            else
            {
                //int count = 0;
                //foreach (var item in Results)
                //{
                //    NumberedResults.Add(count, item);
                //    count++;
                //}

                //int pageCount = 0;
                //int resultCount = 0;
                //foreach (var numberedResult in NumberedResults)
                //{
                //    if (windowHeight - 2 >= resultCount)
                //    {
                //        Page.Add(numberedResult.Key, numberedResult.Value);
                //    }
                //    resultCount++;
                //}

            }

            Console.WriteLine();
            Console.WriteLine(Results.Count + " Results");
            Console.WriteLine(firstItem.Length + " letter words");
            Console.WriteLine($"Original: '{input}'");
            Console.WriteLine($"Does not contain '{ListNoLetters()}'");
            Console.WriteLine($"Contains '{ListYesLetters()}'");
            Console.ReadLine();
        }

        static string ListNoLetters()
        {
            string letters = "";
            foreach (var item in NoLetters)
            {
                if (item != ' ')
                {
                    letters += item;
                    letters += " ";
                }
            }
            if (letters.Length > 0)
            {
                letters = letters.Remove(letters.Length - 1, 1);
            }
            return letters;
        }

        static string ListYesLetters()
        {
            string letters = "";
            foreach (var item in YesLetter)
            {
                if (item != ' ')
                {
                    letters += item;
                    letters += " ";
                }
            }
            if (letters.Length > 0)
            {
                letters = letters.Remove(letters.Length - 1, 1);
            }
            return letters;
        }

        static void SendEmail()
        {
            Console.WriteLine("Mail To");
            MailAddress to = new MailAddress(Console.ReadLine());

            Console.WriteLine("Mail From");
            MailAddress from = new MailAddress(Console.ReadLine());

            MailMessage mail = new MailMessage(from, to);

            Console.WriteLine("Subject");
            mail.Subject = Console.ReadLine();

            Console.WriteLine("Your Message");
            mail.Body = Console.ReadLine();

            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            smtp.Host = "smtp.freesmtpservers.com";
            smtp.Port = 25;

            smtp.Credentials = new NetworkCredential(
                "ethansmithawesome@yahoo.com", "Sm2ith18!");
            smtp.EnableSsl = true;
            Console.WriteLine("Sending email...");
            smtp.Send(mail);















            //try
            //{

            //    SmtpClient mySmtpClient = new SmtpClient("smtp.freesmtpservers.com");

            //    // set smtp-client with basicAuthentication
            //    mySmtpClient.UseDefaultCredentials = false;
            //    System.Net.NetworkCredential basicAuthenticationInfo = new
            //        System.Net.NetworkCredential("username", "password");
            //    mySmtpClient.Credentials = basicAuthenticationInfo;

            //    // add from,to mailaddresses
            //    MailAddress from = new MailAddress("ethansmithawesome@yahoo.com", "EthanSmith");
            //    MailAddress to = new MailAddress("ethansmithawesome@icloud.com", "EthanSmith");
            //    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

            //    // add ReplyTo
            //    MailAddress replyTo = new MailAddress("reply@example.com");
            //    myMail.ReplyToList.Add(replyTo);

            //    // set subject and encoding
            //    myMail.Subject = "List Of Words";
            //    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            //    // set body-message and encoding
            //    myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
            //    myMail.BodyEncoding = System.Text.Encoding.UTF8;
            //    // text or html
            //    myMail.IsBodyHtml = true;

            //    mySmtpClient.Send(myMail);
            //}

            //catch (SmtpException ex)
            //{
            //    throw new ApplicationException
            //      ("SmtpException has occured: " + ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        static void OnStart()
        {
            string wordFile = System.IO.File.ReadAllText(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\WordFinder\WordFinder\Words.txt");
            ReadFile(wordFile, Words);
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
    }
}
