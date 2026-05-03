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
    internal static class ClsAdminBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsAdmin admin, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                conn.Open();

                string sql = @"INSERT INTO admins (ID, username, password, email, nome, cognome, dataDiNascita, codiceFiscale, comuneDiNascita, sesso) 
                             VALUES (@ID, @username, @password, @email, @nome, @cognome, @dataDiNascita, @codiceFiscale, @comuneDiNascita, @sesso)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", admin.ID ?? "");
                cmd.Parameters.AddWithValue("@username", admin.Username ?? "");
                cmd.Parameters.AddWithValue("@password", admin.Password ?? "");
                cmd.Parameters.AddWithValue("@email", admin.Email ?? "");
                cmd.Parameters.AddWithValue("@nome", admin.Nome ?? "");
                cmd.Parameters.AddWithValue("@cognome", admin.Cognome ?? "");
                cmd.Parameters.AddWithValue("@dataDiNascita", admin.DataDiNascita);
                cmd.Parameters.AddWithValue("@codiceFiscale", admin.CodiceFiscale ?? "");
                cmd.Parameters.AddWithValue("@comuneDiNascita", admin.ComuneDiNascita);
                cmd.Parameters.AddWithValue("@sesso", admin.Sesso);

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
        internal static List<ClsAdmin> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsAdmin> admin = new List<ClsAdmin>();
            errore = string.Empty;

            try
            {
                conn.Open();

                string query = "SELECT * FROM admins";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsAdmin adm = new ClsAdmin();
                    adm.ID = dt.Rows[i]["ID"].ToString();
                    adm.Username = dt.Rows[i]["username"].ToString();
                    adm.Password = dt.Rows[i]["password"].ToString();
                    adm.Email = dt.Rows[i]["email"].ToString();
                    admin.Add(adm);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return admin;
        }
        
        internal static List<ClsAdmin> GetByUsername(ref MySqlConnection conn, string username, out string errore)
        {
            DataTable dt = null;
            List<ClsAdmin> admin = new List<ClsAdmin>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(username))
                errore = "Username non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM admins WHERE username LIKE @username";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@username", "%" + username + "%");

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsAdmin adm = new ClsAdmin();
                        adm.ID = dt.Rows[i]["ID"].ToString();
                        adm.Username = dt.Rows[i]["username"].ToString();
                        adm.Password = dt.Rows[i]["password"].ToString();
                        adm.Email = dt.Rows[i]["email"].ToString();
                        admin.Add(adm);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return admin;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, string ID, ClsAdmin admin, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (string.IsNullOrEmpty(ID))
                errore = "ID non valido";
            else
            {
                try
                {
                    conn.Open();

                    string sql = @"UPDATE admins SET username=@username, password=@password, email=@email 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@username", admin.Username ?? "");
                    cmd.Parameters.AddWithValue("@password", admin.Password ?? "");
                    cmd.Parameters.AddWithValue("@email", admin.Email ?? "");

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
        internal static long Delete(ref MySqlConnection conn, string ID, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (string.IsNullOrEmpty(ID))
                errore = "ID non valido";
            else
            {
                try
                {
                    conn.Open();

                    string sql = "DELETE FROM admins WHERE ID=@ID";

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
                conn.Open();

                string query = "SELECT COUNT(*) FROM admins";

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