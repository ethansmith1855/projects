using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadSpead
{
    class Program
    {

        static public float PerSec;
        static public float DownloadSize;

        static void Main(string[] args)
        {
            GetInfo();
            Calculate(PerSec, DownloadSize);
        }

        static void GetInfo()
        {
            //get info
            clear();
            Console.WriteLine("How many MB per second?   (MG)");
            string stringPerSec = Console.ReadLine();

            clear();
            Console.WriteLine("DownloadSize     (GB)");
            string stringDownloadSize = Console.ReadLine();

            //convert string to float
            PerSec = float.Parse(stringPerSec);
            DownloadSize = float.Parse(stringDownloadSize);
        }

        static void Calculate(float perSec, float downloadSize)
        {
            float perSecMB = 1000 / perSec;
            float time = (perSecMB * downloadSize) / 60;

            clear();
            Console.WriteLine(time.ToString());
            Console.ReadKey();
        }


        static void clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

    }
}
