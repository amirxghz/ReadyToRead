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
    internal static class ClsPubblicareBL //Urbani
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsPubblicare pubblicare, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO pubblicare (anno_pubblicazione, edizione, casa_editriceID, libroISBN) 
                             VALUES (@annoPubblicazione, @edizione, @casaEditriceID, @libroISBN)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@annoPubblicazione", pubblicare.AnnoPubblicazione);
                cmd.Parameters.AddWithValue("@edizione", pubblicare.Edizione ?? "");
                cmd.Parameters.AddWithValue("@casaEditriceID", pubblicare.CasaEditriceID);
                cmd.Parameters.AddWithValue("@libroISBN", pubblicare.LibroISBN ?? "");

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
        internal static List<ClsPubblicare> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsPubblicare> pubblicazioni = new List<ClsPubblicare>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM pubblicare";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsPubblicare pubblicare = new ClsPubblicare();
                    pubblicare.ID = (long)dt.Rows[i]["ID"];
                    pubblicare.AnnoPubblicazione = (DateTime)dt.Rows[i]["anno_pubblicazione"];
                    pubblicare.Edizione = dt.Rows[i]["edizione"].ToString();
                    pubblicare.CasaEditriceID = (long)dt.Rows[i]["casa_editriceID"];
                    pubblicare.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                    pubblicazioni.Add(pubblicare);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return pubblicazioni;
        }

        internal static ClsPubblicare GetByID(ref MySqlConnection conn, long ID, out string errore)
        {
            DataTable dt = null;
            ClsPubblicare pubblicare = null;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM pubblicare WHERE ID=@ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ID", ID);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        pubblicare = new ClsPubblicare();
                        pubblicare.ID = (long)dt.Rows[0]["ID"];
                        pubblicare.AnnoPubblicazione = (DateTime)dt.Rows[0]["anno_pubblicazione"];
                        pubblicare.Edizione = dt.Rows[0]["edizione"].ToString();
                        pubblicare.CasaEditriceID = (long)dt.Rows[0]["casa_editriceID"];
                        pubblicare.LibroISBN = dt.Rows[0]["libroISBN"].ToString();
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return pubblicare;
        }

        internal static List<ClsPubblicare> GetByCasaEditriceID(ref MySqlConnection conn, long casaEditriceID, out string errore)
        {
            DataTable dt = null;
            List<ClsPubblicare> pubblicazioni = new List<ClsPubblicare>();
            errore = string.Empty;

            if (casaEditriceID <= 0)
                errore = "Casa editrice ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM pubblicare WHERE casa_editriceID=@casaEditriceID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@casaEditriceID", casaEditriceID);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsPubblicare pubblicare = new ClsPubblicare();
                        pubblicare.ID = (long)dt.Rows[i]["ID"];
                        pubblicare.AnnoPubblicazione = (DateTime)dt.Rows[i]["anno_pubblicazione"];
                        pubblicare.Edizione = dt.Rows[i]["edizione"].ToString();
                        pubblicare.CasaEditriceID = (long)dt.Rows[i]["casa_editriceID"];
                        pubblicare.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                        pubblicazioni.Add(pubblicare);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return pubblicazioni;
        }

        internal static List<ClsPubblicare> GetByLibroISBN(ref MySqlConnection conn, string libroISBN, out string errore)
        {
            DataTable dt = null;
            List<ClsPubblicare> pubblicazioni = new List<ClsPubblicare>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(libroISBN))
                errore = "ISBN non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM pubblicare WHERE libroISBN=@libroISBN";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@libroISBN", libroISBN);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsPubblicare pubblicare = new ClsPubblicare();
                        pubblicare.ID = (long)dt.Rows[i]["ID"];
                        pubblicare.AnnoPubblicazione = (DateTime)dt.Rows[i]["anno_pubblicazione"];
                        pubblicare.Edizione = dt.Rows[i]["edizione"].ToString();
                        pubblicare.CasaEditriceID = (long)dt.Rows[i]["casa_editriceID"];
                        pubblicare.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                        pubblicazioni.Add(pubblicare);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return pubblicazioni;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsPubblicare pubblicare, out string errore)
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

                    string sql = @"UPDATE pubblicare SET anno_pubblicazione=@annoPubblicazione, edizione=@edizione, 
                                 casa_editriceID=@casaEditriceID, libroISBN=@libroISBN 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@annoPubblicazione", pubblicare.AnnoPubblicazione);
                    cmd.Parameters.AddWithValue("@edizione", pubblicare.Edizione ?? "");
                    cmd.Parameters.AddWithValue("@casaEditriceID", pubblicare.CasaEditriceID);
                    cmd.Parameters.AddWithValue("@libroISBN", pubblicare.LibroISBN ?? "");

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

                    string sql = "DELETE FROM pubblicare WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM pubblicare";

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