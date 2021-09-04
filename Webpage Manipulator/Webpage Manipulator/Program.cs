using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace Webpage_Manipulator
{
    class Program
    {

        public static Dictionary<int, string> Links = new Dictionary<int, string>();
        public static Dictionary<int, string> GoodLinks = new Dictionary<int, string>();
        public static List<string> Question = new List<string>();
        public static Dictionary<int, string> ImageLinks = new Dictionary<int, string>();
        public static List<string> SpecificTerm = new List<string>();

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            PutQuestionIntoList(input);
            //FindLink(input);
            SearchGoogle(input);
            
            Console.ReadKey();
        }

        public static void OpenLink()
        {
            Console.WriteLine("Which link would you like to open?");
            string input = Console.ReadLine();
            int number;
            Int32.TryParse(input, out number);
            Process.Start(Links[number]);
            string spider = Links[number];
            ImageLinks.Clear();
            SearchGoogle(spider);
            //FindLink(spider);
        }

        public static void FindGoodLink()
        {
            int x = 0;
            bool open = false;
            string word = "";
            int linkCount = 0;
            bool firstTime = true;
            foreach (var link in Links.Values)
            {
                foreach (var qWord in Question)
                {
                    int wordcount = qWord.Length;
                    foreach (var letter in link)
                    {
                        if (x <= wordcount)
                        {
                            char qWordChar = qWord[x];
                            if (qWordChar == letter)
                            {
                                open = true;
                                if (firstTime == true)
                                {
                                    firstTime = false;
                                }
                                else
                                {
                                    x++;
                                }
                            }
                            else
                            {
                                open = false;
                                firstTime = true;
                                x = 0;
                            }
                        }
                        if (x == wordcount)
                        {
                            if (open == true)
                            {
                                foreach (var goodLink in GoodLinks)
                                {
                                    if (link == goodLink.Value)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        linkCount++;
                                        GoodLinks.Add(linkCount, link);
                                        open = false;
                                        firstTime = true;
                                    }
                                }
                            }
                            else
                            {
                                x = 0;
                            }
                        }
                    }
                }
            }
        }
        public static void FindSpecificLink(Dictionary<int, string> dic, string specificTerm)
        {
            int x = 0;
            bool open = false;
            string word = "";
            int linkCount = 0;
            bool firstTime = true;
            SpecificTerm.Clear();
            specificTerm += " ";
            SpecificTerm.Add(specificTerm);

            foreach (var link in Links.Values)
            {
                foreach (var qWord in SpecificTerm)
                {
                    int wordcount = qWord.Length;
                    foreach (var letter in link)
                    {
                        char qWordChar = qWord[x];
                        if (x < wordcount - 1)
                        {
                            if (qWordChar == letter)
                            {
                                open = true;
                                if (firstTime != true)
                                {
                                    firstTime = false;
                                }
                                else
                                {
                                    x++;
                                }
                            }
                            else
                            {
                                open = false;
                                firstTime = true;
                                x = 0;
                            }
                        }
                        if (x == wordcount - 1)
                        {
                            if (open == true)
                            {
                                foreach (var goodLink in GoodLinks)
                                {
                                    if (link == goodLink.Value)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        linkCount++;
                                        dic.Add(linkCount, link);
                                        open = false;
                                        firstTime = true;
                                    }
                                }
                                if (GoodLinks.Count == 0)
                                {
                                    linkCount++;
                                    dic.Add(linkCount, link);
                                    open = false;
                                    firstTime = true;
                                }
                            }
                            else
                            {
                                x = 0;
                            }
                        }
                    }
                }
            }
        }

        public static void FindSpecificLink(Dictionary<int, string> dic, List<string> key)  //WORKING ON     --A Better Virsion Of FindGoodLink
        {
            int x = 0;
            bool open = false;
            string word = "";
            int linkCount = 0;
            bool firstTime = true;

            foreach (var link in Links.Values)
            {
                foreach (var qWord in key)
                {
                    int wordcount = qWord.Length;
                    foreach (var letter in link)
                    {
                        char qWordChar = qWord[x];
                        if (x < wordcount - 1)
                        {
                            if (qWordChar == letter)
                            {
                                open = true;
                                if (firstTime != true)
                                {
                                    firstTime = false;
                                }
                                else
                                {
                                    x++;
                                }
                            }
                            else
                            {
                                open = false;
                                firstTime = true;
                                x = 0;
                            }
                        }
                        if (x == wordcount - 1)
                        {
                            if (open == true)
                            {
                                foreach (var goodLink in GoodLinks)
                                {
                                    if (link == goodLink.Value)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        linkCount++;
                                        dic.Add(linkCount, link);
                                        open = false;
                                        firstTime = true;
                                    }
                                }
                                if (GoodLinks.Count == 0)
                                {
                                    linkCount++;
                                    dic.Add(linkCount, link);
                                    open = false;
                                    firstTime = true;
                                }
                            }
                            else
                            {
                                x = 0;
                            }
                        }
                    }
                }
            }
        }           

        public static void PutQuestionIntoList(string question)
        {
            string word = "";
            question += " ";
            foreach (var letter in question)
            {
                if (letter != ' ')
                {
                    word += letter;
                }
                else
                {
                    Question.Add(word);
                    word = "";
                }
            }
        }

        public static void FindLink(string responceFromServer)
        {
            Links.Clear();
            bool linkOpen = false;
            int linkCount = 0;
            string link = "";
            bool open = false;
            string word = "";
            bool firstLetterInLink = true;
            int x = 0;
            foreach (var item in responceFromServer)
            {
                if (linkOpen == true)
                {
                    if (firstLetterInLink == true)
                    {
                        firstLetterInLink = false;
                    }
                    else
                    {
                        if (item != '"')
                        {
                            link += item;
                        }
                        else
                        {
                            linkCount++;
                            Links.Add(linkCount, link);
                            link = "";
                            linkOpen = false;
                            firstLetterInLink = true;
                        }
                    }
                }
                if (linkOpen == false)
                {
                    if (open == true && x < 5)
                    {
                        word += item;
                        x++;
                    }
                    if (x == 4)
                    {
                        if (word == "ref=")
                        {
                            open = false;
                            word = "";
                            x = 0;
                            linkOpen = true;
                        }
                        else
                        {
                            open = false;
                            word = "";
                            x = 0;
                        }
                    }
                    if (item == 'h')
                    {
                        open = true;
                    }
                }
            }
            Clear();
            Console.WriteLine(word);
            FindSpecificLink(ImageLinks, "img");
            foreach (var lin in ImageLinks)
            {
                Console.WriteLine(lin.Key + ") " + lin.Value);
            }
            OpenLink();
        }

        public static void SearchGoogle(string query)
        {
            string responseFromServer;
            Process.Start("http://search.yahoo.com/search?q=" + query);
            WebRequest request = WebRequest.Create("http://search.yahoo.com/search?q=" + query);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }

            FindLink(responseFromServer);

            // Close the response.
            //response.Close();
        }

        public static void SearchGoogle(string query, int one)
        {
            string responseFromServer;
            Process.Start(query);
            WebRequest request = WebRequest.Create(query);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }

            FindLink(responseFromServer);

            // Close the response.
            //response.Close();
        }

        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

    }
}
