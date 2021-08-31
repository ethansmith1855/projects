using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QuestGame
{
    class Program
    {

        static public string player = "0";
        static public int playerX = 10;
        static public int playerY = 10;
        static public bool keepGoing = true;

        static public int MainMapWidth = 73;
        static public int MainMapHeight = 22;
        static public char[,] MainMap = new char[MainMapWidth, MainMapHeight];

        static public int Health = 100;
        static public int Coins = 0;

        static public string[] unMovableBlocks = { "X", "|", "_" };

        static void Main(string[] args)
        {
            Console.WindowWidth = 75;
            Console.WindowHeight = 25;
            Console.CursorVisible = false;
            Play();
        }

        static void Play()
        {
            LoadMap();
            
            DrawMap();
            DrawPlayer(playerX, playerY);
            //CreateEnemy();

            while (keepGoing)
            {
                var input = Console.ReadKey(true);

                //if hit enemy
                if (MainMap[playerX, playerY] == '&')
                {
                    Health -= 10;
                    DrawMap();
                }

                //if dead 
                if (Health <= 0)
                {
                    Clear();
                    Console.WriteLine("You have died!");
                    Console.ReadKey();
                    keepGoing = false;
                }

                if (!Console.KeyAvailable)
                {
                    switch (input.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (MainMap[playerX, playerY - 1] != 'X' && MainMap[playerX, playerY - 1] != '|' && MainMap[playerX, playerY - 1] != '_')
                            {
                                Console.SetCursorPosition(playerX, playerY);
                                Console.Write(MainMap[playerX, playerY]);
                                DrawPlayer(playerX, --playerY);
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (MainMap[playerX, playerY + 1] != 'X' && MainMap[playerX, playerY + 1] != '|' && MainMap[playerX, playerY + 1] != '_')
                            {
                                Console.SetCursorPosition(playerX, playerY);
                                Console.Write(MainMap[playerX, playerY]);
                                DrawPlayer(playerX, ++playerY);
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (MainMap[playerX + 1, playerY] != 'X' && MainMap[playerX + 1, playerY] != '|' && MainMap[playerX + 1, playerY] != '_')
                            {
                                Console.SetCursorPosition(playerX, playerY);
                                Console.Write(MainMap[playerX, playerY]);
                                DrawPlayer(++playerX, playerY);
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (MainMap[playerX - 1, playerY] != 'X' && MainMap[playerX - 1, playerY] != '|' && MainMap[playerX - 1, playerY] != '_')
                            {
                                Console.SetCursorPosition(playerX, playerY);
                                Console.Write(MainMap[playerX, playerY]);
                                DrawPlayer(--playerX, playerY);

                            }
                            break;
                        default:
                            break;
                    }
                    Thread.Sleep(100);

                }
            }
        }

        static void CreateEnemy(int x, int y, string enemy)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(enemy);
        }

        static void CreateEnemy(int amount)
        { 
            for (int i = 0; i < amount; i++)
            {
                //Set Enemy Cordanets
                int x = 0;
                int y = 0;

                Random rnd = new Random();
                bool newSpace = false;
                while (!newSpace)
                {
                    x = rnd.Next(1, 72);
                    y = rnd.Next(2, 20);

                    if (MainMap[x, y] == ' ')
                    {
                        newSpace = true;
                    }
                }

                //Draw Enemy
                Console.SetCursorPosition(x, y);
                Console.Write('&');
            }
        }

        static void DrawMenu()
        {
            Console.WriteLine($"            Coins: {Coins}              Health: {Health}%");
        }

        static void CreateEnemy()
        {
            //Set Enemy Cordanets
            int x = 0;
            int y = 0;

            Random rnd = new Random();
            bool newSpace = false;
            while (!newSpace)
            {
                x = rnd.Next(1, 72);
                y = rnd.Next(2, 20);

                if (MainMap[x, y] == ' ')
                {
                    newSpace = true;
                }
            }

            //Draw Enemy
            MainMap[x, y] = '&';
            Console.SetCursorPosition(x, y);
            Console.Write('&');
        }

        static void EnemyFollow()
        {
            if (MainMap[playerX, playerY] == '&')
            {
                Health -= 10;
                DrawMap();
            }
        }

        static void DrawMap()
        {
            for (int x = 0; x < MainMapWidth; x++)
            {
                for (int y = 0; y < MainMapHeight; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(MainMap[x, y]);
                }
            }
            DrawMenu();
        }

        static void DrawPlayer(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(player);
        }

        static void LoadMap()
        {
            string fileData = File.ReadAllText(@"..\..\MainMap.txt");
            int i = 0;

            foreach (string row in fileData.Split('\n'))
            {
                int j = 0;
                foreach (char value in row)
                {
                    if (value!='\r') 
                        MainMap[j, i] = value;
                    j++;
                }
                i++;
            }
        }

        static void TimerCallback(object o)
        {
            Clear();
            Console.WriteLine(DateTime.Now);
            GC.Collect();
        }



        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

    }
}
