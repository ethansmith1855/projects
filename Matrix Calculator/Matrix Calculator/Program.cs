using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Calculator
{
    class Program
    {

        public static List<Variable> variables = new List<Variable>();               //m:row n:column
        public static Dictionary<int, int> subscript = new Dictionary<int, int>();   //mn , value
        public static char currentLetter = 'A';

        static void Main(string[] args)
        {
            GetVariable();
            variables.Add(new Variable("A", subscript));
        }

        static void GetVariable()
        {
            Clear();
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
        }

        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
    }
}
