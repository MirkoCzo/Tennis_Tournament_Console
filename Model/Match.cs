﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tennis_Tournament_Console;
using static Tennis_Tournament_Console.Schedule;

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
        private List<Set> sets;
        private int id_Tournament;
        public Match() { }
        public Match(int id, DateTime date, int duration, int round, ScheduleType type, Opponents opponents1, Opponents opponents2, Referee referee, Court court, int id_Tournament)
        {
            this.id = id;
            this.date = date;
            this.duration = duration;
            this.round = round;
            this.type = (int)type; // Assurez-vous que 'type' est de type ScheduleType
            this.opponents1 = opponents1;
            this.opponents2 = opponents2;
            this.referee = referee;
            this.court = court;
            this.id_Tournament = id_Tournament;
            this.sets = new List<Set>(); // Initialise la liste des sets
        }

        public Opponents GetWinner()
        {
            return this.GetWinner();
        }

        public async Task<Opponents> Play()
        {
            return GetWinner();
        }
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
            sb.AppendLine($"ID du Match: {id}");
            sb.AppendLine($"Date: {date.ToString("yyyy-MM-dd HH:mm")}");
            sb.AppendLine($"Durée: {duration} minutes");
            sb.AppendLine($"Round: {round}");
            sb.AppendLine($"Type: {Enum.GetName(typeof(ScheduleType), type)}");
            sb.AppendLine($"Opponents 1: {opponents1}");
            sb.AppendLine($"Opponents 2: {opponents2}");
            sb.AppendLine($"Arbitre: {referee}");
            sb.AppendLine($"Court: {court}");

            // Ajouter les détails des sets, si disponibles
            if (sets != null && sets.Count > 0)
            {
                sb.AppendLine("Sets:");
                foreach (var set in sets)
                {
                    sb.AppendLine($"  - {set}"); // Assurez-vous que la classe Set a une méthode ToString bien définie
                }
            }
            else
            {
                sb.AppendLine("Aucun set n'a encore été joué.");
            }

            return sb.ToString();
        }


    }
}
