using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console
{
    internal class Court
    {
        private int id;
        private bool isAvailable;
        private int nbSpectators;
        private bool covered;
        //private static Queue<Court> courts = new Queue<Court>();

        public bool Available()
        {
            return isAvailable;
        }
        public void Release()
        {
            isAvailable = true;
        }
        public void Occupy()
        {
            isAvailable = false;
        }

        public int getId()
        {
            return id;
        }
        public void setId(int id)
        {
            this.id = id;
        }   

        public int getNbSpectators()
        {
            return nbSpectators;
        }

        
        public bool getCovered()
        {
            return covered;
        }
        
        public void setNbSpectators(int nbSpectators)
        {
            this.nbSpectators = nbSpectators;
        }

        public void setCovered(bool covered)
        {
            this.covered=covered;
        }
    }
}
