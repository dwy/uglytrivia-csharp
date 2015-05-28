﻿namespace Trivia
{
    public class Player
    {
        public string Name { get; private set; }
        public int Place { get; set; }
        public int Purse { get; set; }
        public bool InPenaltyBox { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}