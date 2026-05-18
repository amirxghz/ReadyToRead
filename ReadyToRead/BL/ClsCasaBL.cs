using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadyToRead;

namespace ReadyToRead
{
    internal static class ClsCasaBL //Urbani
    {
        private static ClsCasa CreaCasaDaRiga(DataRow r)
        {
            ClsCasa c = new ClsCasa();
            c.ID = Convert.ToInt64(r["ID"]);
            c.RagioneSociale = r["ragione_sociale"] == DBNull.Value ? "" : r["ragione_sociale"].ToString();
            c.Esclusiva = Convert.ToBoolean(r["esclusiva"]);
            c.UtenteID = Convert.ToInt64(r["utenteID"]);
            c.IndirizzoSedeLegale = r["indirizzo_sede_legale"] == DBNull.Value ? "" : r["indirizzo_sede_legale"].ToString();
            c.IndirizzoSedeOperativa = r["indirizzo_sede_operativo "] == DBNull.Value ? "" : r["indirizzo_sede_operativo "].ToString();

            string tipoAzienda = r["tipo_azienda"] == DBNull.Value ? "" : r["tipo_azienda"].ToString();
            if (!string.IsNullOrEmpty(tipoAzienda))
                c.TipoAzienda = (ClsCasa.eTIPO_AZIENDA)Enum.Parse(typeof(ClsCasa.eTIPO_AZIENDA), tipoAzienda, true);

            string tipologia = r["tipologia"] == DBNull.Value ? "" : r["tipologia"].ToString();
            if (!string.IsNullOrEmpty(tipologia))
                c.Tipologia = (ClsCasa.eTIPO_CASA)Enum.Parse(typeof(ClsCasa.eTIPO_CASA), tipologia, true);

            return c;
        }

        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsCasa casa, out string errore)
        {
            long ID = 0;
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO houses (ragione_sociale, indirizzo_sede_legale, indirizzo_sede_operativo, tipo_azienda, esclusiva, tipologia, utenteID)
                               VALUES (@ragione_sociale, @indirizzo_sede_legale, @indirizzo_sede_operativo, @tipo_azienda, @esclusiva, @tipologia, @utenteID)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ragione_sociale", casa.RagioneSociale ?? "");
                cmd.Parameters.AddWithValue("@indirizzo_sede_legale", casa.IndirizzoSedeLegale ?? "");
                cmd.Parameters.AddWithValue("@indirizzo_sede_operativo", casa.IndirizzoSedeOperativa ?? "");
                cmd.Parameters.AddWithValue("@tipo_azienda", casa.TipoAzienda.ToString());
                cmd.Parameters.AddWithValue("@esclusiva", casa.Esclusiva);
                cmd.Parameters.AddWithValue("@tipologia", casa.Tipologia.ToString());
                cmd.Parameters.AddWithValue("@utenteID", 1);    //placeholder: da sostituire con ID utente sessione

                int numRec = cmd.ExecuteNonQuery();
                if (numRec == 1)
                    ID = cmd.LastInsertedId;

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return ID;
        }
        #endregion

        #region READ
        internal static List<ClsCasa> GetAll(ref MySqlConnection conn, out string errore)
        {
            List<ClsCasa> houses = new List<ClsCasa>();
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM houses", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int i = 0;
                while (i < dt.Rows.Count)
                {
                    houses.Add(CreaCasaDaRiga(dt.Rows[i]));
                    i++;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return houses;
        }

        internal static List<ClsCasa> GetByRagioneSociale(ref MySqlConnection conn, string ragioneSociale, out string errore)
        {
            List<ClsCasa> houses = new List<ClsCasa>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(ragioneSociale))
            {
                errore = "Ragione sociale non valida";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM houses WHERE ragione_sociale LIKE @ragione_sociale";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ragione_sociale", "%" + ragioneSociale + "%");

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        houses.Add(CreaCasaDaRiga(dt.Rows[i]));
                        i++;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return houses;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsCasa casa, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (ID <= 0)
            {
                errore = "ID non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string sql = @"UPDATE houses SET ragione_sociale=@ragione_sociale,
                                    indirizzo_sede_legale=@indirizzo_sede_legale,
                                    indirizzo_sede_operativo=@indirizzo_sede_operativo,
                                    tipo_azienda=@tipo_azienda,
                                    esclusiva=@esclusiva,
                                    tipologia=@tipologia
                                    WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@ragione_sociale", casa.RagioneSociale ?? "");
                    cmd.Parameters.AddWithValue("@indirizzo_sede_legale", casa.IndirizzoSedeLegale ?? "");
                    cmd.Parameters.AddWithValue("@indirizzo_sede_operativo", casa.IndirizzoSedeOperativa ?? "");
                    cmd.Parameters.AddWithValue("@tipo_azienda", casa.TipoAzienda.ToString());
                    cmd.Parameters.AddWithValue("@esclusiva", casa.Esclusiva);
                    cmd.Parameters.AddWithValue("@tipologia", casa.Tipologia.ToString());

                    esito = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return esito;
        }
        #endregion

        #region DELETE
        internal static long Delete(ref MySqlConnection conn, long ID, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (ID <= 0)
            {
                errore = "ID non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM houses WHERE ID=@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    esito = cmd.ExecuteNonQuery();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return esito;
        }
        #endregion

        #region COUNT
        internal static int Count(ref MySqlConnection conn, out string errore)
        {
            int count = 0;
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM houses", conn);
                object risultato = cmd.ExecuteScalar();
                if (risultato != null)
                    count = Convert.ToInt32(risultato);

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return count;
        }
        #endregion
    }
}