using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
        public Court() { }
        public Court(int id, int nbSpec, bool covered)
        {
            this.id = id;
            this.nbSpectators = nbSpec;
            this.covered = covered;
        }

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
            this.covered = covered;
        }
        public override string ToString()
        {
            return $"Court ID: {this.id}, Nb Spectators: {this.nbSpectators}, Covered: {this.covered}";
        }
    }
}
