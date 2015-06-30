using System;
using System.IO;

namespace Trivia
{
    public class Game
    {
        private readonly PlayerPool playerPool = new PlayerPool();
        private readonly Screen screen;
        private readonly QuestionPool questionPool;

        private readonly Random random;

        public Game(TextWriter @out, Random random = null)
        {
            this.random = random;
            screen = new Screen(@out);
            questionPool = new QuestionPool();
        }

        public void AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            playerPool.AddPlayer(player);

            screen.PrintPlayerAdded(playerName, playerPool.HowManyPlayers());
        }

        public void Roll()
        {
            int roll = RollDice();
            var currentPlayer = playerPool.CurrentPlayer;
            screen.PrintPlayerRoll(roll, currentPlayer.Name);

            if (currentPlayer.InPenaltyBox)
            {
                screen.PrintPlayerGettingOutOfPenaltyBox(currentPlayer, roll);
                // TODO never getting out of the penalty box, player can still play
            }

            if (currentPlayer.CanAdvance())
            {
                AdvancePlayer(roll);
                AskQuestion();
            }
        }

        private void AdvancePlayer(int roll)
        {
            Player currentPlayer = playerPool.CurrentPlayer;
            currentPlayer.AddPlace(roll);

            screen.PrintPlayerMoved(currentPlayer.Name, currentPlayer.Place);
            screen.PrintCategory(CurrentCategory().ToString());
        }

        private int RollDice()
        {
            return random.Next(5) + 1;
        }

        private void AskQuestion()
        {
            string question = questionPool.GetQuestion(CurrentCategory());
            screen.PrintQuestion(question);
        }

        private QuestionCategory CurrentCategory()
        {
            int place = playerPool.CurrentPlayer.Place;
            
            var result = QuestionCategory.Rock;
            if (place % 4 == 0) result = QuestionCategory.Pop;
            if (place % 4 == 1) result = QuestionCategory.Science;
            if (place % 4 == 2) result = QuestionCategory.Sports;
            return result;
        }

        private void WasCorrectlyAnswered()
        {
            var currentPlayer = playerPool.CurrentPlayer;
            if (currentPlayer.CanAdvance())
            {
                AddPursesAndDidPlayerWin();
            }
        }

        private void AddPursesAndDidPlayerWin()
        {
            screen.PrintCorrectAnswer();
            var currentPlayer = playerPool.CurrentPlayer;
            currentPlayer.AddPurse();
            screen.PrintPlayerPurse(currentPlayer.Name, currentPlayer.Purse);
        }

        public void WrongAnswer()
        {
            screen.PrintPlayerWrongAnswer(playerPool.CurrentPlayer.Name);
            playerPool.CurrentPlayer.PutInPenaltyBox();
        }

        public void NextPlayer()
        {
            playerPool.NextPlayer();
        }

        public void Play()
        {
            if (AnswerWasWrong())
            {
                WrongAnswer();
                return;
            }
            WasCorrectlyAnswered();
        }

        private bool AnswerWasWrong()
        {
            return random.Next(9) == 7;
        }

        public bool DidLastPlayerWin()
        {
            return playerPool.LastPlayer.DidPlayerWin();
        }
    }
}
