using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Calculator
{
    class Variable
    {
        public Dictionary<int, int> Subscript = new Dictionary<int, int>();
        public string VarName;

        public Variable()
        {

        }

        public Variable(string varName, Dictionary<int, int> subsript)
        {
            this.VarName = varName;
            this.Subscript = subsript;
        }
    }
}
