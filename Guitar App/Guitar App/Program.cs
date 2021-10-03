using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Guitar_App
{
    class Program
    {

        //public static string[] e = { "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#", "E" };
        //public static string[] B = { "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        //public static string[] G = { "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G" };
        //public static string[] D = { "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D" };
        //public static string[] A = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A" };
        //public static string[] E = { "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#", "E" };

        public static List<string[]> Strings = new List<string[]>();
        public static List<Instrument> Instruments = new List<Instrument>();
        public static List<string> usedNotes = new List<string>();
        public static Dictionary<int, string> NoteHistory = new Dictionary<int, string>();
        public static int NoteHistoryCount = 0;
        public static string currentInstrument = "";

        static void Main(string[] args)
        {
            //Strings.Add(e);
            //Strings.Add(B);
            //Strings.Add(G);
            //Strings.Add(D);
            //Strings.Add(A);
            //Strings.Add(E);
            AddDefaltInstruments();
            CommandLine();
        }

        public static void AddDefaltInstruments()
        {

            currentInstrument = "Guitar";

            Dictionary<int, string> notes = new Dictionary<int, string>();
            notes.Add(1, "E");
            notes.Add(2, "B");
            notes.Add(3, "G");
            notes.Add(4, "D");
            notes.Add(5, "A");
            notes.Add(6, "E");
            Instruments.Add(new Instrument("Guitar", notes));
            Dictionary<int, string> notes1 = new Dictionary<int, string>();
            notes1.Add(1, "A");
            notes1.Add(2, "E");
            notes1.Add(3, "C");
            notes1.Add(4, "G");
            Instruments.Add(new Instrument("Ukulele", notes1));
            Dictionary<int, string> notes2 = new Dictionary<int, string>();
            notes2.Add(1, "E");
            notes2.Add(2, "B");
            notes2.Add(3, "G");
            notes2.Add(4, "D");
            notes2.Add(5, "A");
            notes2.Add(6, "D");
            Instruments.Add(new Instrument("DropDGuitar", notes2));
            Dictionary<int, string> notes3 = new Dictionary<int, string>();
            notes3.Add(1, "E");
            notes3.Add(2, "B");
            notes3.Add(3, "G");
            notes3.Add(4, "D");
            notes3.Add(5, "A");
            notes3.Add(6, "C");
            Instruments.Add(new Instrument("DropCGuitar", notes3));
        }

        public static void SearchGoogle(string query)
        {
            Process.Start("http://google.com/search?q=" + query);
        }

        static void MakeInstrument()
        {
            Dictionary<int, string> notes = new Dictionary<int, string>();
            Clear();
            Console.WriteLine("What is the name?");
            string name = Console.ReadLine();
            Clear();
            Console.WriteLine("How many strings?");
            string amountOfstrings = Console.ReadLine();
            if (IsNumber(amountOfstrings) != true)
            {
                Clear();
                Console.WriteLine("Not Valid.");
                return;
            }
            for (int i = 1; i <= Convert.ToInt32(amountOfstrings); i++)
            {
                Clear();
                Console.WriteLine($"String {i}: ");
                string stringInput = Console.ReadLine();
                stringInput = stringInput.ToUpper();
                notes.Add(i, stringInput);
            }

            Instruments.Add(new Instrument(name, notes));

        }

        static void ChangeInstruments()
        {
            bool found = false;
            Clear();
            Console.WriteLine("What Instrument would your like to change to?");
            foreach (var item in Instruments)
            {
                Console.WriteLine(item.Name);
            }
            string input = Console.ReadLine();
            foreach (var item in Instruments)
            {
                if (item.Name.ToLower() == input.ToLower())
                {
                    Clear();
                    currentInstrument = item.Name;
                    Console.WriteLine("Done");
                    Console.ReadKey();
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                Clear();
                Console.WriteLine($"Could not find {input}");
                Console.ReadKey();
            }
        }

        static void CommandLine() //Main Line
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("What Note Would You Like To Find?");
                if (NoteHistory.Count != 0)
                {
                    Console.WriteLine("History:");
                    foreach (var note in NoteHistory)
                    {
                        Console.WriteLine($"{note.Key}) {note.Value}");
                    }
                }
                Console.WriteLine("");
                string input = Console.ReadLine();

                if (input.Contains("clear"))
                {
                    NoteHistory.Clear();
                    NoteHistoryCount = 0;
                    continue;
                }

                if (input.Contains("add"))
                {
                    MakeInstrument();
                    NoteHistory.Clear();
                    NoteHistoryCount = 0;
                    continue;
                }

                if (input.ToLower() == "change")
                {
                    ChangeInstruments();
                    continue;
                }

                if (input.Contains("scale"))
                {
                    Scale(input);
                }

                if (IsNumber(input) == true)
                {
                    if (NoteHistory.ContainsKey(Convert.ToInt32(input)))
                    {
                        foreach (var chord in NoteHistory.Keys)
                        {
                            if (chord.ToString() == input)
                            {
                                FretBoard(NoteHistory[chord]);
                                break;
                            }
                        }
                    }
                }

                if (input == "help")
                {
                    Help();
                }
                else
                {
                    if (input.Length >= 1 && IsNumber(input) == false && input.Contains("M") == false & input.Contains("m") == false)
                    {
                        FretBoard(input);
                    }
                    if (input.Length > 1)
                    {
                        if (input.Contains("M") || input.Contains("m"))
                        {
                            Chord(input);
                        }
                    }
                }
            }
        }

        static Dictionary<int, string> ChangeKey(string input)
        {
            input = input.ToUpper();
            Dictionary<int, string> notes = new Dictionary<int, string>();
            string[] notesArray = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
            string[] notesArrayExtended = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" , "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
            int x = 0;
            //foreach (var item in A)
            //{
            //    itemCount++;
            //    if (itemCount <= 12)
            //    {
            //        notes.Add(item, itemCount);
            //    }
            //}
            bool firstNote = false;
            foreach (var note in notesArray)
            {
                if (firstNote == true)
                {
                    notes.Add(notes.Count + 1, note);
                }
                if (input == note)
                {
                    notes.Add(notes.Count + 1, note);
                    firstNote = true;
                }
                x++;
            }
            bool start = false;
            int itemCount = 0;
            foreach (var item in notesArrayExtended)
            {
                if (start == true)
                {
                    if (item != input)
                    {
                        notes.Add(notes.Count + 1, item);
                    }
                    else
                    {
                        break;
                    }
                }
                if (itemCount == 11)
                {
                    start = true;
                }
                itemCount++;
            }

            return notes;
        }

        static void Scale(string input)
        {
            string note = "";
            string m = ""; // minor or major m or M
            string word = "";
            bool hasSeventh = false;
            bool isDiminished = false;
            bool sharp = false;

            foreach (var letter in input)
            {
                if (letter == '#')
                {
                    sharp = true;
                    word += letter;
                    note = word;
                    word = "";
                }
                if (char.IsLetter(letter) && letter != 'm' && letter != 'M')
                {
                    word += letter;
                }
                if (letter == 'm')//minor
                {
                    if (sharp == false)
                    {
                        note = word;
                        word = "";
                    }
                    m = "m";
                }
                if (letter == 'M')//major
                {
                    if (sharp == false)
                    {
                        note = word;
                        word = "";
                    }
                    m = "M";
                }
            }
            Dictionary<int, string> noteDic = new Dictionary<int, string>();

            noteDic = ChangeKey(note);

            Clear();
            FindInterval(noteDic, m, hasSeventh, isDiminished);
        }

        static void Chord(string input)
        {
            string note = "";
            string m = ""; // minor or major m or M
            string word = "";
            bool sharp = false;
            bool hasSeventh = false;
            bool isDiminished = false;
            if (input.Contains("dim"))
            {
                isDiminished = true;
            }
            foreach (var letter in input)
            {
                if (letter == '#')
                {
                    sharp = true;
                    word += letter;
                    note = word;
                    word = "";
                }
                if (char.IsLetter(letter) && letter != 'm' && letter != 'M')
                {
                    word += letter;
                }
                if (letter == 'm')//minor
                {
                    if (sharp == false)
                    {
                        note = word;
                        word = "";
                    }
                    m = "m";
                }
                if (letter == 'M')//major
                {
                    if (sharp == false)
                    {
                        note = word;
                        word = "";
                    }
                    m = "M";
                }
                if (input.Contains("7"))
                {
                    hasSeventh = true;
                }
            }
            Dictionary<int, string> noteDic = new Dictionary<int, string>();

            noteDic = ChangeKey(note);

            Clear();

            FindInterval(noteDic, m, hasSeventh, isDiminished);

        }

        static void FindInterval(Dictionary<int, string> input, string minorOrMajor, bool hasSeventh, bool isDiminished)
        {
            string first = "";
            string third = "";
            string fith = "";
            string seventh = "";
            int count = 0;
            if (isDiminished == true)
            {
                foreach (var item in input.Values)
                {
                    if (count == 0)
                    {
                        first = item;
                    }
                    if (count == 2)
                    {
                        third = item;
                    }
                    if (count == 9)
                    {
                        fith = item;
                    }
                    if (count == 10)//flat cause its minor
                    {
                        if (hasSeventh == true)
                        {
                            seventh = item;
                        }
                    }
                    count++;
                }
            }
            if (minorOrMajor == "m") //minor
            {
                foreach (var item in input.Values)
                {
                    if (count == 0)
                    {
                        first = item;
                    }
                    if (count == 3)
                    {
                        third = item;
                    }
                    if (count == 7)
                    {
                        fith = item;
                    }
                    if (count == 10)//flat cause its minor
                    {
                        if (hasSeventh == true)
                        {
                            seventh = item;
                        }
                    }
                    count++;
                }
            }
            if (minorOrMajor == "M") //Major
            {
                foreach (var item in input.Values)
                {
                    if (count == 0)
                    {
                        first = item;
                    }
                    if (count == 4)
                    {
                        third = item;
                    }
                    if (count == 7)
                    {
                        fith = item;
                    }
                    if (count == 11)
                    {
                        if (hasSeventh == true)
                        {
                            seventh = item;
                        }
                    }
                    count++;
                }
            }
            string answer = "";
            if (hasSeventh == false)
            {
                answer = $"{first} {third} {fith}";
            }
            else
            {
                answer = $"{first} {third} {fith} {seventh}";
            }
            ChordMaker(answer);
        }

        static bool IsNumber(string input)
        {
            bool isNumber = false;

            foreach (var letter in input)
            {
                if (char.IsDigit(letter))
                {
                    isNumber = true;
                }
                else
                {
                    isNumber = false;
                    break;
                }
            }

            return isNumber;
        }

        static void ChordMaker(string input)
        {
            List<string> notes = new List<string>();
            List<string> notesOnFret = new List<string>();
            string note = "";
            input = input.ToUpper();
            input += " ";

            foreach (var letter in input)
            {
                if (letter == ' ')
                {
                    if (note.Length != 0)
                    {
                        bool sameNote = false;
                        if (notes.Count != 0)
                        {
                            foreach (var x in notes)
                            {
                                if (x == note)
                                {
                                    sameNote = true;
                                    break;
                                }
                                else
                                {
                                    sameNote = false;
                                }
                            }
                        }
                        if (sameNote == false)
                        {
                            notes.Add(note);
                            note = "";
                        }
                        else
                        {
                            note = "";
                        }
                    }
                }
                else
                {
                    note += letter;
                }
            }  //get notes
            
            Clear();

            Console.WriteLine("What position do you want. Type all for every position.");
            string position = Console.ReadLine();


            Clear();
            Console.WriteLine($"Current Instrument: {currentInstrument}");
            Console.WriteLine();
            Console.WriteLine("   1   2   3   4   5   6   7   8   9   10  11  12");

            int s = 0;
            int sc = 0;

            int instrumentIndex = 0;
            foreach (var item in Instruments)
            {
                if (currentInstrument == item.Name)
                {
                    break;
                }
                instrumentIndex++;
            }
            Dictionary<int, string[]> changedKeyNote = new Dictionary<int, string[]>();
            var f = Instruments[instrumentIndex];
            foreach (var item in f.Notes)
            {
                var changed = ChangeKey(item.Value);
                var beg = changed[1];
                changed.Add(13, beg);
                string[] finalItem;
                finalItem = changed.Values.ToArray();
                changedKeyNote.Add(item.Key, finalItem);
            }
            foreach (var x in changedKeyNote.Values)
            {
                int positionCount = 0;
                sc = 0;
                s = 0;
                s++;
                int c = 0;
                foreach (var n in x)
                {
                    if (notes.Contains(n.ToString()))
                    {
                        sc++;
                        positionCount++;
                    }
                    if (position.ToLower() == "all")
                    {
                        if (notes.Contains(n.ToString()))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            notesOnFret.Add(n.ToString());
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    else
                    {
                        if (notes.Contains(n.ToString()))
                        {
                            if (positionCount.ToString() == position)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                notesOnFret.Add(n.ToString());
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    if (c == 0)
                    {
                        Console.Write(n);
                        Console.Write("|");
                    }
                    else if (n.ToString().Length != 2)
                    {
                        Console.Write("-");
                        Console.Write(n);
                        Console.Write("-|");
                    }
                    else
                    {
                        Console.Write("-");
                        Console.Write(n);
                        Console.Write("|");
                    }
                    c++;
                }
                Console.WriteLine();
            }

            string notesForList = "";

            foreach (var d in notes)
            {
                notesForList += d;
                notesForList += " ";
            }
            bool noteHisoryAlreadyThere = false;
            foreach (var notelist in NoteHistory.Values)
            {
                if (notelist == notesForList)
                {
                    noteHisoryAlreadyThere = true;
                    break;
                }
                else
                {
                    noteHisoryAlreadyThere = false;
                }
            }

            if (noteHisoryAlreadyThere != true)
            {
                NoteHistoryCount++;
                NoteHistory.Add(NoteHistoryCount, notesForList);
            }
            notesOnFret.Reverse();
            foreach (var item in notesOnFret)
            {
                Play(item, "E", 1);
            }

            Console.ReadKey();

        }

        static void Help()
        {
            Clear();
            Console.WriteLine("What is your question?");
            string input = Console.ReadLine();
            SearchGoogle(input);
        }
        
        static void FretBoard(string input)
        {

            List<string> notes = new List<string>();
            string note = "";
            input = input.ToUpper();
            input += " ";

            foreach (var letter in input)
            {
                if (letter == ' ')
                {
                    if (note.Length != 0)
                    {
                        bool sameNote = false;
                        if (notes.Count != 0)
                        {
                            foreach (var x in notes)
                            {
                                if (x == note)
                                {
                                    sameNote = true;
                                    break;
                                }
                                else
                                {
                                    sameNote = false;
                                }
                            }
                        }
                        if (sameNote == false)
                        {
                            notes.Add(note);
                            note = "";
                        }
                        else
                        {
                            note = "";
                        }
                    }
                }
                else
                {
                    note += letter;
                }
            }  //get notes

            Clear();
            Console.WriteLine($"Current Instrument: {currentInstrument}");
            Console.WriteLine();
            Console.WriteLine("   1   2   3   4   5   6   7   8   9   10  11  12");

            int s = 0;
            int sc = 0;

            int instrumentIndex = 0;
            foreach (var item in Instruments)
            {
                if (currentInstrument == item.Name)
                {
                    break;
                }
                instrumentIndex++;
            }
            Dictionary<int, string[]> changedKeyNote = new Dictionary<int, string[]>();
            var f = Instruments[instrumentIndex];
            foreach (var item in f.Notes)
            {
                var changed = ChangeKey(item.Value);
                var beg = changed[1];
                changed.Add(13, beg);
                string[] finalItem;
                finalItem = changed.Values.ToArray();
                changedKeyNote.Add(item.Key, finalItem);
            }
            foreach (var x in changedKeyNote.Values)
            {
                int positionCount = 0;
                sc = 0;
                s = 0;
                s++;
                int c = 0;
                foreach (var n in x)
                {
                    if (notes.Contains(n.ToString()))
                    {
                        sc++;
                        positionCount++;
                    }
                    if (notes.Contains(n.ToString()))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    if (c == 0)
                    {
                        Console.Write(n);
                        Console.Write("|");
                    }
                    else if (n.ToString().Length != 2)
                    {
                        Console.Write("-");
                        Console.Write(n);
                        Console.Write("-|");
                    }
                    else
                    {
                        Console.Write("-");
                        Console.Write(n);
                        Console.Write("|");
                    }
                    c++;
                }
                Console.WriteLine();
            }

            string notesForList = "";

            foreach (var d in notes)
            {
                notesForList += d;
                notesForList += " ";
            }
            bool noteHisoryAlreadyThere = false;
            foreach (var notelist in NoteHistory.Values)
            {
                if (notelist == notesForList)
                {
                    noteHisoryAlreadyThere = true;
                    break;
                }
                else
                {
                    noteHisoryAlreadyThere = false;
                }
            }

            if (noteHisoryAlreadyThere != true)
            {
                NoteHistoryCount++;
                NoteHistory.Add(NoteHistoryCount, notesForList);
            }
            Console.ReadKey();
        }

        static public void Play(string toneInput, string durrationInput, decimal octiveInput)
        {
            
            int tone = 0;
            int durration = 0;
            int octive = 1;

            foreach (var item in usedNotes)
            {
                if (item.ToString() == toneInput)
                {
                    octive++;
                }
            }

            switch (toneInput.ToUpper())
            {
                case "A":
                    tone = 440;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "A#":
                    tone = 466;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "B":
                    tone = 494;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "C":
                    tone = 262;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "C#":
                    tone = 277;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "D":
                    tone = 294;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "D#":
                    tone = 311;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "E":
                    tone = 330;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "F":
                    tone = 349;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "F#":
                    tone = 370;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "G":
                    tone = 392;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "G#":
                    tone = 415;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                case "OC":
                    tone = 524;
                    usedNotes.Add(toneInput.ToUpper());
                    break;
                default:
                    Console.WriteLine("BadCode: Tones Method, swtich");
                    Console.ReadKey();
                    break;
            }
            switch (durrationInput.ToUpper())
            {
                case "W":
                    durration = 1600;
                    break;
                case "H":
                    durration = 800;
                    break;
                case "Q":
                    durration = 400;
                    break;
                case "E":
                    durration = 200;
                    break;
                case "S":
                    durration = 100;
                    break;
                //rest
                default:
                    Console.WriteLine("BadCode: Tones Method, swtich");
                    Console.ReadKey();
                    break;
            }

            //Play
            if (octive < 1)
            {
                octive = 1;
            }
            if (octive > 16)
            {
                octive = 16;
            }
            Console.Beep(tone /** octive*/, durration);
        }

        static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
    }
}
