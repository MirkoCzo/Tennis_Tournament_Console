using Tennis_Tournament_Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console
{
    internal class Player : Person
    {
        private int rank;
        private string gender;

        public Player() { }

        public Player(int id, string firstname, string lastname, string nationality, int rank, string gender)
            : base(id, firstname, lastname, nationality) 
        {
            this.rank = rank;
            this.gender = gender;
        }


        public int getRank()
        {
            return rank;
        }
        public string getGender()
        {
            return gender;
        }
        public void setRank(int rank)
        {
            this.rank = rank;
        }
        public void setGender(string gender)
        {
            this.gender = gender;
        }
    }

}