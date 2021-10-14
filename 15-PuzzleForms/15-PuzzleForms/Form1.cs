using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15_PuzzleForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public Dictionary<int, int> Pieces = new Dictionary<int, int>();
        public bool won = false;
        public bool started = false;
        public int wins = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(1);
            ForEachButton();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(2);
            ForEachButton();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(3);
            ForEachButton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(4);
            ForEachButton();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(5);
            ForEachButton();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(6);
            ForEachButton();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(7);
            ForEachButton();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(8);
            ForEachButton();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CheckForOpenPiece(9);
            ForEachButton();
        }

        private void ForEachButton()
        {
            started = true;
            gameTimer.Start();
            CheckIfSolved();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            MixBoard();
            ClearLabel("won");
            gameTimer.Stop();
            started = false;
            DisableButtons("e");
            won = false;
        }

        public void DisableButtons(string action)
        {
            if (action == "e")
            {
                foreach (var item in Pieces)
                {
                    var labels = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + item.Key));
                    foreach (var label in labels)
                    {
                        label.Enabled = true;
                    }
                }
            }
            if (action == "d")
            {
                foreach (var item in Pieces)
                {
                    var labels = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + item.Key));
                    foreach (var label in labels)
                    {
                        label.Enabled = false;
                    }
                }
            }

        }

        public void ClearLabel(string name)
        {
            var labels = Controls.OfType<Label>().Where(x => x.Name.StartsWith(name));
            foreach (var label in labels)
            {
                label.Text = string.Empty;
            }

        }

        public void AddLabel(string text, int x, int y, string name)
        {
            Label label = new Label();
            label.Location = new Point(x, y);
            label.Text = text;
            label.Font = new Font("Arial", 10, FontStyle.Regular);
            label.AutoSize = true;
            label.Name = name; // the key for clearlabel
            this.Controls.Add(label);
        }

        private void MixBoard()
        {
            int[] pieces = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
            Random rnd = new Random();
            //var mixedPieces = pieces.OrderBy(x => rnd.Next()).ToArray();

            Pieces.Clear();             //gets list in order
            int count = 1;
            foreach (var piece in pieces)
            {
                Pieces.Add(count, piece);
                count++;
            }

            ChangeButtonLabel();

            //mix pieces by moving them
            for (int i = 0; i <= 1000; i++)
            {
                var randomNumber = rnd.Next(1, 9);
                CheckForOpenPiece(randomNumber);
            }

        }

        private void CheckIfSolved()
        {
            bool solved = true;
            foreach (var item in Pieces)
            {
                var current = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + item.Key));
                foreach (var cu in current)
                {
                    if (cu.Text == "")
                    {
                        if ("9" != item.Key.ToString())
                        {
                            solved = false;
                            continue;
                        }
                    }
                    else
                    {
                        if (cu.Text != item.Key.ToString())
                        {
                            solved = false;
                            started = false;
                            gameTimer.Stop();
                            continue;
                        }
                    }
                }
            }
            if (solved == true)
            {
                AddLabel("You Won!", 100, 100, "won");
                won = true;
                wins++;
                WinsLabel.Text = "Won: " + wins;
                DisableButtons("d");
            }
        }

        private void CheckForOpenPiece(int number)
        {
            int[] tryNumbers = { 1, 3 };
            foreach (var num in tryNumbers)
            {
                if (number + num > 0)
                {
                    var P = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + (number + num)));
                    var L = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + number));
                    foreach (var p in P)
                    {
                        foreach (var l in L)
                        {
                            if (p.Text == "" || p.Text == "button9")
                            {
                                if (number == 2)
                                {
                                    var First = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + (number + 1)));
                                    var Second = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + (number + 2)));
                                    foreach (var first in First)
                                    {
                                        foreach (var second in Second)
                                        {
                                            //second.Text = first.Text;
                                            //first.Text = l.Text;
                                            //l.Text = "";
                                        }
                                    }
                                }
                                if (number % 3 == 0 && num == 3)
                                {
                                    p.Text = l.Text;
                                    l.Text = "";
                                    return;
                                }
                                if (number % 3 != 0)
                                {
                                    p.Text = l.Text;
                                    l.Text = "";
                                    return;
                                }
                            }
                        }
                    }
                }
                if (number - num > 0)
                {
                    var P = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + (number - num)));
                    var L = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + number));
                    foreach (var p in P)
                    {
                        foreach (var l in L)
                        {
                            if (p.Text == "")
                            {
                                if (number == 4 || number == 7)
                                {
                                    if (num != 1)
                                    {
                                        p.Text = l.Text;
                                        l.Text = "";
                                        return;
                                    }
                                }
                                if (number != 4 && number != 7)
                                {
                                    p.Text = l.Text;
                                    l.Text = "";
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ChangeButtonLabel()
        {
            foreach (var piece in Pieces)
            {
                var p = Controls.OfType<Button>().Where(x => x.Name.StartsWith("button" + piece.Key));
                foreach (var item in p)
                {
                    if (piece.Value != 0)
                    {
                        item.Text = piece.Value.ToString();
                    }
                    else
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MixBoard();
            gameTimer.Interval = (100); // 45 mins
            gameTimer.Tick += new EventHandler(gameTimer_Tick);
            gameTimer.Start();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (started == true)
            {
                gameTimer.Dispose();
                
                TimerLabel.Text = gameTimer.Interval.ToString();
            }
        }

        private void TimerLabel_Click(object sender, EventArgs e)
        {

        }

        private void WinsLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
