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
    internal static class ClsRecensioneBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsRecensire recensione, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO recensire (titolo, descrizione, valutazione, username) 
                             VALUES (@titolo, @descrizione, @valutazione, @username)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@titolo", recensione.Titolo ?? "");
                cmd.Parameters.AddWithValue("@descrizione", recensione.Descrizione ?? "");
                cmd.Parameters.AddWithValue("@valutazione", 1);
                cmd.Parameters.AddWithValue("@username", "");

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
        internal static List<ClsRecensire> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsRecensire> recensioni = new List<ClsRecensire>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                string query = "SELECT * FROM recensire";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsRecensire recensione = new ClsRecensire();
                    recensione.Titolo = dt.Rows[i]["titolo"].ToString();
                    recensione.Descrizione = dt.Rows[i]["descrizione"].ToString();
                    recensioni.Add(recensione);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return recensioni;
        }
        
        internal static List<ClsRecensire> GetByValutazione(ref MySqlConnection conn, int valutazione, out string errore)
        {
            DataTable dt = null;
            List<ClsRecensire> recensioni = new List<ClsRecensire>();
            errore = string.Empty;

            if (valutazione < 1 || valutazione > 5)
                errore = "Valutazione non valida (1-5)";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM recensire WHERE valutazione=@valutazione";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@valutazione", valutazione);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsRecensire recensione = new ClsRecensire();
                        recensione.Titolo = dt.Rows[i]["titolo"].ToString();
                        recensione.Descrizione = dt.Rows[i]["descrizione"].ToString();
                        recensioni.Add(recensione);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return recensioni;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsRecensire recensione, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string sql = @"UPDATE recensire SET titolo=@titolo, descrizione=@descrizione 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@titolo", recensione.Titolo ?? "");
                    cmd.Parameters.AddWithValue("@descrizione", recensione.Descrizione ?? "");

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
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    string sql = "DELETE FROM recensire WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
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
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                string query = "SELECT COUNT(*) FROM recensire";

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