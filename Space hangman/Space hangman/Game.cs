using System;
using System.Collections;
using System.Collections.Generic;

namespace Spaceman
{

    public class Game
    {
        private readonly int _maxGuesses = 5;

        private int _wrongGuesses;

        private string[] _hardCodeWords = new string[] { "lazzer", "sissors", "disc", "probing"};

        private string[] _mediumCodeWords = new string[] { "shazzam", "monster", "abducted", "overlords", "superior" };
        private string CodeWord { get; set; }

        private string _hiddenWord;

        public bool hardMode;

        private List<char> _wrongCharacters = new List<char>();

        readonly Ufo ufo = new Ufo();

        public Game(string modeSelect)
        {
            if (modeSelect.Length != 1)
            {
                Console.WriteLine("Please enter one letter at a time.");
                return;
            }
            else
            {
                if (modeSelect == "M")
                {
                    Random rand = new Random();
                    int index = rand.Next(_mediumCodeWords.Length);
                    CodeWord = _mediumCodeWords[index];


                    for (int i = 0; i < CodeWord.Length; i++)
                    {
                        _hiddenWord += "_ ";
                    }

                }
                else if (modeSelect == "H")
                {
                    hardMode = true;

                    Random rand = new Random();
                    int index = rand.Next(_hardCodeWords.Length);
                    CodeWord = _hardCodeWords[index];

                    for (int i = 0; i < CodeWord.Length; i++)
                    {
                        _hiddenWord += "_ ";
                    }
                }
                else
                    throw new InvalidCastException("Please select a mode");
            }

            _wrongGuesses = 0;
        }

        public static void Greet()
        {
            Console.WriteLine("Greetings Earthling");
            Console.WriteLine("  |^^^^^^^^^|");
            Console.WriteLine("  |    0    |");
            Console.WriteLine("  |  0   0  |");
            Console.WriteLine("__| ^^^^^^^ |__");
            Console.WriteLine("  | ^^^^^^^ |");
            Console.WriteLine("  |    ~    |");
            Console.WriteLine("  |_________|");
            Console.WriteLine("    _|   |_    \n");
            Console.WriteLine("In order to better understand your species we need a few dumb (cough).");
            Console.WriteLine("I mean courageous volenteers. If you can beat this game on hard.");
            Console.WriteLine("We'll have no use for you so you'll be free to go.");
            Console.WriteLine("If you complete on medium you can save yourself but not your a freind.");
        }

        public void Display()
        {
            Console.WriteLine(ufo.Stringify());
            Console.WriteLine("Word: " + _hiddenWord);
            Console.WriteLine();
            Console.Write("Wrong guesses: ");
            foreach (char guess in _wrongCharacters)
            {
                Console.Write(" " + guess);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Incorect guesses: " + _wrongGuesses);
            Console.WriteLine();
        }

        public void Ask()
        {
            Console.Write("Please guess a letter: ");
            string stringGuess = Console.ReadLine();
            string guess = stringGuess.Trim().ToLower();
            Console.WriteLine();

            if (guess.Length != 1)
            {
                Console.WriteLine("Please enter one letter at a time.");
                return;
            }
            char cGuess = Convert.ToChar(guess);
            if (CodeWord.Contains(cGuess))
            {
                 
                Console.WriteLine($"'{cGuess}' is in the word!");

                
                for (int i = 0; i < CodeWord.Length; i++)
                {
                    if (cGuess == CodeWord[i])
                    {
                        // replace letter at index*2 because hidden word appears like _ _ _ _ _ 
                        _hiddenWord = _hiddenWord.Remove(i*2, 1).Insert(i*2, cGuess.ToString());
                    }
                }
            }
            else 
            {
                    Console.WriteLine($"'{guess}' isn't in the word! The tractor beam keeps pulling you in ...");

                    _wrongCharacters.Add(cGuess);
                    ufo.AddPart();
                    _wrongGuesses ++;
            }   
        }

        public bool DidWin()
        {
            string hiddenWordNoSpaces = _hiddenWord.Replace(" ", "");
           return CodeWord.Equals(hiddenWordNoSpaces);
        }

        public bool DidLose()
        {
            return (_wrongGuesses >= _maxGuesses);
        }
    }
}