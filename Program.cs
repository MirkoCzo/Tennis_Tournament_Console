using Tennis_Tournament_Console;
/*
Set set = new Set();
set.Play();
Games game = new Games(0,0,0);
Console.WriteLine("Test TieBreak...");
game.PlayTieBreak();
Console.WriteLine("Test Jeu Normal...");
game.PlayGame();*/


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
