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
    internal static class ClsClienteBL //Urbani
    {
        private const string SELECT_BASE =
            @"SELECT c.ID, c.indirizzo, c.cap, c.utenteID,
                     u.nome, u.cognome, u.username, u.password, u.email,
                     u.data_nascita, u.genere, u.comune_nascita
              FROM clienti c
              INNER JOIN utenti u ON c.utenteID = u.ID";

        private static ClsCliente CreaClienteDaRiga(DataRow r)
        {
            ClsCliente c = new ClsCliente();
            c.ID = Convert.ToInt64(r["ID"]);
            c.UtenteID = Convert.ToInt32(r["utenteID"]);
            c.Indirizzo = r["indirizzo"] == DBNull.Value ? "" : r["indirizzo"].ToString();
            c.CAP = r["cap"] == DBNull.Value ? "" : r["cap"].ToString();
            c.Nome = r["nome"] == DBNull.Value ? "" : r["nome"].ToString();
            c.Cognome = r["cognome"] == DBNull.Value ? "" : r["cognome"].ToString();
            c.Username = r["username"] == DBNull.Value ? "" : r["username"].ToString();
            c.Password = r["password"] == DBNull.Value ? "" : r["password"].ToString();
            c.Email = r["email"] == DBNull.Value ? "" : r["email"].ToString();
            if (r["data_nascita"] != DBNull.Value)
                c.DataDiNascita = Convert.ToDateTime(r["data_nascita"]);
            if (r["genere"] != DBNull.Value)
                c.Sesso = r["genere"].ToString() == "m" ? ClsUtente.eSESSO.m : ClsUtente.eSESSO.f;
            if (r["comune_nascita"] != DBNull.Value)
                c.ComuneDiNascita = (ClsUtente.eCOMUNE)Enum.Parse(typeof(ClsUtente.eCOMUNE), r["comune_nascita"].ToString(), true);
            return c;
        }

        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsCliente cliente, out string errore)
        {
            long clienteID = 0;
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sqlUtente = @"INSERT INTO utenti (nome, cognome, username, password, email, data_nascita, genere, comune_nascita)
                                     VALUES (@nome, @cognome, @username, SHA2(@password, 256), @email, @data_nascita, @genere, @comune_nascita)";
                MySqlCommand cmdU = new MySqlCommand(sqlUtente, conn);
                cmdU.Parameters.AddWithValue("@nome", cliente.Nome ?? "");
                cmdU.Parameters.AddWithValue("@cognome", cliente.Cognome ?? "");
                cmdU.Parameters.AddWithValue("@username", cliente.Username ?? "");
                cmdU.Parameters.AddWithValue("@password", cliente.Password ?? "");
                cmdU.Parameters.AddWithValue("@email", cliente.Email ?? "");
                cmdU.Parameters.AddWithValue("@data_nascita", cliente.DataDiNascita);
                cmdU.Parameters.AddWithValue("@genere", cliente.Sesso.ToString());
                cmdU.Parameters.AddWithValue("@comune_nascita", cliente.ComuneDiNascita.ToString());
                cmdU.ExecuteNonQuery();
                long utenteID = cmdU.LastInsertedId;

                string sqlCliente = @"INSERT INTO clienti (indirizzo, cap, utenteID)
                                      VALUES (@indirizzo, @cap, @utenteID)";
                MySqlCommand cmdC = new MySqlCommand(sqlCliente, conn);
                cmdC.Parameters.AddWithValue("@indirizzo", cliente.Indirizzo ?? "");
                cmdC.Parameters.AddWithValue("@cap", cliente.CAP ?? "");
                cmdC.Parameters.AddWithValue("@utenteID", utenteID);
                cmdC.ExecuteNonQuery();
                clienteID = cmdC.LastInsertedId;

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return clienteID;
        }
        #endregion

        #region READ
        internal static List<ClsCliente> GetAll(ref MySqlConnection conn, out string errore)
        {
            List<ClsCliente> clienti = new List<ClsCliente>();
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
                    clienti.Add(CreaClienteDaRiga(dt.Rows[i]));
                    i++;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return clienti;
        }

        internal static List<ClsCliente> GetByEmail(ref MySqlConnection conn, string email, out string errore)
        {
            List<ClsCliente> clienti = new List<ClsCliente>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(email))
            {
                errore = "Email non valida";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = SELECT_BASE + " WHERE u.email LIKE @email";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@email", "%" + email + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        clienti.Add(CreaClienteDaRiga(dt.Rows[i]));
                        i++;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return clienti;
        }

        internal static List<ClsCliente> GetByCAP(ref MySqlConnection conn, string cap, out string errore)
        {
            List<ClsCliente> clienti = new List<ClsCliente>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(cap))
            {
                errore = "CAP non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = SELECT_BASE + " WHERE c.cap=@cap";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@cap", cap);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        clienti.Add(CreaClienteDaRiga(dt.Rows[i]));
                        i++;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return clienti;
        }

        internal static List<ClsCliente> GetByNominativo(ref MySqlConnection conn, string testo, out string errore)
        {
            List<ClsCliente> clienti = new List<ClsCliente>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(testo))
            {
                errore = "Testo non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = SELECT_BASE + " WHERE u.nome LIKE @testo OR u.cognome LIKE @testo";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@testo", "%" + testo + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        clienti.Add(CreaClienteDaRiga(dt.Rows[i]));
                        i++;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return clienti;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long clienteID, ClsCliente cliente, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (clienteID <= 0)
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
                                         WHERE ID=(SELECT utenteID FROM clienti WHERE ID=@clienteID)";
                    MySqlCommand cmdU = new MySqlCommand(sqlUtente, conn);
                    cmdU.Parameters.AddWithValue("@clienteID", clienteID);
                    cmdU.Parameters.AddWithValue("@nome", cliente.Nome ?? "");
                    cmdU.Parameters.AddWithValue("@cognome", cliente.Cognome ?? "");
                    cmdU.Parameters.AddWithValue("@username", cliente.Username ?? "");
                    cmdU.Parameters.AddWithValue("@password", cliente.Password ?? "");
                    cmdU.Parameters.AddWithValue("@email", cliente.Email ?? "");
                    cmdU.Parameters.AddWithValue("@data_nascita", cliente.DataDiNascita);
                    cmdU.Parameters.AddWithValue("@genere", cliente.Sesso.ToString());
                    cmdU.Parameters.AddWithValue("@comune_nascita", cliente.ComuneDiNascita.ToString());
                    cmdU.ExecuteNonQuery();

                    string sqlCliente = @"UPDATE clienti SET indirizzo=@indirizzo, cap=@cap WHERE ID=@clienteID";
                    MySqlCommand cmdC = new MySqlCommand(sqlCliente, conn);
                    cmdC.Parameters.AddWithValue("@clienteID", clienteID);
                    cmdC.Parameters.AddWithValue("@indirizzo", cliente.Indirizzo ?? "");
                    cmdC.Parameters.AddWithValue("@cap", cliente.CAP ?? "");
                    esito = cmdC.ExecuteNonQuery();

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
        internal static long Delete(ref MySqlConnection conn, long clienteID, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (clienteID <= 0)
            {
                errore = "ID non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string queryID = "SELECT utenteID FROM clienti WHERE ID=@id";
                    MySqlCommand cmdSel = new MySqlCommand(queryID, conn);
                    cmdSel.Parameters.AddWithValue("@id", clienteID);
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

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM clienti", conn);
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