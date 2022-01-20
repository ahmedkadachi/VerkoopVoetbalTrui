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
    public class BestellingDatabeheer : IBestellingRepository
    {
        private string connectionString;

        public BestellingDatabeheer(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool BestaatBestelling(Bestelling bestelling)
        {
            string query = "SELECT count(*) FROM dbo.Bestelling WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", bestelling.BestellingId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("BestaatBestelling niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftBestellingTruitjes(Bestelling bestelling)
        {
            string query = "SELECT count(*) FROM dbo.Bestel_Trui WHERE IdBestelling=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", bestelling.BestellingId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("HeeftBestellingTruitjes niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool BestaatBestelling(int bestellingId)
        {
            string query = "SELECT count(*) FROM dbo.Bestelling WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", bestellingId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("BestaatBestelling niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
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
                    throw new BestellingDatabeheerException("BestaatKlant niet gelukt", ex);
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
                    throw new BestellingDatabeheerException("BestaatKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Bestelling GeefBestelling(int bestellingId)
        {
            string queryTrui = "SELECT * FROM dbo.Bestelling WHERE Id=@bestellingId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(queryTrui, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@bestellingId", bestellingId);
                    IDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    Dictionary<Voetbaltruitje, int> producten = GeefVoetbaltruiVanBestelling(bestellingId);

                    Bestelling b = new Bestelling((int)dataReader["Id"], GeefKlant((int)dataReader["KlantId"]),
                        (DateTime)dataReader["Datum"], (double)dataReader["Prijs"], Convert.ToBoolean(dataReader["Betaald"]), producten);

                    dataReader.Close();
                    return b;
                    //int bestellingId, Klant klant, DateTime? tijdstip, double prijs, bool betaald, Dictionary< Voetbaltruitje, int> producten
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("GeefBestelling niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<Bestelling> GeefBestellingen()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Bestelling> GeefBestellingenVanKlant(Klant klant)
        {
            string queryTrui = "SELECT * FROM dbo.Bestelling WHERE KlantId=@KlantId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(queryTrui, conn))
            {
                try
                {
                    List<Bestelling> bestellingen = new List<Bestelling>();
                    conn.Open();
                    command.Parameters.AddWithValue("@KlantId", klant.KlantId);
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Dictionary<Voetbaltruitje, int> producten = GeefVoetbaltruiVanBestelling((int)dataReader["Id"]);

                        bestellingen.Add(new Bestelling((int)dataReader["Id"], GeefKlant((int)dataReader["KlantId"]),
                            (DateTime)dataReader["Datum"], (double)dataReader["Prijs"], Convert.ToBoolean(dataReader["Betaald"]), producten));
                    }
                    dataReader.Close();
                    return bestellingen;
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("GeefBestellingenVanKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public IReadOnlyList<Bestelling> GeefBestellingenTussenDatums(DateTime? startDatum, DateTime? eindDatum)
        {
            string queryTrui = "SELECT * FROM dbo.Bestelling WHERE Datum BETWEEN @startDatum AND @eindDatum";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(queryTrui, conn))
            {
                try
                {
                    List<Bestelling> bestellingen = new List<Bestelling>();
                    conn.Open();
                    command.Parameters.AddWithValue("@startDatum", startDatum);
                    command.Parameters.AddWithValue("@eindDatum", eindDatum);
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Dictionary<Voetbaltruitje, int> producten = GeefVoetbaltruiVanBestelling((int)dataReader["Id"]);

                        bestellingen.Add(new Bestelling((int)dataReader["Id"], GeefKlant((int)dataReader["KlantId"]),
                            (DateTime)dataReader["Datum"], (double)dataReader["Prijs"], Convert.ToBoolean(dataReader["Betaald"]), producten));
                    }
                    dataReader.Close();
                    return bestellingen;
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("GeefBestellingenTussenDatums niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Bestelling GeefBestellingVanKlant(int klantId)
        {
            string queryTrui = "SELECT * FROM dbo.Bestelling WHERE KlantId=@KlantId";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(queryTrui, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@KlantId", klantId);
                    IDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    Dictionary<Voetbaltruitje, int> producten = GeefVoetbaltruiVanBestelling((int)dataReader["Id"]);

                    Bestelling b = new Bestelling((int)dataReader["Id"], GeefKlant((int)dataReader["KlantId"]),
                        (DateTime)dataReader["Datum"], (double)dataReader["Prijs"], Convert.ToBoolean(dataReader["Betaald"]), producten);

                    dataReader.Close();
                    return b;
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("GeefBestellingVanKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateBestelling(Bestelling bestelling)
        {
            string query = "UPDATE dbo.Bestelling SET Datum=@Datum, Prijs=@Prijs, Betaald=@Betaald," +
                "KlantId=@KlantId WHERE Id=@IdBestelling";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@IdBestelling", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@Prijs", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@Betaald", SqlDbType.Bit));
                    command.Parameters.Add(new SqlParameter("@KlantId", SqlDbType.Int));
                    command.Parameters["@IdBestelling"].Value = bestelling.BestellingId;
                    command.Parameters["@Datum"].Value = bestelling.Tijdstip;
                    command.Parameters["@Prijs"].Value = bestelling.Prijs;
                    command.Parameters["@Betaald"].Value = bestelling.Betaald;
                    command.Parameters["@KlantId"].Value = bestelling.Klant.KlantId;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("UpdateBestelling niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VerwijderBestelling(Bestelling bestelling)
        {
            string query = "DELETE FROM dbo.Bestelling WHERE Id=@Id";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    command.Parameters["@Id"].Value = bestelling.BestellingId;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("VerwijderBestelling niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VerwijderTruiVanBestelling(int bestellingId, int truiId)
        {
            string query = "DELETE FROM dbo.Bestel_Trui WHERE IdBestelling=@Id AND IdTruitje=@IdTruitje";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@IdTruitje", SqlDbType.Int));
                    command.Parameters["@Id"].Value = bestellingId;
                    command.Parameters["@IdTruitje"].Value = truiId;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new KlantDatabeheerException("VerwijderBestelling niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VoegBestellingToe(Bestelling bestelling)
        {
            int bestellingID;
            string querybestelling = "INSERT INTO dbo.Bestelling(Datum,Prijs,Betaald,KlantId) output INSERTED.Id " +
                "VALUES(@Datum,@Prijs,@Betaald,@KlantId) ";
            //string query = "INSERT INTO dbo.Truitje (Maat, Seizoen, Prijs, Versie, " +
            //    "Thuis, Ploeg, Competitie) VALUES(@Maat, @Seizoen, @Prijs, @Versie, " +
            //    "@Thuis, @Ploeg, @Competitie)";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(querybestelling, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@Prijs", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@Betaald", SqlDbType.Bit));
                    command.Parameters.Add(new SqlParameter("@KlantId", SqlDbType.Int));
                    command.Parameters["@Datum"].Value = bestelling.Tijdstip;
                    command.Parameters["@Prijs"].Value = bestelling.Prijs;
                    command.Parameters["@Betaald"].Value = bestelling.Betaald;
                    command.Parameters["@KlantId"].Value = bestelling.Klant.KlantId;
                    bestellingID = (int)command.ExecuteScalar();
                    //command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("VoegBestellingToe niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            string queryBestelTrui = "INSERT INTO dbo.Bestel_Trui(IdBestelling,IdTruitje,Aantal)" +
                    "VALUES(@IdBestelling,@IdTruitje,@Aantal)";

            foreach (KeyValuePair<Voetbaltruitje, int> element in bestelling.TruiBestelling)
            {
                using (SqlCommand command = new SqlCommand(queryBestelTrui, conn))
                {
                    try
                    {
                        Console.WriteLine(element.Value);
                        Console.WriteLine(element.Key.Id);
                        Console.WriteLine(bestellingID);
                        conn.Open();
                        command.Parameters.Add(new SqlParameter("@IdBestelling", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@IdTruitje", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@Aantal", SqlDbType.Int));
                        command.Parameters["@IdBestelling"].Value = bestellingID;
                        command.Parameters["@IdTruitje"].Value = element.Key.Id;
                        command.Parameters["@Aantal"].Value = element.Value;
                        
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new BestellingDatabeheerException("VoegBestellingToe niet gelukt", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        public Dictionary<Voetbaltruitje,int> GeefVoetbaltruiVanBestelling(int bestellingId)
        {
            string query = "SELECT * FROM dbo.Truitje t, dbo.Bestel_Trui b WHERE b.IdBestelling=@bestellingId AND t.Id=b.IdTruitje";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    Dictionary<Voetbaltruitje,int> truitjes = new Dictionary<Voetbaltruitje, int>();
                    conn.Open();
                    command.Parameters.AddWithValue("@bestellingId", bestellingId);
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Club c = new Club((string)dataReader["Competitie"], (string)dataReader["Ploeg"]);
                        ClubSet cs = new ClubSet(Convert.ToBoolean(dataReader["Thuis"]), (int)dataReader["Versie"]);
                        var maat = (Kledingmaat)Enum.Parse(typeof(Kledingmaat), (string)dataReader["Maat"]);
                        Voetbaltruitje v = new Voetbaltruitje((int)dataReader["Id"], c, (string)dataReader["Seizoen"], (double)dataReader["Prijs"], maat, cs);
                        int aantal = (int)dataReader["Aantal"];
                        truitjes.Add(v,aantal);
                    }
                    dataReader.Close();
                    return truitjes;
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("GeefVoetbaltruiVanBestelling niet gelukt", ex);
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
                    throw new BestellingDatabeheerException("GeefKlant niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateBestelTrui(int bestellingId, int truiId, int aantal)
        {
            string query = "UPDATE dbo.Bestel_Trui SET IdBestelling=@IdBestelling, IdTruitje=@IdTruitje, Aantal=@Aantal " +
                " WHERE IdBestelling=@IdBestelling AND IdTruitje=@IdTruitje";
            SqlConnection conn = getConnection();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@IdBestelling", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@IdTruitje", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Aantal", SqlDbType.Int));
                    command.Parameters["@IdBestelling"].Value = bestellingId;
                    command.Parameters["@IdTruitje"].Value = truiId;
                    command.Parameters["@Aantal"].Value = aantal;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BestellingDatabeheerException("UpdateBestelTrui niet gelukt", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
