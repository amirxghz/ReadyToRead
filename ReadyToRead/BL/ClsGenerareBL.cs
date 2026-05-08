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
    internal static class ClsGenerareBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsGenerare generare, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO generare (data, autoreID, adminID) 
                             VALUES (@data, @autoreID, @adminID)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@data", generare.Data);
                cmd.Parameters.AddWithValue("@autoreID", generare.AutoreID);
                cmd.Parameters.AddWithValue("@adminID", generare.AdminID);

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
        internal static List<ClsGenerare> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsGenerare> generazioni = new List<ClsGenerare>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM generare";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsGenerare generare = new ClsGenerare();
                    generare.ID = (long)dt.Rows[i]["ID"];
                    generare.Data = (DateTime)dt.Rows[i]["data"];
                    generare.AutoreID = (long)dt.Rows[i]["autoreID"];
                    generare.AdminID = (long)dt.Rows[i]["adminID"];
                    generazioni.Add(generare);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return generazioni;
        }

        internal static ClsGenerare GetByID(ref MySqlConnection conn, long ID, out string errore)
        {
            DataTable dt = null;
            ClsGenerare generare = null;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM generare WHERE ID=@ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ID", ID);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        generare = new ClsGenerare();
                        generare.ID = (long)dt.Rows[0]["ID"];
                        generare.Data = (DateTime)dt.Rows[0]["data"];
                        generare.AutoreID = (long)dt.Rows[0]["autoreID"];
                        generare.AdminID = (long)dt.Rows[0]["adminID"];
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return generare;
        }

        internal static List<ClsGenerare> GetByAutoreID(ref MySqlConnection conn, long autoreID, out string errore)
        {
            DataTable dt = null;
            List<ClsGenerare> generazioni = new List<ClsGenerare>();
            errore = string.Empty;

            if (autoreID <= 0)
                errore = "Autore ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM generare WHERE autoreID=@autoreID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@autoreID", autoreID);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsGenerare generare = new ClsGenerare();
                        generare.ID = (long)dt.Rows[i]["ID"];
                        generare.Data = (DateTime)dt.Rows[i]["data"];
                        generare.AutoreID = (long)dt.Rows[i]["autoreID"];
                        generare.AdminID = (long)dt.Rows[i]["adminID"];
                        generazioni.Add(generare);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return generazioni;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsGenerare generare, out string errore)
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

                    string sql = @"UPDATE generare SET data=@data, autoreID=@autoreID, adminID=@adminID 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@data", generare.Data);
                    cmd.Parameters.AddWithValue("@autoreID", generare.AutoreID);
                    cmd.Parameters.AddWithValue("@adminID", generare.AdminID);

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

                    string sql = "DELETE FROM generare WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM generare";

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