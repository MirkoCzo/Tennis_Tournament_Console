using Tennis_Tournament_Console.DAO;
using Tennis_Tournament_Console;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console
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

        public Set(int id,int idMatch)
        {
            this.id = id;
            this.id_match= idMatch;
        }
        public Set()
        {

        }

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

        public void Play()
        {
            int gameNumber = 0;
            Match match = getMatch(this.id_match);
            Schedule.ScheduleType type = GetTypeMatch(match);
            while (!CheckIfSetIsFinished(scoreOp1, scoreOp2, match))
            {
                Games game = new Games(this.id, gameNumber);
                gameNumber++;
                if(gameNumber>=12 && (scoreOp1 == 6 && scoreOp2 == 6))
                {
                    if(setNumber == 3 ||setNumber == 5)
                    {
                        if (type == Schedule.ScheduleType.GentlemenSingle && setNumber == 5)
                        {
                            SuperTieBreak superTieBreak = new SuperTieBreak(this.id, gameNumber);
                            superTieBreak.PlaySuperTieBreak();
                            UpdateSets(superTieBreak);
                        }
                        else if (type != Schedule.ScheduleType.GentlemenSingle && setNumber == 3)
                        {
                            SuperTieBreak superTieBreak = new SuperTieBreak(this.id, gameNumber);
                            superTieBreak.PlaySuperTieBreak();
                            UpdateSets(superTieBreak);
                        }
                    }
                    else
                    {
                        game.PlayTieBreak();
                        UpdateSets(game);
                    }
                   
                }
                else
                {
                    game.PlayGame();
                    UpdateSets(game);
                }
                games.Add(game);
            }

        }
        private bool CheckIfSetIsFinished(int ScoreOp1, int ScoreOp2, Match match)
        {
            Schedule.ScheduleType type;
            type = GetTypeMatch(match);
            int numberWinningSets = GetNbWinningSets(type);
            return (ScoreOp1 >= numberWinningSets && ScoreOp1 - ScoreOp2 >= 2) || (ScoreOp2 >= numberWinningSets && ScoreOp2 - ScoreOp1 >= 2);
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
                    scoreOp1++;
                }
                else
                {
                    scoreOp2++;
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
        private int GetNbWinningSets(Schedule.ScheduleType scheduleType)
        {
            Schedule schedule = new Schedule(scheduleType);
            return schedule.NbWinningSets();
        }
    }
  
}
