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
            GetInput();
            variables.Add(new Variable("A", subscript));
        }

        static void GetInput()
        {
            Clear();
            int rowCount = 0;
            string input = "";
            bool keepGoing = true;
            int rowSize = 0;
            int columnSize = 0;
            string number = "";
            bool countingRows = true;
            while (keepGoing)
            {
                Console.WriteLine("Please type row x column");
                Console.WriteLine();
                Console.WriteLine($"");
                input = Console.ReadLine();
                int gap = 0;
                //read input
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
            }
        }

        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
    }
}
