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
    internal static class ClsUtenteBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsUtente utente, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO utenti (username, password, nome, cognome, email, dataDiNascita, codiceFiscale, comuneDiNascita, sesso) 
                             VALUES (@username, @password, @nome, @cognome, @email, @dataDiNascita, @codiceFiscale, @comuneDiNascita, @sesso)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", utente.Username ?? "");
                cmd.Parameters.AddWithValue("@password", utente.Password ?? "");
                cmd.Parameters.AddWithValue("@nome", utente.Nome ?? "");
                cmd.Parameters.AddWithValue("@cognome", utente.Cognome ?? "");
                cmd.Parameters.AddWithValue("@email", utente.Email ?? "");
                cmd.Parameters.AddWithValue("@dataDiNascita", utente.DataDiNascita);
                cmd.Parameters.AddWithValue("@codiceFiscale", utente.CodiceFiscale ?? "");
                cmd.Parameters.AddWithValue("@comuneDiNascita", utente.ComuneDiNascita);
                cmd.Parameters.AddWithValue("@sesso", utente.Sesso);

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
        
        internal static List<ClsUtente> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsUtente> utenti = new List<ClsUtente>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM utenti";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsUtente utente = new ClsUtente();
                    utente.Username = dt.Rows[i]["username"].ToString();
                    utente.Password = dt.Rows[i]["password"].ToString();
                    utente.Nome = dt.Rows[i]["nome"].ToString();
                    utente.Cognome = dt.Rows[i]["cognome"].ToString();
                    utente.Email = dt.Rows[i]["email"].ToString();
                    utente.DataDiNascita = (DateTime)dt.Rows[i]["dataDiNascita"];
                    utente.CodiceFiscale = dt.Rows[i]["codiceFiscale"].ToString();
                    utenti.Add(utente);
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
            DataTable dt = null;
            List<ClsUtente> utenti = new List<ClsUtente>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM utenti WHERE username LIKE @username";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@username", "%" + username + "%");

                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsUtente utente = new ClsUtente();
                    utente.Username = dt.Rows[i]["username"].ToString();
                    utente.Password = dt.Rows[i]["password"].ToString();
                    utente.Nome = dt.Rows[i]["nome"].ToString();
                    utente.Cognome = dt.Rows[i]["cognome"].ToString();
                    utente.Email = dt.Rows[i]["email"].ToString();
                    utente.DataDiNascita = (DateTime)dt.Rows[i]["dataDiNascita"];
                    utente.CodiceFiscale = dt.Rows[i]["codiceFiscale"].ToString();
                    utenti.Add(utente);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
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
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string sql = @"UPDATE utenti SET username=@username, password=@password, nome=@nome, 
                                 cognome=@cognome, email=@email, dataDiNascita=@dataDiNascita, 
                                 codiceFiscale=@codiceFiscale, comuneDiNascita=@comuneDiNascita, sesso=@sesso 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@username", utente.Username ?? "");
                    cmd.Parameters.AddWithValue("@password", utente.Password ?? "");
                    cmd.Parameters.AddWithValue("@nome", utente.Nome ?? "");
                    cmd.Parameters.AddWithValue("@cognome", utente.Cognome ?? "");
                    cmd.Parameters.AddWithValue("@email", utente.Email ?? "");
                    cmd.Parameters.AddWithValue("@dataDiNascita", utente.DataDiNascita);
                    cmd.Parameters.AddWithValue("@codiceFiscale", utente.CodiceFiscale ?? "");
                    cmd.Parameters.AddWithValue("@comuneDiNascita", utente.ComuneDiNascita);
                    cmd.Parameters.AddWithValue("@sesso", utente.Sesso);

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

                    string sql = "DELETE FROM utenti WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM utenti";

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

