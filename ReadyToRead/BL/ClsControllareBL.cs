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
    internal static class ClsControllareBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsControllare controllare, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO controllare (data, adminID, prodottoID) 
                             VALUES (@data, @adminID, @prodottoID)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@data", controllare.Data);
                cmd.Parameters.AddWithValue("@adminID", controllare.AdminID);
                cmd.Parameters.AddWithValue("@prodottoID", controllare.ProdottoID);

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
        internal static List<ClsControllare> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsControllare> controlli = new List<ClsControllare>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM controllare";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsControllare controllare = new ClsControllare();
                    controllare.ID = (long)dt.Rows[i]["ID"];
                    controllare.Data = (DateTime)dt.Rows[i]["data"];
                    controllare.AdminID = (long)dt.Rows[i]["adminID"];
                    controllare.ProdottoID = (long)dt.Rows[i]["prodottoID"];
                    controlli.Add(controllare);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return controlli;
        }

        internal static ClsControllare GetByID(ref MySqlConnection conn, long ID, out string errore)
        {
            DataTable dt = null;
            ClsControllare controllare = null;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM controllare WHERE ID=@ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ID", ID);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        controllare = new ClsControllare();
                        controllare.ID = (long)dt.Rows[0]["ID"];
                        controllare.Data = (DateTime)dt.Rows[0]["data"];
                        controllare.AdminID = (long)dt.Rows[0]["adminID"];
                        controllare.ProdottoID = (long)dt.Rows[0]["prodottoID"];
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return controllare;
        }

        internal static List<ClsControllare> GetByAdminID(ref MySqlConnection conn, long adminID, out string errore)
        {
            DataTable dt = null;
            List<ClsControllare> controlli = new List<ClsControllare>();
            errore = string.Empty;

            if (adminID <= 0)
                errore = "Admin ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM controllare WHERE adminID=@adminID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@adminID", adminID);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsControllare controllare = new ClsControllare();
                        controllare.ID = (long)dt.Rows[i]["ID"];
                        controllare.Data = (DateTime)dt.Rows[i]["data"];
                        controllare.AdminID = (long)dt.Rows[i]["adminID"];
                        controllare.ProdottoID = (long)dt.Rows[i]["prodottoID"];
                        controlli.Add(controllare);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return controlli;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsControllare controllare, out string errore)
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

                    string sql = @"UPDATE controllare SET data=@data, adminID=@adminID, prodottoID=@prodottoID 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@data", controllare.Data);
                    cmd.Parameters.AddWithValue("@adminID", controllare.AdminID);
                    cmd.Parameters.AddWithValue("@prodottoID", controllare.ProdottoID);

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

                    string sql = "DELETE FROM controllare WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM controllare";

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