﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console.Model
{
    internal class Games
    {
        private int id;
        private int scoreOp1;
        private int scoreOp2;
        protected List<int> score_Op_One;
        protected List<int> score_Op_Two;
        private int id_set;
        public int gameNumber;
        public int getId() { return id; }
        public List<int> getScoreOpOne() { return score_Op_One; }
        public List<int> getScoreOpTwo() { return score_Op_Two; }

        public int getScoreOp1() { return scoreOp1; }
        public int getScoreOp2() { return scoreOp2; }

        public int getIdSet() { return id_set; }
        public int getGameNumber() { return gameNumber; }

        public void setGameNumber(int gameNumber) { this.gameNumber = gameNumber; }
        public void setIdSet(int id_set) { this.id_set = id_set; }
        public void setId(int id) { this.id = id; }
        public void setScoreOpOne(List<int> score) { this.score_Op_One = score; }
        public void setScoreOpTwo(List<int> score) { this.score_Op_Two = score; }

        public Games(int id, int id_set, int gameNumber)
        {
            this.id = id;
            this.id_set = id_set;
            this.score_Op_One = new List<int>();
            this.score_Op_Two = new List<int>();
            this.gameNumber = gameNumber;
        }
        public Games(int id_set, int gameNumber)
        {
            this.id_set = id_set;
            this.score_Op_One = new List<int>();
            this.score_Op_Two = new List<int>();
            this.gameNumber = gameNumber;
        }

        public Games()
        {
            this.score_Op_One = new List<int>();
            this.score_Op_Two = new List<int>();
        }

        public void PlayGame()
        {
            Random random = new Random();

            int player1Points = 0;
            int player2Points = 0;

            while (!IsGameFinished(player1Points, player2Points))
            {

                bool player1WinsPoint = random.Next(2) == 0;

                if (player1WinsPoint)
                {
                    score_Op_One.Add(player1Points++);
                    score_Op_Two.Add(player2Points);
                }
                else
                {
                    score_Op_Two.Add(player2Points++);
                    score_Op_One.Add(player1Points);
                }

                if (player1Points == 3 && player2Points == 3)//Gestion des égalités
                {
                    HandleDeuce(ref player1Points, ref player2Points);
                }

            }
            this.scoreOp1 = player1Points;
            this.scoreOp2 = player2Points;


        }
        public void PlayTieBreak()
        {
            Random random = new Random();

            int player1Points = 0;
            int player2Points = 0;

            while (!IsTieBreakFinished(player1Points, player2Points))
            {
                bool player1WinsPoint = random.Next(2) == 0;

                if (player1WinsPoint)
                {
                    score_Op_One.Add(player1Points++);
                    score_Op_Two.Add(player2Points);
                }
                else
                {
                    score_Op_Two.Add(player2Points++);
                    score_Op_One.Add(player1Points);
                }

            }

        }

        private bool IsTieBreakFinished(int player1Points, int player2Points)
        {
            return (player1Points >= 7 || player2Points >= 7) && (player1Points - player2Points) >= 2;
        }


        private bool IsGameFinished(int player1Points, int player2Points)
        {
            return (player1Points >= 4 && player1Points - player2Points >= 2) || (player2Points >= 4 && player2Points - player1Points >= 2);
        }

        // Gestion des égalités (40-40)
        private void HandleDeuce(ref int player1Points, ref int player2Points)
        {
            while (!IsHandleDeuceFinished(player1Points, player2Points))
            {
                bool player1WinsPoint = new Random().Next(2) == 0;

                if (player1WinsPoint)
                {
                    //Console.WriteLine("Point gagné joueur 1");
                    player1Points++;
                    score_Op_One.Add(player1Points);
                    score_Op_Two.Add(player2Points);
                    //DisplayCurrentScore(player1Points, player2Points);
                }
                else
                {
                    //Console.WriteLine("Point gagné joueur 2");
                    player2Points++;
                    score_Op_One.Add(player2Points);
                    score_Op_Two.Add(player1Points);
                    //DisplayCurrentScore(player1Points, player2Points);

                }
                if (Math.Abs(player1Points - player2Points) == 0 && player1Points >= 4)
                {
                    //Console.WriteLine("Perte d'avantage retour à l'égalité");
                    player1Points = 3;
                    player2Points = 3;
                    score_Op_One.Add(player1Points);
                    score_Op_Two.Add(player2Points);
                    //DisplayCurrentScore(player1Points, player2Points);
                }
            }
            //Console.WriteLine("Sortie HandleDeuce");
        }
        private bool IsHandleDeuceFinished(int player1Points, int player2Points)
        {
            return (player1Points >= 5 && player1Points - player2Points >= 2) || (player2Points >= 5 && player2Points - player1Points >= 2);
        }

    }
}
