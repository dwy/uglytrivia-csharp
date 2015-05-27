using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        private static bool notAWinner;

        public static void Main(String[] args)
        {
            var game = new Game();
            game.add("Chet");
            game.add("Pat");
            game.add("Sue");

            var random = new Random();

            Run(game, random);
        }

        private static void Run(Game game, Random random)
        {
            do
            {
                game.roll(random.Next(5) + 1);

                if (random.Next(9) == 7)
                {
                    notAWinner = game.wrongAnswer();
                }
                else
                {
                    notAWinner = game.wasCorrectlyAnswered();
                }
            } while (notAWinner);
        }
    }

}

