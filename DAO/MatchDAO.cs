using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_Tournament_Console.DAO
{
    internal class MatchDAO : DAO<Match>
    {
        public MatchDAO()
        {
        }

        public override int Create(Match obj)
        {
            int res = -1;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Match(Date,Duration,Round,Type,Id_Opponent_1,Id_Opponent_2,Id_Tournament,Id_Court,Id_Ref) OUTPUT INSERTED.Id_Match VALUES(@Date,@Duration,@Round,@Type,@Id_Opponent_1,@Id_Opponent_2,@Id_Tournament,@Id_Court,@Id_Ref)", connection);
                    cmd.Parameters.AddWithValue("Date", obj.getDate());
                    cmd.Parameters.AddWithValue("Duration", obj.getDuration());
                    cmd.Parameters.AddWithValue("Round", obj.getRound());
                    cmd.Parameters.AddWithValue("Type", obj.getType());
                    cmd.Parameters.AddWithValue("Id_Opponent_1", obj.getOpponents1().Id);
                    cmd.Parameters.AddWithValue("Id_Opponent_2", obj.getOpponents2().Id);
                    cmd.Parameters.AddWithValue("Id_Tournament", obj.getId_Tournament());
                    cmd.Parameters.AddWithValue("Id_Court", obj.getCourt()?.getId() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Id_Ref", obj.getReferee()?.getId() ?? (object)DBNull.Value);
                    connection.Open();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }

            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        public override bool Delete(Match obj)
        {
            bool success = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.Match WHERE Id_Match = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", obj.getId());
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

        public override Match Find(int id)
        {
            Match match = new Match();
            try 
            {
               using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Match WHERE Id_Match = @Id", connection);
                    cmd.Parameters.AddWithValue("Id", id);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {

                        match.setId((int)reader["Id_Match"]);
                        match.setDate((DateTime)reader["Date"]);
                        match.setDuration((int)reader["Duration"]);
                        match.setRound((int)reader["Round"]);
                        match.setType((int)reader["Type"]);
                        match.setId_Tournament((int)reader["Id_Tournament"]);

                        OpponentsDAO opponentsDAO = new OpponentsDAO();
                        int idOpponent1 = (int)reader["Id_Opponent_1"];
                        int idOpponent2 = (int)reader["Id_Opponent_2"];
                        match.setOpponents1(idOpponent1 != 0 ? opponentsDAO.Find(idOpponent1) : null);
                        match.setOpponents2(idOpponent2 != 0 ? opponentsDAO.Find(idOpponent2) : null);

                        CourtDAO courtDAO = new CourtDAO();
                        int idCourt = (int)reader["Id_Court"];
                        match.setCourt(idCourt != 0 ? courtDAO.Find(idCourt) : null);

                        RefereeDAO refereeDAO = new RefereeDAO();
                        int idRef = (int)reader["Id_Ref"];
                        match.setReferee(idRef != 0 ? refereeDAO.Find(idRef) : null);
                    }
                }
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return match;
        }

        public override List<Match> FindAll()
        {
            List<Match> matchs = new List<Match>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Match", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Match match = new Match();
                        match.setId((int)reader["Id_Match"]);
                        match.setDate((DateTime)reader["Date"]);
                        match.setDuration((int)reader["Duration"]);
                        match.setRound((int)reader["Round"]);
                        match.setType((int)reader["Type"]);
                        match.setId_Tournament((int)reader["Id_Tournament"]);
                        OpponentsDAO opponentsDAO = new OpponentsDAO();
                        match.setOpponents1(opponentsDAO.Find((int)reader["Id_Opponent_1"]));
                        match.setOpponents2(opponentsDAO.Find((int)reader["Id_Opponent_2"]));
                        match.getCourt().setId((int)reader["Id_Court"]);//PAREIL POUR COURT
                        RefereeDAO refereeDAO = new RefereeDAO();
                        match.setReferee(refereeDAO.Find((int)reader["Id_Ref"]));
                        matchs.Add(match);
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return matchs;
        }

        public override bool Update(Match obj)
        {
            bool success = false;
            try 
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.Match SET Date = @Date, Duration = @Duration, Round = @Round, Type = @Type, Id_Opponent_1 = @Id_Opponent_1, Id_Opponent_2 = @Id_Opponent_2, Id_Tournament = @Id_Tournament, Id_Court = @Id_Court, Id_Ref = @Id_Ref WHERE Id_Match = @Id", connection);
                    cmd.Parameters.AddWithValue("Date", obj.getDate());
                    cmd.Parameters.AddWithValue("Duration", obj.getDuration());
                    cmd.Parameters.AddWithValue("Round", obj.getRound());
                    cmd.Parameters.AddWithValue("Type", obj.getType());
                    cmd.Parameters.AddWithValue("Id_Opponent_1", obj.getOpponents1().Id);
                    cmd.Parameters.AddWithValue("Id_Opponent_2", obj.getOpponents2().Id);
                    cmd.Parameters.AddWithValue("Id_Tournament", obj.getId_Tournament());
                    cmd.Parameters.AddWithValue("Id_Court", obj.getCourt().getId());
                    cmd.Parameters.AddWithValue("Id_Ref", obj.getReferee().getId());
                    cmd.Parameters.AddWithValue("Id", obj.getId());
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
