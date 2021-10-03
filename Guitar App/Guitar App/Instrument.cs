using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_App
{
    class Instrument
    {
        public Dictionary<int, string> Notes = new Dictionary<int, string>();
        public string Name;
        
        public Instrument(string name, Dictionary<int, string> notes)
        {
            this.Name = name;
            this.Notes = notes;
        }
    }
}
