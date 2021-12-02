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
    public class VoetbaltruitjeDatabeheer : IVoetbaltruitjeRepository
    {
        private string connectionString;

        public VoetbaltruitjeDatabeheer(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool BestaatVoetbaltruitje(Voetbaltruitje truitje)
        {
            string query = "SELECT count(*) FROM dbo.Truitje WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", truitje.Id);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VoetbaltruitjeDatabeheerException("BestaatVoetbaltruitje niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool BestaatVoetbaltruitje(int voetbaltruitjeId)
        {
            string query = "SELECT count(*) FROM dbo.Truitje WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", voetbaltruitjeId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VoetbaltruitjeDatabeheerException("BestaatVoetbaltruitje niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Voetbaltruitje GeefVoetbaltruitje(int voetbaltruitjeId)
        {
            string query = "SELECT * FROM dbo.Truitje WHERE Id=@voetbaltruitjeId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@voetbaltruitjeId", voetbaltruitjeId);
                    IDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    Club c = new Club((string)dataReader["Competitie"], (string)dataReader["Ploeg"]);
                    ClubSet cs = new ClubSet(Convert.ToBoolean(dataReader["Thuis"]), (int)dataReader["Versie"]);
                    var maat = (Kledingmaat)Enum.Parse(typeof(Kledingmaat), (string)dataReader["Maat"]);
                    Voetbaltruitje v = new Voetbaltruitje((int)dataReader["Id"],c,(string)dataReader["Seizoen"],(double)dataReader["Prijs"],maat,cs);
                    dataReader.Close();
                    return v;
                }
                catch (Exception ex)
                {
                    throw new VoetbaltruitjeDatabeheerException("GeefVoetbaltrui niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjes()
        {
            string query = "SELECT * FROM dbo.Truitje";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    List<Voetbaltruitje> truitjes = new List<Voetbaltruitje>();
                    conn.Open();
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Club c = new Club((string)dataReader["Competitie"], (string)dataReader["Ploeg"]);
                        ClubSet cs = new ClubSet(Convert.ToBoolean(dataReader["Thuis"]), (int)dataReader["Versie"]);
                        var maat = (Kledingmaat)Enum.Parse(typeof(Kledingmaat), (string)dataReader["Maat"]);
                        Voetbaltruitje v = new Voetbaltruitje((int)dataReader["Id"], c, (string)dataReader["Seizoen"], (double)dataReader["Prijs"], maat, cs);
                        truitjes.Add(v);
                    }
                    dataReader.Close();
                    return truitjes;
                }
                catch (Exception ex)
                {
                    throw new VoetbaltruitjeDatabeheerException("GeefVoetbaltrui niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateVoetbaltruitje(Voetbaltruitje truitje)
        {
            string query = "UPDATE dbo.Truitje SET Maat=@Maat, Seizoen=@Seizoen, Prijs=@Prijs, Versie=@Versie, " +
                "Thuis=@Thuis, Ploeg=@Ploeg, Competitie=@Competitie WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Maat", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Seizoen", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Prijs", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@Versie", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Thuis", SqlDbType.Bit));
                    command.Parameters.Add(new SqlParameter("@Ploeg", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Competitie", SqlDbType.NVarChar));
                    command.Parameters["@Id"].Value = truitje.Id;
                    command.Parameters["@Maat"].Value = truitje.Kledingmaat.ToString();
                    command.Parameters["@Seizoen"].Value = truitje.Seizoen;
                    command.Parameters["@Prijs"].Value = truitje.Prijs;
                    command.Parameters["@Versie"].Value = truitje.ClubSet.Versie;
                    command.Parameters["@Thuis"].Value = truitje.ClubSet.Thuis; //attention
                    command.Parameters["@Ploeg"].Value = truitje.Club.Ploegnaam;
                    command.Parameters["@Competitie"].Value = truitje.Club.Competitie;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VoetbaltruitjeDatabeheerException("UpdateTruitje niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VerwijderVoetbaltruitje(Voetbaltruitje truitje)
        {
            string query = "DELETE FROM dbo.Truitje WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    command.Parameters["@Id"].Value = truitje.Id;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VoetbaltruitjeDatabeheerException("VerwijderTruitje niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VoegVoetbaltruitjeToe(Voetbaltruitje truitje)
        {
            string query = "INSERT INTO dbo.Klant (Maat, Seizoen, Prijs, Versie, " +
                "Thuis, Ploeg, Competitie) VALUES(@Maat, @Seizoen, @Prijs, @Versie, " +
                "@Thuis, @Ploeg, @Competitie)";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Maat", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Seizoen", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Prijs", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@Versie", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Thuis", SqlDbType.Bit));
                    command.Parameters.Add(new SqlParameter("@Ploeg", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Competitie", SqlDbType.NVarChar));
                    command.Parameters["@Maat"].Value = truitje.Kledingmaat.ToString();
                    command.Parameters["@Seizoen"].Value = truitje.Seizoen;
                    command.Parameters["@Prijs"].Value = truitje.Prijs;
                    command.Parameters["@Versie"].Value = truitje.ClubSet.Versie;
                    command.Parameters["@Thuis"].Value = truitje.ClubSet.Thuis; //attention
                    command.Parameters["@Ploeg"].Value = truitje.Club.Ploegnaam;
                    command.Parameters["@Competitie"].Value = truitje.Club.Competitie;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VoetbaltruitjeDatabeheerException("VoegTruitjeToe niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
    }
}
