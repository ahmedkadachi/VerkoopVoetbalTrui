using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using DataLayer.Exceptions;

namespace DataLayer
{
    public class ClubDatabeheer : IClubRepository
    {
        private string connectionString;

        public ClubDatabeheer(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool BestaatClub(int clubId)
        {
            string query = "SELECT count(*) FROM dbo.Club WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", clubId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new ClubDatabeheerException("BestaatClub niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool BestaatClub(Club club)
        {
            string query = "SELECT count(*) FROM dbo.Club WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", club.ClubId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new ClubDatabeheerException("BestaatClub niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Club GeefClub(int clubId)
        {
            string query = "SELECT * FROM dbo.Club WHERE Id=@clubId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@clubId", clubId);
                    IDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    Club c = new Club((string)dataReader["Competitie"], (string)dataReader["Ploeg"]);
                    dataReader.Close();
                    return c;
                }
                catch (Exception ex)
                {
                    throw new ClubDatabeheerException("GeefClub niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<Club> GeefClubs()
        {
            string query = "SELECT * FROM dbo.Club";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    List<Club> clubs = new List<Club>();
                    conn.Open();
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Club c = new Club((string)dataReader["Competitie"], (string)dataReader["Ploeg"]);
                        clubs.Add(c);
                    }
                    dataReader.Close();
                    return clubs;
                }
                catch (Exception ex)
                {
                    throw new ClubDatabeheerException("GeefClubs niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateClub(Club club)
        {
            string query = "UPDATE dbo.Club SET Competitie=@Competitie, Ploeg=@Ploeg WHERE ClubId=@ClubId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@ClubId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Competitie", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Ploeg", SqlDbType.NVarChar));
                    command.Parameters["@ClubId"].Value = club.ClubId;
                    command.Parameters["@Competitie"].Value = club.Competitie;
                    command.Parameters["@Ploeg"].Value = club.Ploegnaam;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ClubDatabeheerException("UpdateClub niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VerwijderClub(Club club)
        {
            string query = "DELETE FROM dbo.Club WHERE ClubId=@ClubId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@ClubId", SqlDbType.Int));
                    command.Parameters["@ClubId"].Value = club.ClubId;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ClubDatabeheerException("VerwijderClub niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VoegClub(Club club)
        {
            string query = "INSERT INTO dbo.Club (Competitie,Ploeg) VALUES(@Competitie,@Ploeg)";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Competitie", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Ploeg", SqlDbType.NVarChar));
                    command.Parameters["@Competitie"].Value = club.Competitie;
                    command.Parameters["@Ploeg"].Value = club.Ploegnaam;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("VoegClub niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
