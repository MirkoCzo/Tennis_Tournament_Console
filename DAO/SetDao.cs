using Tennis_Tournament_Console.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console.DAO
{
    internal class SetDAO : DAO<Set>
    {
        public override int Create(Set obj)
        {
            int res = -1;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO dbo.Sets (Score_Op_One,Score_Op_Two , Set_Number, Id_Match) OUTPUT INSERTED.Id_Set VALUES (@scoreOp1, @scoreOp2, @setNumber, @id_match)";
                        command.Parameters.AddWithValue("scoreOp1", obj.getScoreOp1());
                        command.Parameters.AddWithValue("scoreOp2", obj.getScoreOp2());
                        command.Parameters.AddWithValue("setNumber", obj.getSetNumber());
                        command.Parameters.AddWithValue("id_match", obj.getId_match());
                        res = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

            }catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }

        public override bool Delete(Set obj)
        {
            bool success = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM dbo.Sets WHERE Id_Set = @id";
                        command.Parameters.AddWithValue("id", obj.getId());
                        command.ExecuteNonQuery();
                        success = true;
                    }
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        public override Set Find(int id)
        {
            Set set = new Set();
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM dbo.Sets WHERE Id_Set = @id";
                        command.Parameters.AddWithValue("id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if(reader.Read())
                        {
                            set.setId(reader.GetInt32(0));
                            set.setScoreOp1(reader.GetInt32(1));
                            set.setScoreOp2(reader.GetInt32(2));
                            set.setSetNumber(reader.GetInt32(3));
                            set.setId_match(reader.GetInt32(4));
                        }
                    }
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return set;
        }

        public override List<Set> FindAll()
        {
            List<Set> sets = new List<Set>();
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM dbo.Sets";
                        SqlDataReader reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            Set set = new Set();
                            set.setId(reader.GetInt32(0));
                            set.setScoreOp1(reader.GetInt32(1));
                            set.setScoreOp2(reader.GetInt32(2));
                            set.setSetNumber(reader.GetInt32(3));
                            set.setId_match(reader.GetInt32(4));
                            sets.Add(set);
                        }
                    }
                }

            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return sets;
        }

        public override bool Update(Set obj)
        {
            bool success = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE dbo.Sets SET Score_Op_One = @scoreOp1, Score_Op_Two = @scoreOp2, Set_Number = @setNumber, Id_Match = @id_match WHERE Id_Set = @id";
                        command.Parameters.AddWithValue("scoreOp1", obj.getScoreOp1());
                        command.Parameters.AddWithValue("scoreOp2", obj.getScoreOp2());
                        command.Parameters.AddWithValue("setNumber", obj.getSetNumber());
                        command.Parameters.AddWithValue("id_match", obj.getId_match());
                        command.Parameters.AddWithValue("id", obj.getId());
                        command.ExecuteNonQuery();
                        success = true;
                    }
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }
    }
}
