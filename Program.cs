using Tennis_Tournament_Console;
/*
Set set = new Set();
set.Play();
Games game = new Games(0,0,0);
Console.WriteLine("Test TieBreak...");
game.PlayTieBreak();
Console.WriteLine("Test Jeu Normal...");
game.PlayGame();*/

/*/Tournament tournament = new Tournament(1,"Tournament Test",DateTime.Now);
foreach(Court c in Tournament.courtsList)
{
    Console.WriteLine(c);
}
foreach(Referee r in Tournament.refereesList)
{
    Console.WriteLine(r);
}*/
int i = 0;
// Créer et initialiser le tournoi
Tournament tournament = new Tournament(1, "Grand Slam", new DateTime(2024, 1, 1));
tournament.GenerateSchedules(); // Générer les schedules du tournoi
foreach (Court c in Tournament.courtsList)
{
    Console.WriteLine(c);
}
foreach (Referee r in Tournament.refereesList)
{
    Console.WriteLine(r);
}
foreach (Schedule s in tournament.GetSchedules())
{
    Console.WriteLine("Schedule " + i);
    Console.WriteLine(s);
    Console.WriteLine("=========================");
    int x = 1;
    foreach (Opponents op in s.GetOpponentsList())
    {
        
        if (op.Player2 != null)
        {
            Console.WriteLine($"{x}-{op.Player2.getFirstname()}-{op.Player1.getFirstname()} id : {op.Id}");

        }
        else {
            Console.WriteLine($"{x}-{op.Player1.getFirstname()} id : {op.Id}");

        }
        x++;
    }
    i++;
}

// Lancer le tournoi
Console.WriteLine("On lance le tournoi");
tournament.Play();

// Afficher les résultats
/*
Console.WriteLine("Résultats du tournoi:");
foreach (Schedule schedule in tournament.GetSchedules())
{
    Console.WriteLine("Schedule: ");
    Console.WriteLine(schedule); // Afficher les détails du schedule (s'assurer que Schedule a une méthode ToString())
    foreach (Match match in schedule.getMatchsList())
    {
        Console.WriteLine("Match: ");
        Console.WriteLine(match); // Afficher les détails de chaque match
    }
}

Console.WriteLine("Le tournoi est terminé.");

*/


/*
// Création des objets nécessaires (remplacer par des objets réels)
Opponents opponents1 = new Opponents(1,new Player(1,"Mirko","Cuozzo","BEL",2,"MALE"));
Opponents opponents2 = new Opponents(2,new Player(2,"Cedric","Ruitenbeek","BEL",3,"MALE"));
Referee referee = new Referee(3,"Jean","Dupont","GER");
Court court = new Court(1,2000,true);

// Création d'un match
Match match = new Match(1, DateTime.Now, 120, 1, Schedule.ScheduleType.LadiesSingle, opponents1, opponents2, referee, court, 100);

// Création et jeu d'un set
Set set = new Set(1, match); // Utilisez l'objet Match directement
set.Play();

// Affichage des résultats du set
Console.WriteLine($"Score final du set: Joueur 1 - {set.getScoreOp1()}, Joueur 2 - {set.getScoreOp2()}");
*/
