using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis_Tournament_Console;
namespace Tennis_Tournament_Console.DAO
{
    class TournamentDAO : DAO<Tournament>
    {
        public override int Create(Tournament obj)
        {
            int res = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Tournament (Name) VALUES (@Name)", connection);
                    cmd.Parameters.AddWithValue("Name", obj.getName());
                    connection.Open();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }

        public override bool Delete(Tournament obj)
        {
            bool succes = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.Tournament WHERE Id_Tournament = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", obj.getId());
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    succes = res > 0;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return succes;
        }

        public override Tournament Find(int id)
        {
            Tournament tournament = new Tournament();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Tournament WHERE Id_Tournament = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tournament.setId(reader.GetInt32(0));
                        tournament.setName(reader.GetString(1));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tournament;
        }

        public override List<Tournament> FindAll()
        {
            List<Tournament> tournaments = new List<Tournament>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Tournament", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tournament tournament = new Tournament();
                        tournament.setId(reader.GetInt32(0));
                        tournament.setName(reader.GetString(1));
                        tournaments.Add(tournament);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return tournaments;
        }

        public override bool Update(Tournament obj)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.Tournament SET Name = @Name WHERE Id_Tournament = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", obj.getId());
                    cmd.Parameters.AddWithValue("Specs", obj.getName());
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
    }
}
