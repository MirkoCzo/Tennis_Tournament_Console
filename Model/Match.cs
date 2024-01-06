using Tennis_Tournament_Console.DAO;
using Tennis_Tournament_Console.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console
{
    internal class Match
    {
        private int id;
        private DateTime date;
        private int duration;
        private int round;
        private int type;
        private Opponents opponents1;
        private Opponents opponents2;
        private Referee referee;
        private Court court;
        private List<Set> sets = new List<Set>();
        private int id_Tournament;
        private SetDAO setDAO = new SetDAO();
        public Match(DateTime date, int duration, int round, int type, Opponents opponents1, Opponents opponents2, Referee referee, Court court, int id_Tournament)
        {
            this.date = date;
            this.duration = duration;
            this.round = round;
            this.type = type;
            this.opponents1 = opponents1;
            this.opponents2 = opponents2;
            this.referee = referee;
            this.court = court;
            this.id_Tournament = id_Tournament;
        }
        public Match()
        {
            this.opponents1 = new Opponents();
            this.opponents2 = new Opponents();
            this.referee = new Referee();
            this.court = new Court();
        }

        public async Task<Opponents> Play()
        {
            int ScoreOp1 = 0;
            int ScoreOp2 = 0;
            int setNumber = 0;
            while (!CheckIfMatchIsFinished(ScoreOp1, ScoreOp2, type))
            {
                Set set = new Set(this.id);
                set.setSetNumber(setNumber);
                set.setScoreOp1(ScoreOp1);
                set.setScoreOp2(ScoreOp2);
                setNumber++;
                int id = setDAO.Create(set);
                if (id != -1)
                {
                    set.setId(id);
                    sets.Add(set);

                }
                else
                {
                    throw new Exception("Erreur lors de la création du set");
                }
                set.Play();
                setDAO.Update(set);
                if (set.GetWinner().Id == this.opponents1.Id)
                {
                    ScoreOp1++;
                }
                else
                {
                    ScoreOp2++;
                }
                
                
            }
            return WhoWin(ScoreOp1, ScoreOp2);
            
            
        }
        private Opponents WhoWin(int countOp1, int countOp2)
        {
            if (countOp1 > countOp2)
            {
                return this.opponents1;
            }
            else
            {
                return this.opponents2;
            }
        }
        private bool CheckIfMatchIsFinished(int ScoreOp1, int ScoreOp2, int type)
        {
            int numberWinningSets = Schedule.GetNbWinningSets(type);
            return (ScoreOp1 >= numberWinningSets) || (ScoreOp2 >= numberWinningSets);
        }
        //Getter Setter
        public int getId()
        {
            return id;
        }
        public DateTime getDate()
        {
            return date;
        }
        public int getId_Tournament()
        {
            return id_Tournament;
        }
        public void setId_Tournament(int id_Tournament)
        {
            this.id_Tournament = id_Tournament;
        }
        public int getDuration()
        {
            return duration;
        }
        public int getRound()
        {
            return round;
        }
        public Opponents getOpponents1()
        {
            return opponents1;
        }
        public Opponents getOpponents2()
        {
            return opponents2;
        }
        public Referee getReferee()
        {
            return referee;
        }
        public Court getCourt()
        {
            return court;
        }
        public List<Set> getSets()
        {
            return sets;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public void setDate(DateTime date)
        {
            this.date = date;
        }
        public void setDuration(int duration)
        {
            this.duration = duration;
        }
        public void setRound(int round)
        {
            this.round = round;
        }
        public void setOpponents1(Opponents opponents1)
        {
            this.opponents1 = opponents1;
        }
        public void setOpponents2(Opponents opponents2)
        {
            this.opponents2 = opponents2;
        }
        public void setReferee(Referee referee)
        {
            this.referee = referee;
        }
        public void setCourt(Court court)
        {
            this.court = court;
        }
        public void setSets(List<Set> sets)
        {
            this.sets = sets;
        }
        public int getType()
        {
            return type;
        }
        public void setType(int type)
        {
            this.type = type;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ID: {id}");
            sb.AppendLine($"Date: {date}");
            sb.AppendLine($"Duration: {duration}");
            sb.AppendLine($"Round: {round}");
            sb.AppendLine($"Type: {type}");
            sb.AppendLine($"Opponents 1: {opponents1.Player1.getFirstname()}");
            sb.AppendLine($"Opponents 2: {opponents1.Player2?.getFirstname() ?? "N/A"}");
            sb.AppendLine("VS"); 
            sb.AppendLine($"Opponents 2: {opponents2.Player1?.getFirstname()}");
            sb.AppendLine($"Opponents 2: {opponents2.Player2?.getFirstname() ?? "N/A"}");
            sb.AppendLine($"Referee: {referee}");
            sb.AppendLine($"Court: {court}");
            sb.AppendLine($"ID Tournament: {id_Tournament}");

         

            return sb.ToString();
        }


    }

}
