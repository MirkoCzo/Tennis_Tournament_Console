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