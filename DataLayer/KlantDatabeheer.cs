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
    public class KlantDatabeheer : IKlantRepository
    {
        private string connectionString;

        public KlantDatabeheer(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public bool BestaatKlant(Klant klant)
        {
            string query = "SELECT count(*) FROM dbo.Klant WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", klant.KlantId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("BestaatKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool BestaatKlant(int klantId)
        {
            string query = "SELECT count(*) FROM dbo.Klant WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", klantId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw  new KlantDatabeheerException("BestaatKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<Klant> GeefKlanten()
        {
            string query = "SELECT * FROM dbo.Klant";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    List<Klant> klant = new List<Klant>();
                    conn.Open();
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Klant k = new Klant((int)dataReader["Id"], (string)dataReader["Naam"], (string)dataReader["Adres"]);
                        klant.Add(k);
                    }
                    dataReader.Close();
                    return klant;
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("GeefKlanten niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Klant GeefKlant(int klantId)
        {
            string query = "SELECT * FROM dbo.Klant WHERE Id=@klantId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@klantId", klantId);
                    IDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    Klant k = new Klant((int)dataReader["Id"], (string)dataReader["Naam"], (string)dataReader["Adres"]);
                    dataReader.Close();
                    return k;
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("GeefKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateKlant(Klant klant)
        {
            string query = "UPDATE dbo.Klant SET Naam=@Naam, Adres=@Adres WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Adres", SqlDbType.NVarChar));
                    command.Parameters["@Id"].Value = klant.KlantId;
                    command.Parameters["@Naam"].Value = klant.Naam;
                    command.Parameters["@Adres"].Value = klant.Adres;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("UpdateKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VerwijderKlant(Klant klant)
        {
            string query = "DELETE FROM dbo.Klant WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    command.Parameters["@Id"].Value = klant.KlantId;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("VerwijderKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VoegKlantToe(Klant klant)
        {
            string query = "INSERT INTO dbo.Klant (Naam,Adres) VALUES(@Naam,@Adres)";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Adres", SqlDbType.NVarChar));
                    command.Parameters["@Naam"].Value = klant.Naam;
                    command.Parameters["@Adres"].Value = klant.Adres;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("VoegKlantToe niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
