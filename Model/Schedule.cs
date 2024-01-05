using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis_Tournament_Console.DAO;
using Tennis_Tournament_Console;


namespace Tennis_Tournament_Console
{
    internal class Schedule
    {
        public enum ScheduleType //Je sais pas si faut créer l'enum directement dans la classe
        {
            GentlemenSingle,
            LadiesSingle,
            GentlemenDouble,
            LadiesDouble,
            MixedDouble
        }
        private ScheduleType scheduleType;
        private int actualRound;
        private Queue<Match> matcheList;
        private Queue<Opponents> opponentsList;
        OpponentsDAO opponentsDAO = new OpponentsDAO();
        MatchDAO matchDAO = new MatchDAO();

        public Schedule(ScheduleType scheduleType)
        {
            this.scheduleType = scheduleType;
            this.actualRound = 0;
            this.matcheList = new Queue<Match>();
        }
        public int NbWinningSets()
        {
            switch (scheduleType)
            {
                case ScheduleType.GentlemenSingle:
                    return 3; // 3 sets gagnants pour les programmes simples
                case ScheduleType.LadiesSingle:
                    return 2; // 2 sets gagnants pour les programmes simples
                case ScheduleType.GentlemenDouble:
                    return 2; // 2 sets gagnants pour les programmes doubles
                case ScheduleType.LadiesDouble:
                    return 2; // 2 sets gagnants pour les programmes doubles
                case ScheduleType.MixedDouble:
                    return 2; // 2 sets gagnants pour les programmes doubles
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public async void PlayNextRound()
        {
            int matchesPlayed = 0;
            DateTime currentDate = Tournament.date;
            int matchesCount = opponentsList.Count / 2;
            List<Opponents> winners = new List<Opponents>();
            for (int i = 0; i < matchesCount; i++)
            {
                if (Tournament.courtsList.Count == 0 || Tournament.refereesList.Count == 0)
                {
                    matchesCount--;
                    Console.WriteLine("Plus d'arbitre ou de courts dispo");
                }
                else
                {
                    //Set up Opponents 
                    Opponents op1 = opponentsList.Dequeue();
                    Opponents op2 = opponentsList.Dequeue();
                    Match m = new Match();
                    m.setType((int)scheduleType);
                    m.setId_Tournament(Tournament.id);
                    m.setOpponents1(op1);
                    m.setOpponents2(op2);
                    //Set up Date
                    if (matchesPlayed % 30 == 0)
                    {
                        currentDate = currentDate.AddDays(1);
                        currentDate = currentDate.Date.AddHours(10);
                        Console.WriteLine("On rajoute un jours");

                    }
                    else
                    {
                        currentDate.AddHours(4);
                    }
                    m.setDate(currentDate);
                    //Set up Court-Referee
                    Court court = Tournament.courtsList.Dequeue();
                    Referee referee = Tournament.refereesList.Dequeue();
                    m.setReferee(referee);
                    m.setCourt(court);
                    m.setRound(actualRound);
                    int matchId = matchDAO.Create(m);
                    if (matchId != -1)
                    {
                        m.setId(matchId);
                    }
                    Console.WriteLine($"On va jouer le match: {m}");
                    await m.Play();
                    matcheList.Enqueue(m);
                    Tournament.courtsList.Enqueue(court);
                    Tournament.refereesList.Enqueue(referee);
                    winners.Add(m.GetWinner());
                    matchesPlayed++;
                    Console.WriteLine($"Le gagnant est "+m.GetWinner());
                }
            }
            opponentsList = new Queue<Opponents>(winners);
            Tournament.date = currentDate;
            this.actualRound++;
        }
        public Player GetWinner()
        {
            return this.GetWinner();
        }
        public ScheduleType GetType()
        {
            return scheduleType;
        }
        public int GetActualRound()
        {
            return actualRound;
        }

        public void Fill(List<Player> men, List<Player> women)
        {
            if (this.scheduleType == ScheduleType.GentlemenSingle || this.scheduleType == ScheduleType.LadiesSingle)
            {
                if (this.scheduleType == ScheduleType.GentlemenSingle)
                {
                    GenerateOpponentsSingle(men);
                }
                else
                {
                    GenerateOpponentsSingle(women);
                }
            }
            else
            {
                GenerateOpponentsDouble(this.scheduleType, men, women);
            }
        }
        private void GenerateOpponentsDouble(ScheduleType type, List<Player> men, List<Player> women)//GENERER LA LISTE DES OPPOSANTS EN CAS DE DOUBLE 64 opposants 32 matchs
        {
            int IsOpponentCreated;
            Queue<Opponents> opponentsList = new Queue<Opponents>();
            if (type == ScheduleType.GentlemenDouble)
            {
                for (int i = 0; i < 64; i++)
                {
                    Opponents oponnents = new Opponents(men[i * 2], men[(i * 2) + 1]);
                    IsOpponentCreated = opponentsDAO.Create(oponnents);
                    if (IsOpponentCreated != -1)
                    {
                        oponnents.Id = IsOpponentCreated;
                        opponentsList.Enqueue(oponnents);
                    }
                }
                this.opponentsList = opponentsList;
                Shuffle(opponentsList);
            }
            else if (type == ScheduleType.LadiesDouble)
            {
                for (int i = 0; i < 64; i++)
                {
                    Opponents opponents = new Opponents(women[i * 2], women[(i * 2) + 1]);
                    IsOpponentCreated = opponentsDAO.Create(opponents);
                    if (IsOpponentCreated != -1)
                    {
                        opponents.Id = IsOpponentCreated;
                        opponentsList.Enqueue(opponents);
                    }
                }
                this.opponentsList = opponentsList;
                Shuffle(opponentsList);


            }
            else if (type == ScheduleType.MixedDouble)
            {
                List<Player> MixedList = Schedule.MixList(men, women, 64);
                for (int i = 0; i < 64; i++)
                {
                    Opponents oponents = new Opponents(MixedList[i * 2], MixedList[(i * 2) + 1]);
                    IsOpponentCreated = opponentsDAO.Create(oponents);
                    if (IsOpponentCreated != -1)
                    {
                        oponents.Id = IsOpponentCreated;
                        opponentsList.Enqueue(oponents);
                    }
                }
                this.opponentsList = opponentsList;
                Shuffle(opponentsList);

            }

        }
        public void GenerateOpponentsSingle(List<Player> list)//GENERER LA LISTE DES OPPOSANTS EN CAS DE SIMPLE 128 opposants 64 matchs
        {
            int IsOpponentCreated;
            Queue<Opponents> opponentsList = new Queue<Opponents>();
            for (int i = 0; i < 128; i++)
            {
                Opponents opponents = new Opponents(list[i], null);
                IsOpponentCreated = opponentsDAO.Create(opponents);
                if (IsOpponentCreated != -1)
                {
                    opponents.Id = IsOpponentCreated;
                    opponentsList.Enqueue(opponents);
                }
            }
            this.opponentsList = opponentsList;
            Shuffle(opponentsList);

        }

        public Queue<Match> getMatchsList()
        {
            return matcheList;
        }

        static List<T> MixList<T>(List<T> liste1, List<T> liste2, int taille)
        {
            List<T> res = new List<T>();
            taille = Math.Min(Math.Min(liste1.Count, liste2.Count), taille);
            for (int i = 0; i < taille; i++)
            {
                res.Add(liste1[i]);
                res.Add(liste2[i]);
            }
            return res;
        }
        static void Shuffle<T>(Queue<T> queue)
        {
            Random rand = new Random();
            T[] array = queue.ToArray();

            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = array[k];
                array[k] = array[n];
                array[n] = value;
            }

            queue.Clear();
            foreach (T item in array)
            {
                queue.Enqueue(item);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Type de Programme: {scheduleType}");
            sb.AppendLine($"Tour Actuel: {actualRound}");
            sb.Append($"Nombre de Matchs: {matcheList.Count}");

            return sb.ToString();
        }
        public Queue<Opponents> GetOpponentsList()
        {
            return opponentsList;
        }
        public static int GetNbWinningSets(int type)
        {
            switch (type)
            {
                case 0:
                    return 3; // 3 sets gagnants pour les programmes simples
                case 1:
                    return 2; // 2 sets gagnants pour les programmes simples
                case 2:
                    return 2; // 2 sets gagnants pour les programmes simples
                case 3:
                    return 2; // 2 sets gagnants pour les programmes doubles
                case 4:
                    return 2; // 2 sets gagnants pour les programmes doubles
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
