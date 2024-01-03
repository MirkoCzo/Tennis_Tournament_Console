using Tennis_Tournament_Console;

Games game1 = new Games(1, 0, 0);
Games game2 = new Games(1, 0, 0);
Console.WriteLine("Test TieBreak...");
game1.PlayTieBreak();
Console.WriteLine("Test Jeu Normal...");
game2.PlayGame();