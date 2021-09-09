using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Calculator
{
    class Program
    {

        public static List<Variable> variables = new List<Variable>();
        
        public static char currentLetter = 'A';

        static void Main(string[] args)
        {
            GetMainInput();
            ShowAllVariables();
        }

        static void GetMainInput()
        {
            bool keepGoing = true;
            GetVariable();
            while (keepGoing)
            {
                Clear();
                Console.WriteLine("y) get another variable");
                Console.WriteLine("n) stop getting variables");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "y":
                        currentLetter++;
                        GetVariable();
                        break;
                    case "n":
                        keepGoing = false;
                        break;
                    default:
                        Clear();
                        Console.WriteLine("Not an Option");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void GetVariable()
        {
            Clear();
            //Dictionary cant be a public var in this case
            Dictionary<int, int> subscript = new Dictionary<int, int>();   //mn , value
            subscript.Clear();
            int rowCount = 0;
            string input = "";
            int rowSize = 0;
            int columnSize = 0;
            string number = "";
            bool countingRows = true;
            Console.WriteLine("Please type row x column");
            Console.WriteLine();
            Console.WriteLine($"");
            input = Console.ReadLine();
            int gap = 0;
            //read input
            // get row and column size
            foreach (var letter in input.ToLower())
            {
                if (Char.IsWhiteSpace(letter))
                {
                    gap++;
                }
                if (Char.IsNumber(letter))
                {
                    if (countingRows == true)
                    {
                        number += letter;
                    }
                    else
                    {
                        if (gap != input.Length - 1) //subtracting the x
                        {
                            number += letter;
                        }
                        else
                        {
                            number += letter;
                            columnSize = Convert.ToInt32(number);
                            number = "";
                            gap++;
                        }

                    }
                    gap++;
                }
                if (letter == 'x')
                {
                    rowSize = Convert.ToInt32(number);
                    countingRows = false;
                    number = "";
                    gap++;
                }
            }
            //get subscript
            for (int r = 1; r < rowSize + 1; r++)
            {
                for (int c = 1; c < columnSize + 1; c++)
                {
                    Clear();
                    Console.WriteLine("Type in value");
                    Console.WriteLine($"{r},{c}: ");
                    string value = Console.ReadLine();
                    string subscriptTogether = r.ToString() + c.ToString();
                    subscript.Add(Convert.ToInt32(subscriptTogether), Convert.ToInt32(value));
                }
            }
            variables.Add(new Variable(currentLetter.ToString(), subscript));
        }

        static void ShowAllVariables()
        {
            Clear();
            foreach (var variable in variables)
            {

                Console.WriteLine();
                Console.WriteLine($"{variable.VarName}: ");

                foreach (var sub in variable.Subscript)
                {
                    string subS = sub.Key.ToString();
                    subS = subS.Substring(1);
                    if (subS == "1")
                    {
                        Console.WriteLine();
                        Console.Write(sub.Value + " ");
                    }
                    else
                    {
                        Console.Write(sub.Value + " ");
                    }
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
