using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console.Model
{
    internal class Opponents
    {
        int id;
        Player player1;
        Player? player2;

        public Opponents(int id, Player player1, Player? player2)
        {
            this.id = id;
            this.player1 = player1;
            this.player2 = player2;
        }
        public Opponents(int id, Player player1)
        {
            this.id = id;
            this.player1 = player1;
            this.player2 = null;
        }
        public Opponents(Player player1,Player? player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }
        public Opponents()
        {
          
        }
        public int Id { get { return id; } set { this.id = value; } }
        public Player Player1 { get { return player1; } set { this.player1 = value; } }
        public Player? Player2 { get { return player2; } set { this.player2 = value; } }
    }
}
