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
    internal static class ClsOrdineBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsOrdine ordine, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                conn.Open();

                string sql = @"INSERT INTO ordini (ordineID, statoOrdine, totale, destinazione) 
                             VALUES (@ordineID, @statoOrdine, @totale, @destinazione)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ordineID", ordine.OrdineID ?? "");
                cmd.Parameters.AddWithValue("@statoOrdine", "NON_SPEDITO");
                cmd.Parameters.AddWithValue("@totale", ordine.Totale);
                cmd.Parameters.AddWithValue("@destinazione", ordine.Destinazione ?? "");

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
      
        internal static ClsOrdine GetByOrdineID(ref MySqlConnection conn, string ordineID, out string errore)
        {
            DataTable dt = null;
            ClsOrdine ordine = null;
            errore = string.Empty;

            if (string.IsNullOrEmpty(ordineID))
                errore = "Ordine ID non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM ordini WHERE ordineID=@ordineID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ordineID", ordineID);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        ordine = new ClsOrdine();
                        ordine.OrdineID = dt.Rows[0]["ordineID"].ToString();
                        ordine.Totale = (decimal)dt.Rows[0]["totale"];
                        ordine.Destinazione = dt.Rows[0]["destinazione"].ToString();
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return ordine;
        }
        internal static List<ClsOrdine> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsOrdine> ordini = new List<ClsOrdine>();
            errore = string.Empty;

            try
            {
                conn.Open();

                string query = "SELECT * FROM ordini";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsOrdine ordine = new ClsOrdine();
                    ordine.OrdineID = dt.Rows[i]["ordineID"].ToString();
                    ordine.Totale = (decimal)dt.Rows[i]["totale"];
                    ordine.Destinazione = dt.Rows[i]["destinazione"].ToString();
                    ordini.Add(ordine);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return ordini;
        }
        
        internal static List<ClsOrdine> GetByStato(ref MySqlConnection conn, string stato, out string errore)
        {
            DataTable dt = null;
            List<ClsOrdine> ordini = new List<ClsOrdine>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(stato))
                errore = "Stato non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM ordini WHERE statoOrdine=@stato";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@stato", stato);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsOrdine ordine = new ClsOrdine();
                        ordine.OrdineID = dt.Rows[i]["ordineID"].ToString();
                        ordine.Totale = (decimal)dt.Rows[i]["totale"];
                        ordine.Destinazione = dt.Rows[i]["destinazione"].ToString();
                        ordini.Add(ordine);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return ordini;
        }
        internal static List<ClsOrdine> GetByDestinazione(ref MySqlConnection conn, string destinazione, out string errore)
        {
            DataTable dt = null;
            List<ClsOrdine> ordini = new List<ClsOrdine>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(destinazione))
                errore = "Destinazione non valida";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM ordini WHERE destinazione LIKE @destinazione";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@destinazione", "%" + destinazione + "%");

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsOrdine ordine = new ClsOrdine();
                        ordine.OrdineID = dt.Rows[i]["ordineID"].ToString();
                        ordine.Totale = (decimal)dt.Rows[i]["totale"];
                        ordine.Destinazione = dt.Rows[i]["destinazione"].ToString();
                        ordini.Add(ordine);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return ordini;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, string ordineID, ClsOrdine ordine, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (string.IsNullOrEmpty(ordineID))
                errore = "Ordine ID non valido";
            else
            {
                try
                {
                    conn.Open();

                    string sql = @"UPDATE ordini SET statoOrdine=@statoOrdine, totale=@totale, destinazione=@destinazione 
                                 WHERE ordineID=@ordineID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ordineID", ordineID);
                    cmd.Parameters.AddWithValue("@statoOrdine", "NON_SPEDITO");
                    cmd.Parameters.AddWithValue("@totale", ordine.Totale);
                    cmd.Parameters.AddWithValue("@destinazione", ordine.Destinazione ?? "");

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
        
        internal static long UpdateStato(ref MySqlConnection conn, string ordineID, string nuovoStato, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (string.IsNullOrEmpty(ordineID) || string.IsNullOrEmpty(nuovoStato))
                errore = "Parametri non validi";
            else
            {
                try
                {
                    conn.Open();

                    string sql = "UPDATE ordini SET statoOrdine=@statoOrdine WHERE ordineID=@ordineID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ordineID", ordineID);
                    cmd.Parameters.AddWithValue("@statoOrdine", nuovoStato);

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
        internal static long Delete(ref MySqlConnection conn, string ordineID, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (string.IsNullOrEmpty(ordineID))
                errore = "Ordine ID non valido";
            else
            {
                try
                {
                    conn.Open();

                    string sql = "DELETE FROM ordini WHERE ordineID=@ordineID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ordineID", ordineID);

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
                conn.Open();

                string query = "SELECT COUNT(*) FROM ordini";

                MySqlCommand cmd = new MySqlCommand(query, conn);
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