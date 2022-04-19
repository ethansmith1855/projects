using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel_Console_App
{
    class Program
    {

        public static List<Cell> Cells = new List<Cell>();

        static void Main(string[] args)
        {
            CreateCells();
            Console.ReadKey();
        }

        static void NavigateCells()
        {

        }

        static string ChangeCell(string value)
        {
            int length = value.Length;
            int remainder = 7 - length;

            string cell = "|" + value;
            for (int i = 0; i < remainder; i++)
            {
                cell += " ";
            }
            cell += "|";
            return cell;
        }

        static void CreateCells()
        {
            string cell = "|       |";
            int height = 5;
            int width = 5;

            char letter = 'A';

            for (int i = 1; i < height; i++)
            {
                letter = 'A';
                for (int t = 0; t < width; t++)
                {
                    Cells.Add(new Cell(letter, i, $"{letter} {i}"));
                    Console.Write(ChangeCell($"{letter} {i}"));
                    letter++;
                }
                Console.WriteLine();
            }
        }
    }
}
