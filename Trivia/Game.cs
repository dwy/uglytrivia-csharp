using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        List<string> players = new List<string>();

        int[] places = new int[6];
        int[] purses = new int[6];

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        private TextWriter _out;

        public Game(TextWriter @out)
        {
            _out = @out;

            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(createRockQuestion(i));
            }
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Print(playerName + " was added");
            Print("They are player number " + players.Count);
            return true;
        }

        private void Print(string message)
        {
            _out.WriteLine(message);
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            Print(players[currentPlayer] + " is the current player");
            Print("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Print(players[currentPlayer] + " is getting out of the penalty box");
                    places[currentPlayer] = places[currentPlayer] + roll;
                    if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                    Print(players[currentPlayer]
                            + "'s new location is "
                            + places[currentPlayer]);
                    Print("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Print(players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                places[currentPlayer] = places[currentPlayer] + roll;
                if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                Print(players[currentPlayer]
                        + "'s new location is "
                        + places[currentPlayer]);
                Print("The category is " + currentCategory());
                askQuestion();
            }

        }

        private void askQuestion()
        {
            if (currentCategory() == QuestionCategory.CATEGORY_POP)
            {
                Print(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (currentCategory() == QuestionCategory.CATEGORY_SCIENCE)
            {
                Print(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (currentCategory() == QuestionCategory.CATEGORY_SPORTS)
            {
                Print(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (currentCategory() == QuestionCategory.CATEGORY_ROCK)
            {
                Print(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private String currentCategory()
        {
            if (places[currentPlayer] % 4 == 0) return QuestionCategory.CATEGORY_POP;
            if (places[currentPlayer] % 4 == 1) return QuestionCategory.CATEGORY_SCIENCE;
            if (places[currentPlayer] % 4 == 2) return QuestionCategory.CATEGORY_SPORTS;
            return QuestionCategory.CATEGORY_ROCK;
        }

        public bool wasCorrectlyAnswered()
        {
            bool canPlayerMove = !inPenaltyBox[currentPlayer] || isGettingOutOfPenaltyBox;
            if (canPlayerMove)
            {
                return IncreasePurseAndCheckDidPlayerWin();
            }
            NextPlayer();
            return true;
        }

        private bool IncreasePurseAndCheckDidPlayerWin()
        {
            Print("Answer was correct!!!!");
            purses[currentPlayer]++;
            Print(players[currentPlayer]
                  + " now has "
                  + purses[currentPlayer]
                  + " Gold Coins.");

            bool winner = didPlayerWin();
            NextPlayer();

            return winner;
        }

        public bool wrongAnswer()
        {
            Print("Question was incorrectly answered");
            Print(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            NextPlayer();
            return true;
        }


        private void NextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
        }

        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }

}
