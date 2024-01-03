using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tennis_Tournament_Console.DAO
{
    internal abstract class DAO<T>
    {
        protected string connectionString = null;
        public DAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ProjectDB"].ConnectionString;
        }
        public abstract int Create(T obj);
        public abstract bool Delete(T obj);
        public abstract bool Update(T obj);
        public abstract T Find(int id);
        public abstract List<T> FindAll();


    }
}
