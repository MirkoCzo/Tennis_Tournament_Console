using Tennis_Tournament_Console.DAO;
using Tennis_Tournament_Console.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


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
        private List<Match> matcheList;
        private Queue<Opponents> opponentsList;
        OpponentsDAO opponentsDAO = new OpponentsDAO();
        MatchDAO matchDAO = new MatchDAO();
        int matchPlayed = 0;
        DateTime currentDate = Tournament.date;

        public Schedule(ScheduleType scheduleType)
        {
            this.scheduleType = scheduleType;
            this.actualRound = 0;
            this.matcheList = new List<Match>();
        }
      
        
        //Jouer un tour du schedule
        public async void PlayNextRound()
        {
            int matchesCount = opponentsList.Count / 2;
            List<Match> matches = GenerateMatches(matchesCount);
            int currentMatch = 0;
            foreach(Match match in matches)
            {
                currentMatch++;
            }
            List<Opponents> winners = new List<Opponents>();
            Court court;
            Referee referee;
            foreach (Match match in matches)
            {
                
                while (!TryAssignCourtAndReferee(out court, out  referee))
                {
                    await Task.Delay(0001);
                }
                
                match.setCourt(court);
                match.setReferee(referee);
                matchDAO.Update(match);
                
                Opponents winner = await match.Play();
               
            
                winners.Add(winner);
               
             
                this.matchPlayed++;
               
                Tournament.courtsList.Enqueue(court);
                
                Tournament.refereesList.Enqueue(referee);
              

            }
           
            Tournament.date = this.currentDate;
           
            opponentsList = new Queue<Opponents>(winners);
          
            Tournament.round = this.actualRound;
        
            this.actualRound++;
           

        }


        //Générer les match (Horraire-Adversaire)
        public List<Match> GenerateMatches(int count)
        {
            List<Match> matches = new List<Match>();

            for (int i = 0; i < count; i++)
            {
                Opponents op1 = opponentsList.Dequeue();
                Opponents op2 = opponentsList.Dequeue();
                DateTime currentDate = SetMatchDate();

                Match m = CreateMatch(op1, op2, currentDate);
                

                if (m != null)
                {
                    matches.Add(m);
                }
                else
                {
                    throw new Exception("Erreur lors de la création du match");
                }
            }
            this.matcheList.AddRange(matches);
            return matches;
            
        }
        //Set les dates des matchs
        private DateTime SetMatchDate()
        {
            DateTime currentDate = this.currentDate;

            if (this.matchPlayed % 30 == 0)
            {
                currentDate = currentDate.AddDays(1);
                currentDate = currentDate.Date.AddHours(10);
            }
            else if (this.matchPlayed % 10 == 0)
            {
                currentDate = currentDate.AddHours(4);
            }
            else
            {
                currentDate = currentDate.AddHours(0); // Aucun changement d'heure pour les 10 premiers matchs
            }

            this.matchPlayed++;
            this.currentDate = currentDate;
            return currentDate;
        }
        //Save les matchs
        private Match CreateMatch(Opponents op1, Opponents op2, DateTime currentDate)
        {
            Match m = new Match(currentDate, 0, actualRound, (int)scheduleType, op1, op2, null, null, Tournament.id);
            int matchId = matchDAO.Create(m);

            if (matchId != -1)
            {
                m.setId(matchId);
                return m;
            }
            else
            {
                return null;
            }
        }
        //Trouver un arbitre et un court
        private bool TryAssignCourtAndReferee(out Court court, out Referee referee)
        {
            while (Tournament.courtsList.Count > 0 && Tournament.refereesList.Count > 0)
            {
                court = Tournament.courtsList.Dequeue();
                referee = Tournament.refereesList.Dequeue();
                return true;
            }

            court = null;
            referee = null;
            return false;
        }

        //Remplissage schedule
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
            if(type == ScheduleType.GentlemenDouble)
            {
                for (int i = 0; i < 64; i++)
                {
                    Opponents opponents = new Opponents(men[i *2], men[(i*2)+1]);
                   

                    IsOpponentCreated = opponentsDAO.Create(opponents);
                    if (IsOpponentCreated!=-1)
                    {
                        opponents.Id = IsOpponentCreated;
                        opponentsList.Enqueue(opponents);
                    }
                }
                this.opponentsList = opponentsList;
                Shuffle(opponentsList);
            }
            else if(type == ScheduleType.LadiesDouble)
            {
                for (int i = 0; i < 64; i++)
                {
                    Opponents opponents =  new Opponents(women[i * 2], women[(i * 2) + 1]);
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
            else if(type == ScheduleType.MixedDouble)
            {
                List<Player> MixedList= Schedule.MixList(men, women, 64);
                for (int i = 0; i < 64; i++)
                {
                    Opponents opponents = new Opponents(MixedList[i * 2], MixedList[(i * 2) + 1]);

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
        
        //Methodes utiles

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
        public static bool CheckIfScheduleIsFinished(int actualRound, int type)
        {
            if (actualRound == GetNbRound(type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int GetNbRound(int type)
        {
            switch (type)
            {
                case 0:
                    return 7; // 7 tours pour les programmes simples
                case 1:
                    return 7; // 7 tours pour les programmes simples
                case 2:
                    return 7; // 7 tours pour les programmes simples
                case 3:
                    return 6; // 6 tours pour les programmes doubles
                case 4:
                    return 6; // 6 tours pour les programmes doubles
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public int GetNbRound1(ScheduleType type)
        {
            switch (type)
            {
                case ScheduleType.GentlemenSingle:
                    return 7; // 7 tours pour les programmes simples
                case ScheduleType.LadiesSingle:
                    return 7; // 7 tours pour les programmes simples
                case ScheduleType.GentlemenDouble:
                    return 7; // 7 tours pour les programmes simples
                case ScheduleType.LadiesDouble:
                    return 6; // 6 tours pour les programmes doubles
                case ScheduleType.MixedDouble:
                    return 6; // 6 tours pour les programmes doubles
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        //Getter Setter
        public Queue<Opponents> GetOpponentsList()
        {
            return opponentsList;
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
          
    }
}

