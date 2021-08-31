using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_App
{
    class Program
    {
        //Poop
        public static string[] e = { "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#", "E" };
        public static string[] B = { "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        public static string[] G = { "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G" };
        public static string[] D = { "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D" };
        public static string[] A = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A" };
        public static string[] E = { "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#", "E" };

        public static List<string[]> Strings = new List<string[]>();

        static void Main(string[] args)
        {
            Strings.Add(e);
            Strings.Add(B);
            Strings.Add(G);
            Strings.Add(D);
            Strings.Add(A);
            Strings.Add(E);

            NoteFinder();
        }

        static void NoteFinder()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                Clear();
                Console.WriteLine("What Note Would You Like To Find?");
                string note = Console.ReadLine();
                GuitarFretBoard(note);
            }
        }

        static void GuitarFretBoard(string n)
        {

            Console.WriteLine("   1   2   3   4   5   6   7   8   9   10  11  12");

            n = n.ToUpper();

            foreach (var x in Strings)
            {
                int c = 0;
                foreach (var note in x)
                {
                    if (note == n && note.Length == n.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (c == 0)
                    {
                        Console.Write(note);
                        Console.Write("|");
                    }
                    else if (note.Length != 2)
                    {
                        Console.Write("-");
                        Console.Write(note);
                        Console.Write("-|");
                    }
                    else
                    {
                        Console.Write("-");
                        Console.Write(note);
                        Console.Write("|");
                    }
                    c++;
                }
                Console.WriteLine();
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
