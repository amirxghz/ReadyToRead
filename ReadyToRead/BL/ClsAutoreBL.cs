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
    internal static class ClsAutoreBL
    {
        private const string SELECT_BASE =
            @"SELECT a.ID, a.verificato, a.nome_arte, a.data_morte, a.utenteID,
                     u.nome, u.cognome, u.username, u.password, u.email,
                     u.data_nascita, u.genere, u.comune_nascita
              FROM autori a
              INNER JOIN utenti u ON a.utenteID = u.ID";

        private static ClsAutore CreaAutoreDaRiga(DataRow r)
        {
            ClsAutore a = new ClsAutore();
            a.ID = Convert.ToInt64(r["ID"]);
            a.UtenteID = Convert.ToInt64(r["utenteID"]);
            a.ÈVerificato = Convert.ToBoolean(r["verificato"]);
            a.NomeArte = r["nome_arte"] == DBNull.Value ? "" : r["nome_arte"].ToString();
            if (r["data_morte"] != DBNull.Value)
                a.DataMorte = Convert.ToDateTime(r["data_morte"]);
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
        internal static long Create(ref MySqlConnection conn, ClsAutore autore, out string errore)
        {
            long autoreID = 0;
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sqlUtente = @"INSERT INTO utenti (nome, cognome, username, password, email, data_nascita, genere, comune_nascita)
                                     VALUES (@nome, @cognome, @username, @password, @email, @data_nascita, @genere, @comune_nascita)";
                MySqlCommand cmdU = new MySqlCommand(sqlUtente, conn);
                cmdU.Parameters.AddWithValue("@nome", autore.Nome ?? "");
                cmdU.Parameters.AddWithValue("@cognome", autore.Cognome ?? "");
                cmdU.Parameters.AddWithValue("@username", autore.Username ?? "");
                cmdU.Parameters.AddWithValue("@password", autore.Password ?? "");
                cmdU.Parameters.AddWithValue("@email", autore.Email ?? "");
                cmdU.Parameters.AddWithValue("@data_nascita", autore.DataDiNascita);
                cmdU.Parameters.AddWithValue("@genere", autore.Sesso.ToString());
                cmdU.Parameters.AddWithValue("@comune_nascita", autore.ComuneDiNascita.ToString());
                cmdU.ExecuteNonQuery();
                long utenteID = cmdU.LastInsertedId;

                string sqlAutore = @"INSERT INTO autori (verificato, nome_arte, utenteID)
                                     VALUES (@verificato, @nome_arte, @utenteID)";
                MySqlCommand cmdA = new MySqlCommand(sqlAutore, conn);
                cmdA.Parameters.AddWithValue("@verificato", autore.ÈVerificato);
                cmdA.Parameters.AddWithValue("@nome_arte", autore.NomeArte ?? "");
                cmdA.Parameters.AddWithValue("@utenteID", utenteID);
                cmdA.ExecuteNonQuery();
                autoreID = cmdA.LastInsertedId;

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return autoreID;
        }
        #endregion

        #region READ
        internal static List<ClsAutore> GetAll(ref MySqlConnection conn, out string errore)
        {
            List<ClsAutore> autori = new List<ClsAutore>();
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
                    autori.Add(CreaAutoreDaRiga(dt.Rows[i]));
                    i++;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return autori;
        }

        internal static List<ClsAutore> GetByCognome(ref MySqlConnection conn, string cognome, out string errore)
        {
            List<ClsAutore> autori = new List<ClsAutore>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(cognome))
            {
                errore = "Cognome non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = SELECT_BASE + " WHERE u.cognome LIKE @cognome";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@cognome", "%" + cognome + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        autori.Add(CreaAutoreDaRiga(dt.Rows[i]));
                        i++;
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return autori;
        }

        internal static List<ClsAutore> GetVerificati(ref MySqlConnection conn, out string errore)
        {
            List<ClsAutore> autori = new List<ClsAutore>();
            errore = string.Empty;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string query = SELECT_BASE + " WHERE a.verificato = 1";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int i = 0;
                while (i < dt.Rows.Count)
                {
                    autori.Add(CreaAutoreDaRiga(dt.Rows[i]));
                    i++;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return autori;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long autoreID, ClsAutore autore, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (autoreID <= 0)
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
                                         WHERE ID=(SELECT utenteID FROM autori WHERE ID=@autoreID)";
                    MySqlCommand cmdU = new MySqlCommand(sqlUtente, conn);
                    cmdU.Parameters.AddWithValue("@autoreID", autoreID);
                    cmdU.Parameters.AddWithValue("@nome", autore.Nome ?? "");
                    cmdU.Parameters.AddWithValue("@cognome", autore.Cognome ?? "");
                    cmdU.Parameters.AddWithValue("@username", autore.Username ?? "");
                    cmdU.Parameters.AddWithValue("@password", autore.Password ?? "");
                    cmdU.Parameters.AddWithValue("@email", autore.Email ?? "");
                    cmdU.Parameters.AddWithValue("@data_nascita", autore.DataDiNascita);
                    cmdU.Parameters.AddWithValue("@genere", autore.Sesso.ToString());
                    cmdU.Parameters.AddWithValue("@comune_nascita", autore.ComuneDiNascita.ToString());
                    cmdU.ExecuteNonQuery();

                    string sqlAutore = @"UPDATE autori SET verificato=@verificato, nome_arte=@nome_arte
                                         WHERE ID=@autoreID";
                    MySqlCommand cmdA = new MySqlCommand(sqlAutore, conn);
                    cmdA.Parameters.AddWithValue("@autoreID", autoreID);
                    cmdA.Parameters.AddWithValue("@verificato", autore.ÈVerificato);
                    cmdA.Parameters.AddWithValue("@nome_arte", autore.NomeArte ?? "");
                    esito = cmdA.ExecuteNonQuery();

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
        internal static long Delete(ref MySqlConnection conn, long autoreID, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (autoreID <= 0)
            {
                errore = "ID non valido";
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string queryID = "SELECT utenteID FROM autori WHERE ID=@id";
                    MySqlCommand cmdSel = new MySqlCommand(queryID, conn);
                    cmdSel.Parameters.AddWithValue("@id", autoreID);
                    object res = cmdSel.ExecuteScalar();

                    if (res != null)
                    {
                        long utenteID = Convert.ToInt64(res);
                        string sqlDel = "DELETE FROM utenti WHERE ID=@utenteID";
                        MySqlCommand cmdDel = new MySqlCommand(sqlDel, conn);
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

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM autori", conn);
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