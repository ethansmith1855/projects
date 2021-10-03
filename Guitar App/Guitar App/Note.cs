using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_App
{
    class Note
    {
        public int Durration;
        public int Tone;
        public int Octive;

        public Note()
        {
        }

        public Note(int tone, int durration, int octive)
        {
            this.Tone = tone;
            this.Durration = durration;
            this.Octive = octive;
        }
    }
}
