using System;

namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        public class Player
        {
            /// <summary>
            /// Tennis score names.
            /// </summary>
            private static readonly string[] SCORE_NAME = { "Love", "Fifteen", "Thirty", "Deuce" };
            
            public readonly string name;
            
            public uint score = 0;

            public Player(string name)
            {
                this.name = name;
                this.score = 0;
            }

            /// <summary>
            /// We get the tennis score name for this player's current score,
            /// being the fourth one if higher.
            /// </summary>
            /// <returns>Love, Fifteen, Thirty or Deuce</returns>
            public string GetScoreName()
            {
                return SCORE_NAME[Math.Min(this.score, SCORE_NAME.Length-1)];
            }
        }
        
        private Player player1;
        private Player player2;

        public TennisGame1(string player1Name, string player2Name)
        {
            if(player1Name.Equals(player2Name))
            {
                player1Name += " (1)";
                player2Name += " (2)";
            }
            this.player1 = new Player(player1Name);
            this.player2 = new Player(player2Name);
        }

        public void WonPoint(string playerName)
        {
            if (playerName == this.player1.name)
            {
                this.player1.score += 1;
            }
            else if (playerName == this.player2.name)
            {
                this.player2.score += 1;
            }
            else
            {
                throw new ArgumentException("invalid player name");
            }
        }

        public string GetScore()
        {
            if(this.player1.score == this.player2.score)
            {
                if (this.player1.score<3)
                {
                    return this.player1.GetScoreName()+"-All";
                }
                else
                {
                    return this.player1.GetScoreName();
                }
            }
            if (this.player1.score >= 4 || this.player2.score >= 4)
            {
                int minusResult = this.player1.score - this.player2.score;
                if(minusResult == 1)
                {
                    return "Advantage "+this.player1.name;
                }
                if(minusResult == -1)
                {
                    return "Advantage "+this.player2.name;
                }
                if(minusResult >= 2)
                {
                    return "Win for "+this.player1.name;
                }
                return "Win for "+this.player2.name;
            }
            return this.player1.GetScoreName() +"-"+ this.player2.GetScoreName();
        }
    }
}

