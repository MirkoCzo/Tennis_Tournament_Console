using Tennis_Tournament_Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console
{
    internal class SuperTieBreak : Games
    {
        
        private int gameNumber;
        public SuperTieBreak(int id_set, int gameNumber) : base(id_set, gameNumber)
        {
            this.gameNumber = gameNumber;
        }

        public void PlaySuperTieBreak()
        {
            int superTieBreakScoreOp1 = 0;
            int superTieBreakScoreOp2 = 0;

            while (!CheckIfSuperTieBreakFinished(superTieBreakScoreOp1, superTieBreakScoreOp2))
            {
                Random random = new Random();
                if (random.Next(2) == 0)
                {
                    base.score_Op_One.Add(superTieBreakScoreOp1++);
                    base.score_Op_Two.Add(superTieBreakScoreOp2);
                }
                else
                {
                    base.score_Op_Two.Add(superTieBreakScoreOp2++);
                    base.score_Op_One.Add(superTieBreakScoreOp1);
                }
            }

        }

        private bool CheckIfSuperTieBreakFinished(int scoreOp1, int scoreOp2)
        {
            return (scoreOp1 >= 10 || scoreOp2 >= 10) && (scoreOp1 - scoreOp2 >= 2 || scoreOp2 - scoreOp1 >= 2);
        }

    }
}
