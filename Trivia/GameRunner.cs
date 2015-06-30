using System;

namespace Trivia
{
    public static class GameRunner
    {
        public static void Main()
        {
            var random = new Random();
            var aGame = new Game(Console.Out, random);
            aGame.AddPlayer("Chet");
            aGame.AddPlayer("Pat");
            aGame.AddPlayer("Sue");

            Run(aGame);
            Console.ReadLine();
        }

        public static void Run(Game game)
        {
            do
            {
                game.Roll();
                game.Play();
                game.NextPlayer();
            } while (!game.DidLastPlayerWin());
        }
    }

}

