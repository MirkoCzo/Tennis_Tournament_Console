﻿using Tennis_Tournament_Console;
using Tennis_Tournament_Console.DAO;
using Tennis_Tournament_Console.Model;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Tennis_Tournament_Console.Schedule;

namespace Tennis_Tournament_Console
{
    internal class Tournament
    {
        public static int id;
        private string name;
        public static Queue<Court> courtsList;
        public static Queue<Referee> refereesList;
        public static int round;
        public static DateTime date;
        private List<Schedule> scheduleList;
        private TournamentDAO tournamentDAO = new TournamentDAO();

        public Tournament(int id, string name, DateTime date)
        {
            Tournament.id = id;
            this.name = name;
            Tournament.date = date;
            FillList();
        }
        public Tournament(int id,string name)
        {
            Tournament.id=id;
            this.name = name;
        }
        public Tournament()
        {
          
        }
        public Tournament(string name, DateTime date)
        {
            this.name = name;
            Tournament.date = date;
            int id = tournamentDAO.Create(this);
            Tournament.id = id;
            FillList();
        }
        public int getId() { return id; }

        public string getName() { return name; }

        public void setId(int id) { Tournament.id = id; }

        public void setName(string name) { this.name = name; }

        public List<Schedule> GetSchedules() { return scheduleList; }

        public void Play()
        {
            GenerateSchedules();
            foreach (Schedule s in scheduleList)
            {
                if(s.GetType() == ScheduleType.GentlemenSingle || s.GetType() == ScheduleType.LadiesSingle)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        s.PlayNextRound();
                    }
                    
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        s.PlayNextRound();

                    }
                }
              
            }
        }
        public void GenerateSchedules()//1
        {
            this.scheduleList = new List<Schedule>();
            foreach (ScheduleType type in Enum.GetValues(typeof(ScheduleType)))
            {
                this.scheduleList.Add(new Schedule(type));
            }
            FillSchedule();
        }       
        public void FillSchedule()//2
        {
            List<Player> MenList = FetchPlayers("MALE");
            List<Player> WomenList = FetchPlayers("FEMALE");
            foreach (Schedule s in scheduleList)
            {
                s.Fill(MenList,WomenList);
            }
        }        
        
        
        public List<Player> FetchPlayers(string gender)
        {
            PlayerDAO playerDAO = new PlayerDAO();
            List<Player> playersList = new List<Player>();
            playersList = playerDAO.FindByGender(gender);
            return playersList;
        }
        public void FillList()
        {
            CourtDAO courtDAO = new CourtDAO();
            Queue<Court> tmp = new Queue<Court>(courtDAO.FindAll());
            Tournament.courtsList = tmp;
            RefereeDAO refereeDAO = new RefereeDAO();
            Queue<Referee> tmp2 = new Queue<Referee>(refereeDAO.FindAll());
            Tournament.refereesList = tmp2;
        }
        
       
    }
}
