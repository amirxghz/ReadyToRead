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
    internal static class ClsUtenteBL //Amir
    {
        private static ClsUtente CreaUtenteDaRiga(DataRow r)
        {
            ClsUtente u = new ClsUtente();
            u.ID = Convert.ToInt64(r["ID"]);
            u.Nome = r["nome"] == DBNull.Value ? "" : r["nome"].ToString();
            u.Cognome = r["cognome"] == DBNull.Value ? "" : r["cognome"].ToString();
            u.Username = r["username"] == DBNull.Value ? "" : r["username"].ToString();
            u.Password = r["password"] == DBNull.Value ? "" : r["password"].ToString();
            u.Email = r["email"] == DBNull.Value ? "" : r["email"].ToString();
            if (r["data_nascita"] != DBNull.Value)
                u.DataDiNascita = Convert.ToDateTime(r["data_nascita"]);
            if (r["genere"] != DBNull.Value)
                u.Sesso = r["genere"].ToString() == "m" ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
            if (r["comune_nascita"] != DBNull.Value)
                u.ComuneDiNascita = (ClsUtente.eCOMUNE)Enum.Parse(typeof(ClsUtente.eCOMUNE), r["comune_nascita"].ToString(), true);
            u.Foto_profilo = r["foto_profilo"] == DBNull.Value ? "" : r["foto_profilo"].ToString();

            return u;
        }

        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsUtente utente, out string errore)
        {
            long ID = 0;
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO utenti (nome, cognome, username, password, email, data_nascita, genere, comune_nascita, foto_profilo)
                      VALUES (@nome, @cognome, @username, SHA2(@password, 256), @email, @data_nascita, @genere, @comune_nascita, @foto_profilo)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", utente.Nome ?? "");
                cmd.Parameters.AddWithValue("@cognome", utente.Cognome ?? "");
                cmd.Parameters.AddWithValue("@username", utente.Username ?? "");
                cmd.Parameters.AddWithValue("@password", utente.Password ?? "");
                cmd.Parameters.AddWithValue("@email", utente.Email ?? "");
                cmd.Parameters.AddWithValue("@data_nascita", utente.DataDiNascita);
                cmd.Parameters.AddWithValue("@genere", utente.Sesso.ToString());
                cmd.Parameters.AddWithValue("@comune_nascita", utente.ComuneDiNascita.ToString());
                cmd.Parameters.AddWithValue("@foto_profilo", utente.Foto_profilo ?? "");

                cmd.ExecuteNonQuery();
                long utenteID = cmd.LastInsertedId;

                if (utente is ClsAdmin)
                {
                    string sqlAdmin = "INSERT INTO admins (utenteID) VALUES (@utenteID)";
                    MySqlCommand cmdA = new MySqlCommand(sqlAdmin, conn);
                    cmdA.Parameters.AddWithValue("@utenteID", utenteID);
                    cmdA.ExecuteNonQuery();
                    ID = cmdA.LastInsertedId;
                }
                else if (utente is ClsCliente)
                {
                    ClsCliente cliente = (ClsCliente)utente;
                    string sqlCliente = @"INSERT INTO clienti (indirizzo, cap, utenteID)
                                  VALUES (@indirizzo, @cap, @utenteID)";
                    MySqlCommand cmdC = new MySqlCommand(sqlCliente, conn);
                    cmdC.Parameters.AddWithValue("@indirizzo", cliente.Indirizzo ?? "");
                    cmdC.Parameters.AddWithValue("@cap", cliente.CAP ?? "");
                    cmdC.Parameters.AddWithValue("@utenteID", utenteID);
                    cmdC.ExecuteNonQuery();
                    ID = cmdC.LastInsertedId;
                }

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
        internal static List<ClsUtente> GetAll(ref MySqlConnection conn, out string errore)
        {
            List<ClsUtente> utenti = new List<ClsUtente>();
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string query = @"SELECT u.*,
                                    CASE 
                                        WHEN a.utenteID IS NOT NULL THEN 'admin'
                                        WHEN c.utenteID IS NOT NULL THEN 'cliente'
                                        ELSE 'utente'
                                    END AS tipo_utente
                                 FROM utenti u
                                 LEFT JOIN admins a ON a.utenteID = u.ID
                                 LEFT JOIN clienti c ON c.utenteID = u.ID";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tipo = dt.Rows[i]["tipo_utente"].ToString();
                    ClsUtente u;
                    if (tipo == "admin")
                        u = new ClsAdmin();
                    else if (tipo == "cliente")
                        u = new ClsCliente();
                    else
                        u = new ClsUtente();

                    u.ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                    u.Nome = dt.Rows[i]["nome"] == DBNull.Value ? "" : dt.Rows[i]["nome"].ToString();
                    u.Cognome = dt.Rows[i]["cognome"] == DBNull.Value ? "" : dt.Rows[i]["cognome"].ToString();
                    u.Username = dt.Rows[i]["username"] == DBNull.Value ? "" : dt.Rows[i]["username"].ToString();
                    u.Password = dt.Rows[i]["password"] == DBNull.Value ? "" : dt.Rows[i]["password"].ToString();
                    u.Email = dt.Rows[i]["email"] == DBNull.Value ? "" : dt.Rows[i]["email"].ToString();
                    if (dt.Rows[i]["data_nascita"] != DBNull.Value)
                        u.DataDiNascita = Convert.ToDateTime(dt.Rows[i]["data_nascita"]);
                    if (dt.Rows[i]["genere"] != DBNull.Value)
                        u.Sesso = dt.Rows[i]["genere"].ToString() == "m" ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
                    if (dt.Rows[i]["comune_nascita"] != DBNull.Value)
                        u.ComuneDiNascita = (ClsUtente.eCOMUNE)Enum.Parse(typeof(ClsUtente.eCOMUNE), dt.Rows[i]["comune_nascita"].ToString(), true);
                    u.Foto_profilo = dt.Rows[i]["foto_profilo"] == DBNull.Value ? "" : dt.Rows[i]["foto_profilo"].ToString();

                    utenti.Add(u);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return utenti;
        }

        internal static List<ClsUtente> GetByUsername(ref MySqlConnection conn, string username, out string errore)
        {
            List<ClsUtente> utenti = new List<ClsUtente>();
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

                    string query = @"SELECT u.*,
                                        CASE 
                                            WHEN a.utenteID IS NOT NULL THEN 'admin'
                                            WHEN c.utenteID IS NOT NULL THEN 'cliente'
                                            ELSE 'utente'
                                        END AS tipo_utente
                                     FROM utenti u
                                     LEFT JOIN admins a ON a.utenteID = u.ID
                                     LEFT JOIN clienti c ON c.utenteID = u.ID
                                     WHERE u.username LIKE @username";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@username", "%" + username + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string tipo = dt.Rows[i]["tipo_utente"].ToString();
                        ClsUtente u;
                        if (tipo == "admin")
                            u = new ClsAdmin();
                        else if (tipo == "cliente")
                            u = new ClsCliente();
                        else
                            u = new ClsUtente();

                        u.ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                        u.Nome = dt.Rows[i]["nome"] == DBNull.Value ? "" : dt.Rows[i]["nome"].ToString();
                        u.Cognome = dt.Rows[i]["cognome"] == DBNull.Value ? "" : dt.Rows[i]["cognome"].ToString();
                        u.Username = dt.Rows[i]["username"] == DBNull.Value ? "" : dt.Rows[i]["username"].ToString();
                        u.Password = dt.Rows[i]["password"] == DBNull.Value ? "" : dt.Rows[i]["password"].ToString();
                        u.Email = dt.Rows[i]["email"] == DBNull.Value ? "" : dt.Rows[i]["email"].ToString();
                        if (dt.Rows[i]["data_nascita"] != DBNull.Value)
                            u.DataDiNascita = Convert.ToDateTime(dt.Rows[i]["data_nascita"]);
                        if (dt.Rows[i]["genere"] != DBNull.Value)
                            u.Sesso = dt.Rows[i]["genere"].ToString() == "m" ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
                        if (dt.Rows[i]["comune_nascita"] != DBNull.Value)
                            u.ComuneDiNascita = (ClsUtente.eCOMUNE)Enum.Parse(typeof(ClsUtente.eCOMUNE), dt.Rows[i]["comune_nascita"].ToString(), true);
                        u.Foto_profilo = dt.Rows[i]["foto_profilo"] == DBNull.Value ? "" : dt.Rows[i]["foto_profilo"].ToString();

                        utenti.Add(u);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return utenti;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsUtente utente, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (ID <= 0)
            {
                errore = "ID non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string sql = @"UPDATE utenti SET nome=@nome, cognome=@cognome, username=@username, password=@password, email=@email, data_nascita=@data_nascita,
                           genere=@genere, comune_nascita=@comune_nascita, foto_profilo=@foto_profilo
                           WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@nome", utente.Nome ?? "");
                    cmd.Parameters.AddWithValue("@cognome", utente.Cognome ?? "");
                    cmd.Parameters.AddWithValue("@username", utente.Username ?? "");
                    cmd.Parameters.AddWithValue("@password", utente.Password ?? "");
                    cmd.Parameters.AddWithValue("@email", utente.Email ?? "");
                    cmd.Parameters.AddWithValue("@data_nascita", utente.DataDiNascita);
                    cmd.Parameters.AddWithValue("@genere", utente.Sesso.ToString());
                    cmd.Parameters.AddWithValue("@comune_nascita", utente.ComuneDiNascita.ToString());
                    cmd.Parameters.AddWithValue("@foto_profilo", utente.Foto_profilo ?? "");

                    esito = cmd.ExecuteNonQuery();

                    if (utente is ClsAdmin)
                    {
                        ClsAdmin admin = (ClsAdmin)utente;
                        string sqlAdmin = @"UPDATE admins SET utenteID=@utenteID
                                    WHERE utenteID=@utenteID";
                        MySqlCommand cmdA = new MySqlCommand(sqlAdmin, conn);
                        cmdA.Parameters.AddWithValue("@utenteID", ID);
                        cmdA.ExecuteNonQuery();
                    }
                    else if (utente is ClsCliente)
                    {
                        ClsCliente cliente = (ClsCliente)utente;
                        string sqlCliente = @"UPDATE clienti SET indirizzo=@indirizzo, cap=@cap 
                                      WHERE utenteID=@utenteID";
                        MySqlCommand cmdC = new MySqlCommand(sqlCliente, conn);
                        cmdC.Parameters.AddWithValue("@utenteID", ID);
                        cmdC.Parameters.AddWithValue("@indirizzo", cliente.Indirizzo ?? "");
                        cmdC.Parameters.AddWithValue("@cap", cliente.CAP ?? "");
                        cmdC.ExecuteNonQuery();
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

        #region DELETE
        internal static long Delete(ref MySqlConnection conn, long ID, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (ID <= 0)
            {
                errore = "ID non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    MySqlCommand cmdDelAdmin = new MySqlCommand("DELETE FROM admins WHERE utenteID=@ID", conn);
                    cmdDelAdmin.Parameters.AddWithValue("@ID", ID);
                    cmdDelAdmin.ExecuteNonQuery();

                    MySqlCommand cmdDelCliente = new MySqlCommand("DELETE FROM clienti WHERE utenteID=@ID", conn);
                    cmdDelCliente.Parameters.AddWithValue("@ID", ID);
                    cmdDelCliente.ExecuteNonQuery();

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM utenti WHERE ID=@ID", conn);
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
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM utenti", conn);
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

        #region LOGIN
        //Urbani
        internal static string Login(ref MySqlConnection conn, string username, string password)
        {
            string accesso = string.Empty;

            if (string.IsNullOrEmpty(username))
            {
                accesso = "Username non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    // Prima verifica che l'utente esista
                    string query = "SELECT * FROM utenti WHERE username = @username";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@username", username);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        query = "SELECT * FROM utenti WHERE username = @username AND password = SHA2(@password, 256)";
                        da = new MySqlDataAdapter(query, conn);
                        da.SelectCommand.Parameters.AddWithValue("@username", username);
                        da.SelectCommand.Parameters.AddWithValue("@password", password);
                        dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            long utenteID = CreaUtenteDaRiga(dt.Rows[0]).ID;

                            query = "SELECT * FROM clienti WHERE utenteID = @ID";
                            da = new MySqlDataAdapter(query, conn);
                            da.SelectCommand.Parameters.AddWithValue("@ID", utenteID);
                            dt = new DataTable();
                            da.Fill(dt);

                            accesso = dt.Rows.Count > 0 ? "garantitocliente" : "garantitoadmin";
                        }
                        else
                            accesso = "Password errata";
                    }
                    else
                        accesso = "Utente inesistente";

                    conn.Close();
                }
                catch (Exception ex)
                {
                    accesso = ex.Message;
                }
            }

            return accesso;
        }
        #endregion
    }
}