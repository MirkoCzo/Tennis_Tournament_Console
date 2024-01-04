using Tennis_Tournament_Console.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tennis_Tournament_Console;


namespace Tennis_Tournament_Console.DAO
{
    internal class RefereeDAO : DAO<Referee>
    {
        public override int Create(Referee obj)
        {
            int res = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Referee(FirstName,LastName,Nationality) VALUES(@FirstName,@LastName,@Nationality)", connection);
                    cmd.Parameters.AddWithValue("FirstName", obj.getFirstname());
                    cmd.Parameters.AddWithValue("LastName", obj.getLastname());
                    cmd.Parameters.AddWithValue("Nationality",obj.getNationality());
                    connection.Open();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public override bool Delete(Referee obj)
        {
            bool success = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.Referee WHERE Id_Referee = @Id", connection);
                    cmd.Parameters.AddWithValue("Id_Referee", obj.getId);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
            }catch (SqlException ex ) 
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        public override Referee Find(int id)
        {
            Referee referee = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Referee WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        referee.setId(reader.GetInt32(0));
                        referee.setFirstname(reader.GetString(1));
                        referee.setLastname(reader.GetString(2));
                        referee.setNationality(reader.GetString(5));
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return referee;
        }
        public override bool Update(Referee obj)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.Referee SET FirstName = @FirstName, LastName = @LastName, Nationality = @Nationality WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", obj.getId()); 
                    cmd.Parameters.AddWithValue("FirstName", obj.getFirstname());
                    cmd.Parameters.AddWithValue("LastName", obj.getLastname());
                    cmd.Parameters.AddWithValue("Nationality", obj.getNationality());
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }
        public override List<Referee> FindAll()
        {
            List<Referee> referees = new List<Referee>();
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Referee", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        Referee referee = new Referee();
                        referee.setId(reader.GetInt32(0));
                        referee.setFirstname(reader.GetString(1));
                        referee.setLastname(reader.GetString(2));
                        referee.setNationality(reader.GetString(3));
                        referees.Add(referee);

                    }
                }
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return referees;
        }
    }
}
