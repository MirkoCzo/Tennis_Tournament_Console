using Tennis_Tournament_Console;
using Tennis_Tournament_Console.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console
{
    internal class Referee : Person
    {

        private bool isAvailable;
        //private static Queue<Referee> referees = new Queue<Referee>();

        
        public bool Available()
        {
            return isAvailable;
        }
        public void Occupy()
        {
           isAvailable = false;
        }

        public void Release()
        {
            if (isAvailable)
            {
                Console.WriteLine("L'arbitre est déjà disponible.");
            }
            else isAvailable = true;
        }
        
         
    }
}
