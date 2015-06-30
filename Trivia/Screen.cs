namespace Trivia
{
    using System.IO;

    public class Screen
    {
        private readonly TextWriter outputWriter;

        public Screen(TextWriter outputWriter)
        {
            this.outputWriter = outputWriter;
        }

        public void PrintPlayerAdded(string playerName, int playerNumber)
        {
            this.outputWriter.WriteLine(playerName + " was added");
            this.outputWriter.WriteLine("They are player number " + playerNumber);
        }

        public void PrintPlayerGettingOutOfPenaltyBox(Player player, int roll)
        {
            if (player.CanGetOutOfPenaltyBox(roll))
            {
                this.PrintPlayerGettingOutOfPenaltyBox(player.Name);
            }
            else
            {
                this.PrintPlayerNotGettingOutOfPenaltyBox(player.Name);
            }
        }

        private void PrintPlayerNotGettingOutOfPenaltyBox(string playerName)
        {
            this.outputWriter.WriteLine(playerName + " is not getting out of the penalty box");
        }

        private void PrintPlayerGettingOutOfPenaltyBox(string playerName)
        {
            this.outputWriter.WriteLine(playerName + " is getting out of the penalty box");
        }

        public void PrintPlayerRoll(int roll, string playerName)
        {
            this.outputWriter.WriteLine(playerName + " is the current player");
            this.outputWriter.WriteLine("They have rolled a " + roll);
        }

        public void PrintCategory(string category)
        {
            this.outputWriter.WriteLine("The category is " + category);
        }

        public void PrintPlayerMoved(string playerName, int playerPlace)
        {
            this.outputWriter.WriteLine(playerName + "'s new location is " + playerPlace);
        }

        public void PrintQuestion(string question)
        {
            this.outputWriter.WriteLine(question);
        }

        public void PrintPlayerPurse(string playerName, int playerPurse)
        {
            this.outputWriter.WriteLine(playerName + " now has " + playerPurse + " Gold Coins.");
        }

        public void PrintCorrectAnswer()
        {
            this.outputWriter.WriteLine("Answer was correct!!!!");
        }

        public void PrintPlayerWrongAnswer(string playerName)
        {
            this.outputWriter.WriteLine("Question was incorrectly answered");
            this.outputWriter.WriteLine(playerName + " was sent to the penalty box");
        }
    }
}