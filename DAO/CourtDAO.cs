using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console.DAO
{
    class CourtDAO : DAO<Court>
    {
        public override int Create(Court obj)
        {
            int id = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Court (NbSpec, Covered) VALUES (@NbSpec, @Covered)", connection);
                    cmd.Parameters.AddWithValue("NbSpec",obj.getNbSpectators());
                    cmd.Parameters.AddWithValue("Covered", obj.getCovered() ? 1 : 0);
                    connection.Open();
                    id = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return id;
        }

        public override bool Delete(Court obj)
        {
            bool succes = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.Court WHERE Id_Court = @Id", connection);
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

        public override Court Find(int id)
        {
            Court court = new Court();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Court WHERE Id_Court = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        court.setId(reader.GetInt32(0));
                        court.setNbSpectators(reader.GetInt32(1));
                        court.setCovered(reader.GetInt32(2) != 0);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return court;
        }

        public override List<Court> FindAll()
        {
            List<Court> courts = new List<Court>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Court", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Court court = new Court();
                        court.setId(reader.GetInt32(0));
                        court.setNbSpectators(reader.GetInt32(1));
                        bool covered = reader.GetInt32(2) != 0;
                        court.setCovered(covered);
                        courts.Add(court);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return courts;
        }

        public override bool Update(Court obj)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.Court SET NbSpec = @Specs, Covered = @Covered WHERE Id_Court = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", obj.getId()); 
                    cmd.Parameters.AddWithValue("Specs", obj.getNbSpectators());
                    cmd.Parameters.AddWithValue("Covered", obj.getCovered() ? 1 : 0);
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
