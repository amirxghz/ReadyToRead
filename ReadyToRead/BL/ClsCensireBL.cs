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
    internal static class ClsCensireBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsCensire censire, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO censire (data, adminID, genereID) 
                             VALUES (@data, @adminID, @genereID)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@data", censire.Data);
                cmd.Parameters.AddWithValue("@adminID", censire.AdminID);
                cmd.Parameters.AddWithValue("@genereID", censire.GenereID);

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
        internal static List<ClsCensire> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsCensire> censimenti = new List<ClsCensire>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM censire";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsCensire censire = new ClsCensire();
                    censire.ID = (long)dt.Rows[i]["ID"];
                    censire.Data = (DateTime)dt.Rows[i]["data"];
                    censire.AdminID = (long)dt.Rows[i]["adminID"];
                    censire.GenereID = (long)dt.Rows[i]["genereID"];
                    censimenti.Add(censire);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return censimenti;
        }

        internal static ClsCensire GetByID(ref MySqlConnection conn, long ID, out string errore)
        {
            DataTable dt = null;
            ClsCensire censire = null;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM censire WHERE ID=@ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ID", ID);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        censire = new ClsCensire();
                        censire.ID = (long)dt.Rows[0]["ID"];
                        censire.Data = (DateTime)dt.Rows[0]["data"];
                        censire.AdminID = (long)dt.Rows[0]["adminID"];
                        censire.GenereID = (long)dt.Rows[0]["genereID"];
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return censire;
        }

        internal static List<ClsCensire> GetByAdminID(ref MySqlConnection conn, long adminID, out string errore)
        {
            DataTable dt = null;
            List<ClsCensire> censimenti = new List<ClsCensire>();
            errore = string.Empty;

            if (adminID <= 0)
                errore = "Admin ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM censire WHERE adminID=@adminID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@adminID", adminID);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsCensire censire = new ClsCensire();
                        censire.ID = (long)dt.Rows[i]["ID"];
                        censire.Data = (DateTime)dt.Rows[i]["data"];
                        censire.AdminID = (long)dt.Rows[i]["adminID"];
                        censire.GenereID = (long)dt.Rows[i]["genereID"];
                        censimenti.Add(censire);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return censimenti;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsCensire censire, out string errore)
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

                    string sql = @"UPDATE censire SET data=@data, adminID=@adminID, genereID=@genereID 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@data", censire.Data);
                    cmd.Parameters.AddWithValue("@adminID", censire.AdminID);
                    cmd.Parameters.AddWithValue("@genereID", censire.GenereID);

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

                    string sql = "DELETE FROM censire WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM censire";

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