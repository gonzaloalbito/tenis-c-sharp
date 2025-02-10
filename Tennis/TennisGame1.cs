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
            public static readonly string[] SCORE_NAME = { "Love", "Fifteen", "Thirty", "Forty" };
            public static readonly string[] SCORE_DIFF_NAME = { "Deuce", "Advantage {0}", "Win for {0}" };
            
            public readonly string name;
            
            public uint score = 0;

            public Player(string name)
            {
                this.name = name;
                this.score = 0;
            }

            /// <summary>
            /// We get the tennis score name for this player's current score.
            /// </summary>
            /// <returns>Love, Fifteen or Thirty</returns>
            public string GetScoreName()
            {
                return SCORE_NAME[Math.Min(this.score, SCORE_NAME.Length-1)];
            }

            /// <summary>
            /// We get the tennis score name for this player's current score,
            /// being the fourth one if higher.
            /// </summary>
            /// <returns>Love, Fifteen, Thirty or Deuce</returns>
            public string GetScoreDiffName(Player otherPlayer)
            {
                return SCORE_NAME[Math.Min(this.score, SCORE_NAME.Length-1)];
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="otherPlayer"></param>
            /// <returns></returns>
            public sbyte CompareScores(Player otherPlayer)
            {
                long scoreDiff = (long)this.score-otherPlayer.score;
                return (sbyte)Math.Min(
                                Math.Max(scoreDiff, -(SCORE_DIFF_NAME.Length-1)),
                                SCORE_DIFF_NAME.Length-1);
            }
        }
        
        private Player player1;
        private Player player2;

        public TennisGame1(string player1Name, string player2Name)
        {
            if(player1Name.Equals(player2Name))
            {
                throw new ArgumentException("Player names cannot be the same.");
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
            //TODO GAM Increase readability.
            sbyte scoreDiff = this.player1.CompareScores(this.player2);
            if(Math.Max(this.player1.score, this.player2.score)<4)
            {
                if(scoreDiff==0)
                {
                    if(this.player1.score<3)
                    {
                        return this.player1.GetScoreName()+"-All";
                    }
                    return Player.SCORE_DIFF_NAME[0];
                }
                return this.player1.GetScoreName() +"-"+ this.player2.GetScoreName();
            }
            Player winningPlayer = scoreDiff > 0 ? this.player1 : this.player2;
            return string.Format(Player.SCORE_DIFF_NAME[Math.Abs(scoreDiff)], winningPlayer.name);
        }
    }
}

