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
    internal static class ClsCaratterizzareBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsCaratterizzare caratterizzare, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO caratterizzare (libroISBN, genereID) 
                             VALUES (@libroISBN, @genereID)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@libroISBN", caratterizzare.LibroISBN ?? "");
                cmd.Parameters.AddWithValue("@genereID", caratterizzare.GenereID);

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
        internal static List<ClsCaratterizzare> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsCaratterizzare> caratterizzazioni = new List<ClsCaratterizzare>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM caratterizzare";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                int i = 0;
                while (i < dt.Rows.Count)
                {
                    ClsCaratterizzare caratterizzare = new ClsCaratterizzare();
                    caratterizzare.ID = (long)dt.Rows[i]["ID"];
                    caratterizzare.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                    caratterizzare.GenereID = (int)dt.Rows[i]["genereID"];
                    caratterizzazioni.Add(caratterizzare);
                    i++;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return caratterizzazioni;
        }

        internal static ClsCaratterizzare GetByID(ref MySqlConnection conn, long ID, out string errore)
        {
            DataTable dt = null;
            ClsCaratterizzare caratterizzare = null;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM caratterizzare WHERE ID=@ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ID", ID);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        caratterizzare = new ClsCaratterizzare();
                        caratterizzare.ID = (long)dt.Rows[0]["ID"];
                        caratterizzare.LibroISBN = dt.Rows[0]["libroISBN"].ToString();
                        caratterizzare.GenereID = (int)dt.Rows[0]["genereID"];
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return caratterizzare;
        }

        internal static List<ClsCaratterizzare> GetByLibroISBN(ref MySqlConnection conn, string libroISBN, out string errore)
        {
            DataTable dt = null;
            List<ClsCaratterizzare> caratterizzazioni = new List<ClsCaratterizzare>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(libroISBN))
                errore = "ISBN non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM caratterizzare WHERE libroISBN=@libroISBN";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@libroISBN", libroISBN);

                    dt = new DataTable();
                    da.Fill(dt);

                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        ClsCaratterizzare caratterizzare = new ClsCaratterizzare();
                        caratterizzare.ID = (long)dt.Rows[i]["ID"];
                        caratterizzare.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                        caratterizzare.GenereID = (int)dt.Rows[i]["genereID"];
                        caratterizzazioni.Add(caratterizzare);
                        i++;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return caratterizzazioni;
        }

        internal static List<ClsCaratterizzare> GetByGenereID(ref MySqlConnection conn, long genereID, out string errore)
        {
            DataTable dt = null;
            List<ClsCaratterizzare> caratterizzazioni = new List<ClsCaratterizzare>();
            errore = string.Empty;

            if (genereID <= 0)
                errore = "Genere ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM caratterizzare WHERE genereID=@genereID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@genereID", genereID);

                    dt = new DataTable();
                    da.Fill(dt);

                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        ClsCaratterizzare caratterizzare = new ClsCaratterizzare();
                        caratterizzare.ID = (long)dt.Rows[i]["ID"];
                        caratterizzare.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                        caratterizzare.GenereID = (int)dt.Rows[i]["genereID"];
                        caratterizzazioni.Add(caratterizzare);
                        i++;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return caratterizzazioni;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsCaratterizzare caratterizzare, out string errore)
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

                    string sql = @"UPDATE caratterizzare SET libroISBN=@libroISBN, genereID=@genereID 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@libroISBN", caratterizzare.LibroISBN ?? "");
                    cmd.Parameters.AddWithValue("@genereID", caratterizzare.GenereID);

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

                    string sql = "DELETE FROM caratterizzare WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM caratterizzare";

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