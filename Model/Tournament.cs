using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tennis_Tournament_Console.DAO;
using Tennis_Tournament_Console;
using static Tennis_Tournament_Console.Schedule;

namespace Tennis_Tournament_Console
{
    internal class Tournament
    {
        public static int id;
        private string name;
        public static Queue<Court> courtsList;
        public static Queue<Referee> refereesList;
        public static DateTime date;
        private List<Schedule> scheduleList;

        public Tournament(int id, string name, DateTime date)
        {
            Tournament.id = id;
            this.name = name;
            Tournament.date = date;
            Console.WriteLine("On rempli le tournoi");
            FillList();
        }
        public Tournament(int id, string name)
        {
            Tournament.id = id;
            this.name = name;
        }
        public Tournament()
        {

        }
        public int getId() { return id; }

        public string getName() { return name; }

        public void setId(int id) { Tournament.id = id; }

        public void setName(string name) { this.name = name; }

        public List<Schedule> GetSchedules() { return scheduleList; }

        public void Play()
        {
            Console.WriteLine("On Génère les Schedules");
            GenerateSchedules();
            foreach (Schedule s in scheduleList)
            {
                Console.WriteLine("On joue les schedules");
                // Déterminez le nombre de rounds à jouer pour chaque type de schedule
                int roundsToPlay = s.GetType() == ScheduleType.GentlemenSingle || s.GetType() == ScheduleType.LadiesSingle ? 7 : 6;

                for (int i = 0; i < roundsToPlay; i++)
                {
                    s.PlayNextRound();  // Jouer le round actuel
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
                s.Fill(MenList, WomenList);
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
            Console.WriteLine("On rempli les courts");
            Queue<Court> tmp = new Queue<Court>(courtDAO.FindAll());
            Tournament.courtsList = tmp;
            Console.WriteLine("On rempli les arbitres");
            RefereeDAO refereeDAO = new RefereeDAO();
            Queue<Referee> tmp2 = new Queue<Referee>(refereeDAO.FindAll());
            Tournament.refereesList = tmp2;
        }


    }
}
