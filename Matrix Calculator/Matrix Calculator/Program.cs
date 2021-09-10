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

        //FIX- needs better spacing with bigger numbers

        static void Main(string[] args)
        {
            MainControl();
        }

        static void GetMainInput()  //NOT USING
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
                        //currentLetter++;
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

        static void MainControl()
        {
            bool switchStatement = false;
            bool keepGoing = true;
            while (keepGoing)
            {
                switchStatement = false;
                Clear();
                Console.WriteLine("Type help for help");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        GetVariable();
                        switchStatement = true;
                        break;
                    case "2":
                        ShowAllVariables();
                        switchStatement = true;
                        break;
                    case "help":
                        HelpPage();
                        switchStatement = true;
                        break;
                    default:
                        break;
                }
                if (switchStatement == false)
                {
                    CheckMainInput(input);
                }
            }
        }

        static void CheckMainInput(string input)
        {
            foreach (var letter in input)
            {
                bool letterIsVar = false;
                foreach (var variable in variables)
                {
                    if (letterIsVar == true)
                    {
                        if (input.Contains('>'))
                        {
                            TransferVariable(input);
                            break;
                        }
                        //switch (letter)
                        //{
                        //    case '>':
                        //        TransferVariable(input);
                        //        break;
                        //    default:
                        //        Clear();
                        //        Console.WriteLine($"{input} does not exist");
                        //        Console.ReadKey();
                        //        break;
                        //}
                    }
                    foreach (var varname in variable.VarName)
                    {
                        if (letter == varname)
                        {
                            letterIsVar = true;
                        }
                    }
                }
            }
        }

        static void TransferVariable(string input)
        {
            string orginVar = "";
            string toVar = "";
            bool firstVar = true;
            foreach (var letter in input)
            {
                if (Char.IsLetter(letter))
                {
                    if (firstVar == true)
                    {
                        orginVar = letter.ToString();
                        firstVar = false;
                    }
                    else
                    {
                        toVar = letter.ToString();
                    }
                }
            }
            var toVarLetter = "";
            toVarLetter = variables.Find(x => x.VarName == toVar).VarName;
            var orginSubscript = variables.Find(x => x.VarName == orginVar).Subscript;
            variables.Remove(variables.Where(x => x.VarName == toVar).FirstOrDefault());
            variables.Add(new Variable(toVarLetter, orginSubscript));
        }

        static void HelpPage()
        {
            Clear();
            Console.WriteLine("1) Add variable");
            Console.WriteLine("2) Show variables");
            Console.ReadKey();
        }

        static void GetVariable()
        {
            Clear();
            //Dictionary cant be a public var in this case
            Dictionary<int, int> subscript = new Dictionary<int, int>();   //mn , value
            subscript.Clear();
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

            //Get variable name 
            Clear();
            Console.WriteLine("Variable name: ");
            var varName = Console.ReadLine();
            varName = varName.ToUpper();
            //If var is already in the variable list, it replaces it
            variables.Remove(variables.Where(x => x.VarName == varName).FirstOrDefault());

            variables.Add(new Variable(varName, subscript));
        }

        static void ShowAllVariables()
        {
            Clear();
            //NEED TO ORDER BY ALPHEBET HERE

            foreach (var variable in variables)
            {
                int biggestNumber = 0;
                string spaces = "";
                foreach (var sub in variable.Subscript.Values) //spaces numbers better
                {
                    string subS = sub.ToString();
                    int valueSpots = 0;
                    foreach (var item in subS)
                    {
                        valueSpots++;
                    }
                    if (valueSpots > biggestNumber)
                    {
                        biggestNumber = valueSpots;
                    }
                }
                for (int i = 0; i < biggestNumber; i++)
                {
                    spaces += " ";
                }

                Console.WriteLine();
                Console.WriteLine($"{variable.VarName}: ");

                foreach (var sub in variable.Subscript)
                {
                    string subS = sub.Key.ToString();
                    subS = subS.Substring(1);
                    if (subS == "1")
                    {
                        Console.WriteLine();
                        Console.Write(sub.Value + spaces);
                    }
                    else
                    {
                        Console.Write(sub.Value + spaces);
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
