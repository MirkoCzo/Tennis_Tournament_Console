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
    internal class PlayerDAO : DAO<Player>
    {
        public override int Create(Player obj)
        {
            int res = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Player(FirstName, LastName, Rank, Gender, Nationality) VALUES(@FirstName, @LastName, @Rank, @Gender, @Nationality)", connection);
                    cmd.Parameters.AddWithValue("FirstName", obj.getFirstname());
                    cmd.Parameters.AddWithValue("LastName", obj.getLastname());
                    cmd.Parameters.AddWithValue("Rank", obj.getRank());
                    cmd.Parameters.AddWithValue("Gender", obj.getGender());
                    cmd.Parameters.AddWithValue("Nationality", obj.getNationality());
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


        public override bool Delete(Player obj)
        {
            bool succes = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.Player WHERE Id_Player = @Id", connection);
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

        public override Player Find(int id)
        { 
            Player player = null;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Player WHERE Id_Player = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        player.setId(reader.GetInt32(0));
                        player.setFirstname(reader.GetString(1));
                        player.setLastname(reader.GetString(2));
                        player.setRank(reader.GetInt32(3));
                        player.setGender(reader.GetString(4));
                        player.setNationality(reader.GetString(5));                       
                    }
                }
            }catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return player;
        }

        public override List<Player> FindAll()
        {
            List<Player> players = new List<Player>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Player", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.setId(reader.GetInt32(0));
                        player.setFirstname(reader.GetString(1));
                        player.setLastname(reader.GetString(2));
                        player.setRank(reader.GetInt32(3));
                        player.setGender(reader.GetString(4));
                        player.setNationality(reader.GetString(5));
                        players.Add(player);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return players;
        }


        public override bool Update(Player obj)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.Player SET FirstName = @FirstName, LastName = @LastName, Rank = @Rank, Gender = @Gender, Nationality = @Nationality WHERE Id_Player = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", obj.getId()); // Assuming there is an Id property in your Player class
                    cmd.Parameters.AddWithValue("FirstName", obj.getFirstname());
                    cmd.Parameters.AddWithValue("LastName", obj.getLastname());
                    cmd.Parameters.AddWithValue("Rank", obj.getRank());
                    cmd.Parameters.AddWithValue("Gender", obj.getGender());
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
        public List<Player> FindByGender(string gender)
        {
            List<Player> players = new List<Player>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Player Where Gender = @Gender", connection);
                    cmd.Parameters.AddWithValue("Gender", gender);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Player player = new Player();
                        player.setId(reader.GetInt32(0));
                        player.setFirstname(reader.GetString(1));
                        player.setLastname(reader.GetString(2));
                        player.setRank(reader.GetInt32(3));
                        player.setGender(reader.GetString(4));
                        player.setNationality(reader.GetString(5));
                        players.Add(player);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return players;
        }

    }
}


