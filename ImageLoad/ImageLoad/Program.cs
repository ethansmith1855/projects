using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;

namespace ImageLoad
{
    class Program
    {

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();


        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);
            DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);

            //maximize screen size
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);

            var screens = System.Windows.Forms.Screen.AllScreens;
            int countScreens = 0;
            foreach (var screen in screens)  // 0 based
            {
                if (countScreens == 1)
                {
                    var bounds = screen.Bounds;
                    int screenX = bounds.X;
                    int screenY = bounds.Y;
                    //Console.SetWindowPosition(screenX, screenY);  FIX
                }
                countScreens++;
            }
            Console.CursorVisible = false;
            ReadImage(@"C:\Users\smith\OneDrive\Documents\GitHub\projects\ImageLoad\Picture\RGB.jpg");
            Console.ReadKey();
        }

        static void ReadImage(string file)
        {

            Bitmap bitmap = new Bitmap(file);
            var bitmapWidth = bitmap.Width;
            var bitmapHeight = bitmap.Height;
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            List<Color> colors = new List<Color>();

            for (int y = 0; y < bitmapHeight; y++)
            {
                for (int x = 0; x < bitmapWidth; x++)
                {
                    if (x % 8 == 0)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);
                        colors.Add(pixelColor);
                    }
                }
            }


            List<Color> useColors = new List<Color>();
            int count = 0;

            foreach (var color in colors)
            {
                if (bitmapWidth < windowWidth)
                {
                    if (count != bitmapWidth)
                    {
                        ShowRGBColor(color, color);
                        Thread.Sleep(0);
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }

                if (count % 8 == 0)
                {
                    useColors.Add(color);
                }
                count++;
            }

            //foreach (var ite in colors)
            //{
            //    SetScreenColorsApp.SetScreenColors(ite, Color.Magenta) ;
            //    Console.WriteLine($"Name: {ite.Name}, R: {ite.R}, G: {ite.G}, B: {ite.B}, Hue: {ite.GetHue()}, Saturation: {ite.GetSaturation()}, Brightness: {ite.GetBrightness()} ");
            //    Thread.Sleep(1);
            //}

            foreach (var item in useColors)
            {
                
            }

        }

        static void DrawFlag()
        {
            int x = 0;
            int y = 0;
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            while (y + 1 != windowHeight)
            {
                x++;
                if (x == windowWidth)
                {
                    x = 0;
                    y++;
                }
                if (y % 2 == 0)
                {
                    ShowRGBColor(Color.CadetBlue, Color.CadetBlue);
                }
                else
                {
                    ShowRGBColor(Color.Blue, Color.Blue);
                }
            }
        }

        static void ShowRGBColor(Color screenTextColor, Color screenBackgroundColor)
        {
            char square = (char)166;
            int irc = SetScreenColorsApp.SetScreenColors(screenTextColor, screenBackgroundColor);
            Debug.Assert(irc == 0, "SetScreenColors failed, Win32Error code = " + irc + " = 0x" + irc.ToString("x"));

            Console.Write(square);
           
        }
    }
}
