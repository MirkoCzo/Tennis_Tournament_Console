using Tennis_Tournament_Console.DAO;
using Tennis_Tournament_Console.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console.Model
{
    internal class Set
    {
        private int id;
        private int scoreOp1;
        private int scoreOp2;
        private int setNumber;
        private bool isSuperTieBreak;
        private bool isFinished;
        public int id_match;
        private List<Games> games;
        private Opponents winner = new Opponents();
        private GamesDAO gamesDAO = new GamesDAO();
        private MatchDAO matchDAO = new MatchDAO();
        private Match match;
        bool isTieBreakPlayed = false;
        bool isSuperTieBreakPlayed = false;

        public Set(int id,int idMatch)
        {
            this.id = id;
            this.id_match= idMatch;
        }
        public Set(int idMatch)
        {
            this.id_match = idMatch;
        }
        public Set()
        {

        }

        

        public void Play()
        {
            int gameNumber = 1;
            Schedule.ScheduleType type = GetTypeMatch(getMatch(this.id_match)); 
            games = new List<Games>();
            while (!CheckIfSetIsFinished(scoreOp1, scoreOp2, match) && !isTieBreakPlayed && !isSuperTieBreakPlayed)
            {
                Games game = new Games(this.id, gameNumber);
                gameNumber++;
                
                if (ShouldPlayTieBreak(gameNumber))
                {
                    if (setNumber == 3 || setNumber == 5)
                    {
                        if (type == Schedule.ScheduleType.GentlemenSingle && setNumber == 5)
                        {
                            //Super Tie break Homme
                            SuperTieBreak superTieBreak = new SuperTieBreak(this.id, gameNumber);
                            superTieBreak.PlaySuperTieBreak();
                            UpdateSets(superTieBreak);
                            isSuperTieBreakPlayed = true;
                        }
                        else if (type != Schedule.ScheduleType.GentlemenSingle && setNumber == 3)
                        {
                            //Super Tie break Autre
                            SuperTieBreak superTieBreak = new SuperTieBreak(this.id, gameNumber);
                            superTieBreak.PlaySuperTieBreak();
                            UpdateSets(superTieBreak);
                            isSuperTieBreakPlayed = true;
                        }
                    }
                    else
                    {
                        //Tie break normal
                        game.PlayTieBreak();
                        UpdateSets(game);
                        isTieBreakPlayed = true;
                    }

                }
                else
                {
                    //Jeu normal
                    game.PlayGame();
                    UpdateSets(game);

                }
                //Sauve le jeu
              
                game.setGameNumber(gameNumber);
                game.setIdSet(this.id);
                int id = gamesDAO.Create(game);
                if (id != -1) 
                {
                    game.setId(id);
                    games.Add(game);

                }
                else
                {
                    throw new Exception("Erreur lors de la création du jeu");
                }

                
            }


        }
        private bool ShouldPlayTieBreak(int gameNumber)
        {
            return gameNumber >= 12 && scoreOp1 == 6 && scoreOp2 == 6;
        }
        private bool CheckIfSetIsFinished(int ScoreOp1, int ScoreOp2, Match match)
        {
            //Schedule.ScheduleType type;
            //type = GetTypeMatch(match);
            //int numberWinningSets = GetNbWinningSets(type);
            return (ScoreOp1 >= 6 && ScoreOp1 - ScoreOp2 >= 2) || (ScoreOp2 >= 6 && ScoreOp2 - ScoreOp1 >= 2);
        }
        public Opponents GetWinner()
        {
            if (scoreOp1 > scoreOp2)
            {
                return(matchDAO.Find(this.id_match)).getOpponents1();
            }
            else
            {
               return(matchDAO.Find(this.id_match)).getOpponents2();
            }
        }

        private bool CheckIfSuperTieBreak()
        {
            if ((setNumber == 3 || setNumber == 5) && CheckIfSetIsFinished(scoreOp1, scoreOp2, getMatch(id_match)))
            {
                return true;
            }

            return false;
        }


        private void UpdateSets(Games game)
        {

                if (game.getScoreOpOne().Last() > game.getScoreOpTwo().Last())
                {
                    this.scoreOp1++;
                }
                else
                {
                    this.scoreOp2++;
                } 
        }

       

                
        public Schedule.ScheduleType GetTypeMatch(Match match)
        {
           int type = match.getType();
            switch (type)
            {
                case 0:
                    return Schedule.ScheduleType.GentlemenSingle; 

                case 1:
                    return Schedule.ScheduleType.LadiesSingle; 

                case 2:
                    return Schedule.ScheduleType.GentlemenDouble; 

                case 3:
                    return Schedule.ScheduleType.LadiesDouble; 

                case 4:
                    return Schedule.ScheduleType.MixedDouble; 

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), "Type invalide."); 
            }
        }
        //Getter Setter
        public int getId()
        {
            return id;
        }
        public int getScoreOp1()
        {
            return scoreOp1;
        }
        public int getScoreOp2()
        {
            return scoreOp2;
        }
        public int getSetNumber()
        {
            return setNumber;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public void setScoreOp1(int scoreOp1)
        {
            this.scoreOp1 = scoreOp1;
        }
        public void setScoreOp2(int scoreOp2)
        {
            this.scoreOp2 = scoreOp2;
        }
        public void setSetNumber(int setNumber)
        {
            this.setNumber = setNumber;
        }
        public int getId_match()
        {
            return id_match;
        }
        public void setId_match(int id_match)
        {
            this.id_match = id_match;
        }

        public void setIsFinished(bool finished)
        {
            this.isFinished = finished;
        }

        public bool getIsFinished()
        {
            return isFinished;
        }

        public void setIsTieBreak(bool isSuperTieBreak)
        {
            this.isSuperTieBreak = isSuperTieBreak;
        }

        public bool getIsTieBreak()
        {
            return isSuperTieBreak;
        }

        public void setGames(List<Games> setGames)
        {
            this.games = setGames;
        }
        public Match getMatch(int id)
        {
            MatchDAO matchDAO = new MatchDAO();
            Match match = matchDAO.Find(id);
            return match;
        }

        public List<Games> getGames()
        {
            return this.games;
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"ID du set : {id}");
            stringBuilder.AppendLine($"Score Joueur 1 : {scoreOp1}");
            stringBuilder.AppendLine($"Score Joueur 2 : {scoreOp2}");
            stringBuilder.AppendLine($"Numéro du set : {setNumber}");
            stringBuilder.AppendLine($"Tie Break : {isTieBreakPlayed}");
            stringBuilder.AppendLine($"Super Tie Break : {isSuperTieBreak}");
            stringBuilder.AppendLine($"Terminé : {isFinished}");
            stringBuilder.AppendLine($"ID du match : {id_match}");
            stringBuilder.AppendLine($"Gagnant du set : {winner?.Player1.getFirstname()}");

            return stringBuilder.ToString();
        }

    }
}
