using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console
{
    internal class Person
    {
        private int id;
        private string firstname;
        private string lastname;
        private string nationality;

        public Person() { }

        public Person(int id, string firstname, string lastname, string nationality)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.nationality = nationality;
        }

        public int getId()
        {
            return id;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public string getFirstname()
        {
            return firstname;
        }
        public string getLastname()
        {
            return lastname;
        }
        public string getNationality()
        {
            return nationality;
        }
        public void setFirstname(string firstname)
        {
            this.firstname = firstname;
        }
        public void setLastname(string lastname)
        {
            this.lastname = lastname;
        }
        public void setNationality(string nationality)
        {
            this.nationality = nationality;
        }
    }
}
