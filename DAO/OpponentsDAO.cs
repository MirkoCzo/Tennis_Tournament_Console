using Tennis_Tournament_Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console.DAO
{
    internal class OpponentsDAO : DAO<Opponents>
    {
        public override int Create(Opponents obj)
        {
            int res = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Opponents(Id_Player1,Id_Player2) VALUES(@Id_Player1,@Id_Player2)", connection);
                    cmd.Parameters.AddWithValue("Id_Player1", obj.Player1);
                    cmd.Parameters.AddWithValue("Id_Player2", obj.Player2);
                    connection.Open();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }

                }catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public override bool Delete(Opponents obj)
        {
            bool success = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.Opponents WHERE Id_Opponent = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", obj.Id);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        public override Opponents Find(int id)
        {
            Opponents opponents = null;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Opponents WHERE Id_Opponent = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int idOpponents = reader.GetInt32(0);
                        int idPlayer1 = reader.GetInt32(1);
                        int? idPlayer2 = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2);
                        PlayerDAO playerDAO = new PlayerDAO();
                        Player player1 = playerDAO.Find(idPlayer1);
                        Player? player2 = idPlayer2 == null ? null : playerDAO.Find((int)idPlayer2);
                        opponents = new Opponents(idOpponents, player1, player2);
                    }
                }
            }catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return opponents;
        }

        public override List<Opponents> FindAll()
        {
            List<Opponents> listOpponents = new List<Opponents>();
            Opponents opponents;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Opponents", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int idOpponents = reader.GetInt32(0);
                        int idPlayer1 = reader.GetInt32(1);
                        int? idPlayer2 = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2);
                        PlayerDAO playerDAO = new PlayerDAO();
                        Player player1 = playerDAO.Find(idPlayer1);
                        Player? player2 = idPlayer2 == null ? null : playerDAO.Find((int)idPlayer2);
                        opponents = new Opponents(idOpponents, player1, player2);
                        listOpponents.Add(opponents);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listOpponents;
        }

        public override bool Update(Opponents obj)
        {
            bool success = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.Opponents SET Id_Player1 = @Id_Player1, Id_Player2 = @Id_Player2 WHERE Id_Opponent = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", obj.Id);
                    cmd.Parameters.AddWithValue("Id_Player1", obj.Player1);
                    cmd.Parameters.AddWithValue("Id_Player2", obj.Player2);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }
    }
}
