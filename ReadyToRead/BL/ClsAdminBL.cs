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
        private const string SELECT_BASE =
            @"SELECT a.ID, a.utenteID,
                     u.nome, u.cognome, u.username, u.password, u.email,
                     u.data_nascita, u.genere, u.comune_nascita
              FROM admins a
              INNER JOIN utenti u ON a.utenteID = u.ID";

        private static ClsAdmin CreaAdminDaRiga(DataRow r)
        {
            ClsAdmin a = new ClsAdmin();
            //a.ID = r["ID"];
            //a.UtenteID = Convert.ToInt32(r["utenteID"]);
            a.Nome = r["nome"] == DBNull.Value ? "" : r["nome"].ToString();
            a.Cognome = r["cognome"] == DBNull.Value ? "" : r["cognome"].ToString();
            a.Username = r["username"] == DBNull.Value ? "" : r["username"].ToString();
            a.Password = r["password"] == DBNull.Value ? "" : r["password"].ToString();
            a.Email = r["email"] == DBNull.Value ? "" : r["email"].ToString();
            if (r["data_nascita"] != DBNull.Value)
                a.DataDiNascita = Convert.ToDateTime(r["data_nascita"]);
            if (r["genere"] != DBNull.Value)
                a.Sesso = r["genere"].ToString() == "m" ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
            if (r["comune_nascita"] != DBNull.Value)
                a.ComuneDiNascita = (ClsUtente.eCOMUNE)Enum.Parse(typeof(ClsUtente.eCOMUNE), r["comune_nascita"].ToString(), true);
            return a;
        }

        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsAdmin admin, out string errore)
        {
            long adminID = 0;
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sqlUtente = @"INSERT INTO utenti (nome, cognome, username, password, email, data_nascita, genere, comune_nascita)
                                     VALUES (@nome, @cognome, @username, @password, @email, @data_nascita, @genere, @comune_nascita)";
                MySqlCommand cmdU = new MySqlCommand(sqlUtente, conn);
                cmdU.Parameters.AddWithValue("@nome", admin.Nome ?? "");
                cmdU.Parameters.AddWithValue("@cognome", admin.Cognome ?? "");
                cmdU.Parameters.AddWithValue("@username", admin.Username ?? "");
                cmdU.Parameters.AddWithValue("@password", admin.Password ?? "");
                cmdU.Parameters.AddWithValue("@email", admin.Email ?? "");
                cmdU.Parameters.AddWithValue("@data_nascita", admin.DataDiNascita);
                cmdU.Parameters.AddWithValue("@genere", admin.Sesso.ToString());
                cmdU.Parameters.AddWithValue("@comune_nascita", admin.ComuneDiNascita.ToString());
                cmdU.ExecuteNonQuery();
                long utenteID = cmdU.LastInsertedId;

                string sqlAdmin = "INSERT INTO admins (utenteID) VALUES (@utenteID)";
                MySqlCommand cmdA = new MySqlCommand(sqlAdmin, conn);
                cmdA.Parameters.AddWithValue("@utenteID", utenteID);
                cmdA.ExecuteNonQuery();
                adminID = cmdA.LastInsertedId;

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return adminID;
        }
        #endregion

        #region READ
        internal static List<ClsAdmin> GetAll(ref MySqlConnection conn, out string errore)
        {
            List<ClsAdmin> admins = new List<ClsAdmin>();
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(SELECT_BASE, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int i = 0;
                while (i < dt.Rows.Count)
                {
                    admins.Add(CreaAdminDaRiga(dt.Rows[i]));
                    i++;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return admins;
        }

        internal static List<ClsAdmin> GetByUsername(ref MySqlConnection conn, string username, out string errore)
        {
            List<ClsAdmin> admins = new List<ClsAdmin>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(username))
            {
                errore = "Username non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = SELECT_BASE + " WHERE u.username LIKE @username";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@username", "%" + username + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        admins.Add(CreaAdminDaRiga(dt.Rows[i]));
                        i++;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return admins;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, string adminIDStr, ClsAdmin admin, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (string.IsNullOrEmpty(adminIDStr))
            {
                errore = "ID non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string sqlUtente = @"UPDATE utenti SET nome=@nome, cognome=@cognome, username=@username,
                                         password=@password, email=@email, data_nascita=@data_nascita,
                                         genere=@genere, comune_nascita=@comune_nascita
                                         WHERE ID=(SELECT utenteID FROM admins WHERE ID=@adminID)";
                    MySqlCommand cmdU = new MySqlCommand(sqlUtente, conn);
                    cmdU.Parameters.AddWithValue("@adminID", adminIDStr);
                    cmdU.Parameters.AddWithValue("@nome", admin.Nome ?? "");
                    cmdU.Parameters.AddWithValue("@cognome", admin.Cognome ?? "");
                    cmdU.Parameters.AddWithValue("@username", admin.Username ?? "");
                    cmdU.Parameters.AddWithValue("@password", admin.Password ?? "");
                    cmdU.Parameters.AddWithValue("@email", admin.Email ?? "");
                    cmdU.Parameters.AddWithValue("@data_nascita", admin.DataDiNascita);
                    cmdU.Parameters.AddWithValue("@genere", admin.Sesso.ToString());
                    cmdU.Parameters.AddWithValue("@comune_nascita", admin.ComuneDiNascita.ToString());
                    esito = cmdU.ExecuteNonQuery();

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
        internal static long Delete(ref MySqlConnection conn, string adminIDStr, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (string.IsNullOrEmpty(adminIDStr))
            {
                errore = "ID non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string queryID = "SELECT utenteID FROM admins WHERE ID=@id";
                    MySqlCommand cmdSel = new MySqlCommand(queryID, conn);
                    cmdSel.Parameters.AddWithValue("@id", adminIDStr);
                    object res = cmdSel.ExecuteScalar();

                    if (res != null)
                    {
                        long utenteID = Convert.ToInt64(res);
                        MySqlCommand cmdDel = new MySqlCommand("DELETE FROM utenti WHERE ID=@utenteID", conn);
                        cmdDel.Parameters.AddWithValue("@utenteID", utenteID);
                        esito = cmdDel.ExecuteNonQuery();
                    }

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

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM admins", conn);
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