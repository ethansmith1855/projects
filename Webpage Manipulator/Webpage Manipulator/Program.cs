﻿using System;
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

        public static List<string> Links = new List<string>();

        static void Main(string[] args)
        {

            string input = Console.ReadLine();
            //FindLink(input);
            SearchGoogle(input);
            
            Console.ReadKey();
        }

        public static void FindLink(string responceFromServer)
        {
            bool linkOpen = false;
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
                            Links.Add(link);
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
            foreach (var lin in Links)
            {
                Console.WriteLine(lin);
            }
            Console.ReadKey();
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

        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

    }
}
