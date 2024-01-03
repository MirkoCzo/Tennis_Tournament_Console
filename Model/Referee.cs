using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tennis_Tournament_Console.DAO;
using Tennis_Tournament_Console;

namespace Tennis_Tournament_Console
{
    internal class Referee : Person
    {
        public Referee() { }
        public Referee(int id, string firstname, string lastname, string nationality)
            : base(id, firstname, lastname, nationality)
        {
            
        }

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
        public void FillList()
        {
            RefereeDAO refereeDAO = new RefereeDAO();
            Queue<Referee> tmp = new Queue<Referee>(refereeDAO.FindAll());
            //Referee.referees = tmp;
        }

    }
}
