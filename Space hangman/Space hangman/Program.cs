using System;

namespace Spaceman
{
  class Program
  {
    static void Main(string[] args)
    {
            
           
            Game.Greet();
            Console.Write("Please input 'M' for medium or 'H' for hard mode: ");
            string difficulty = Console.ReadLine();
            string _difficulty = difficulty.Trim().ToUpper();

            Console.Clear();

            Game game = new Game(_difficulty);

            do
            {
                game.Display();
                game.Ask();
                if (game.DidLose())
                {
                    game.Display();
                    Console.WriteLine("Looks like your the new test subject!");
                    break;
                }
                else if (game.DidWin())
                {
                    game.Display();

                    if (game.hardMode)
                    {
                        Console.WriteLine("Hooray! They let you and your friend go!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("They let you go but could you of saved your freind aswell?");
                        break;
                    }
                }
            } while (true);
        }
  }
}
