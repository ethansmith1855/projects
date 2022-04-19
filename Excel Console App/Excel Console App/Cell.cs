using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel_Console_App
{
    class Cell
    {
        public char Letter;
        public int Number;
        public string Value;

        public Cell(char letter, int number, string value)
        {
            Letter = letter;
            Number = number;
            Value = value;
        }
    }
}
