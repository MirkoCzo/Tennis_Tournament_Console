using Tennis_Tournament_Console;
using Tennis_Tournament_Console.Model;
using System.Diagnostics;
int currentTourNumber = 0;
int currentMatchNumber = 0;

Tournament t = new Tournament("TestTournoi", new DateTime(2024, 1, 1));
t.GenerateSchedules();
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
List<Schedule> scheduleList = t.GetSchedules();
foreach (Schedule s in scheduleList)
{
    currentTourNumber++;
    
    int NumberTourToPlay = s.GetNbRound1(s.GetType());
    if (currentTourNumber <= NumberTourToPlay)
    {
        Console.WriteLine("Tour n°"+currentTourNumber);
        Console.WriteLine("-------------------------------------------------");
        s.PlayNextRound();
    }
    

}
/*List<Schedule> scheduleList = t.GetSchedules();
Queue<Opponents> opponents = scheduleList[1].GetOpponentsList();

for (int i = 0; i < 7; i++)
{
    currentTourNumber++;
    Console.WriteLine("Tour n"+currentTourNumber);
    Console.WriteLine("-------------------------------");
    scheduleList[0].PlayNextRound();

}
Queue<Opponents> w = scheduleList[0].GetOpponentsList();
foreach (Opponents o in w)
{
    Console.WriteLine("Le gagnant est : ");
    Console.WriteLine(o.Player1.getFirstname());
}*/
