﻿using System;
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

            Run(game, new Random());
        }

        private static void Run(Game game, Random random)
        {
            Game aGame = game;
            Random rand = random;

            do
            {
                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }
            } while (notAWinner);
        }
    }

}

