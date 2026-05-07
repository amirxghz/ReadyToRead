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
    internal static class ClsScrivereBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsScrivere scrivere, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO scrivere (data, autoreID, libroISBN) 
                             VALUES (@data, @autoreID, @libroISBN)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@data", scrivere.Data);
                cmd.Parameters.AddWithValue("@autoreID", scrivere.AutoreID);
                cmd.Parameters.AddWithValue("@libroISBN", scrivere.LibroISBN ?? "");

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
        internal static List<ClsScrivere> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsScrivere> scritture = new List<ClsScrivere>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM scrivere";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsScrivere scrivere = new ClsScrivere();
                    scrivere.ID = (long)dt.Rows[i]["ID"];
                    scrivere.Data = (DateTime)dt.Rows[i]["data"];
                    scrivere.AutoreID = (long)dt.Rows[i]["autoreID"];
                    scrivere.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                    scritture.Add(scrivere);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return scritture;
        }

        internal static ClsScrivere GetByID(ref MySqlConnection conn, long ID, out string errore)
        {
            DataTable dt = null;
            ClsScrivere scrivere = null;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM scrivere WHERE ID=@ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ID", ID);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        scrivere = new ClsScrivere();
                        scrivere.ID = (long)dt.Rows[0]["ID"];
                        scrivere.Data = (DateTime)dt.Rows[0]["data"];
                        scrivere.AutoreID = (long)dt.Rows[0]["autoreID"];
                        scrivere.LibroISBN = dt.Rows[0]["libroISBN"].ToString();
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return scrivere;
        }

        internal static List<ClsScrivere> GetByAutoreID(ref MySqlConnection conn, long autoreID, out string errore)
        {
            DataTable dt = null;
            List<ClsScrivere> scritture = new List<ClsScrivere>();
            errore = string.Empty;

            if (autoreID <= 0)
                errore = "Autore ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM scrivere WHERE autoreID=@autoreID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@autoreID", autoreID);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsScrivere scrivere = new ClsScrivere();
                        scrivere.ID = (long)dt.Rows[i]["ID"];
                        scrivere.Data = (DateTime)dt.Rows[i]["data"];
                        scrivere.AutoreID = (long)dt.Rows[i]["autoreID"];
                        scrivere.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                        scritture.Add(scrivere);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return scritture;
        }

        internal static List<ClsScrivere> GetByLibroISBN(ref MySqlConnection conn, string libroISBN, out string errore)
        {
            DataTable dt = null;
            List<ClsScrivere> scritture = new List<ClsScrivere>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(libroISBN))
                errore = "ISBN non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM scrivere WHERE libroISBN=@libroISBN";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@libroISBN", libroISBN);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsScrivere scrivere = new ClsScrivere();
                        scrivere.ID = (long)dt.Rows[i]["ID"];
                        scrivere.Data = (DateTime)dt.Rows[i]["data"];
                        scrivere.AutoreID = (long)dt.Rows[i]["autoreID"];
                        scrivere.LibroISBN = dt.Rows[i]["libroISBN"].ToString();
                        scritture.Add(scrivere);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return scritture;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsScrivere scrivere, out string errore)
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

                    string sql = @"UPDATE scrivere SET data=@data, autoreID=@autoreID, libroISBN=@libroISBN 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@data", scrivere.Data);
                    cmd.Parameters.AddWithValue("@autoreID", scrivere.AutoreID);
                    cmd.Parameters.AddWithValue("@libroISBN", scrivere.LibroISBN ?? "");

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

                    string sql = "DELETE FROM scrivere WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM scrivere";

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