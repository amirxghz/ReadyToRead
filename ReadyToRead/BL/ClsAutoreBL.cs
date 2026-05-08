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
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsAutore autore, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO autori (nome, cognome, èVerificato, username, password, email, dataDiNascita, codiceFiscale, comuneDiNascita, sesso) 
                             VALUES (@nome, @cognome, @èVerificato, @username, @password, @email, @dataDiNascita, @codiceFiscale, @comuneDiNascita, @sesso)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", autore.Nome ?? "");
                cmd.Parameters.AddWithValue("@cognome", autore.Cognome ?? "");
                cmd.Parameters.AddWithValue("@èVerificato", autore.ÈVerificato);
                cmd.Parameters.AddWithValue("@username", autore.Username ?? "");
                cmd.Parameters.AddWithValue("@password", autore.Password ?? "");
                cmd.Parameters.AddWithValue("@email", autore.Email ?? "");
                cmd.Parameters.AddWithValue("@dataDiNascita", autore.DataDiNascita);
                cmd.Parameters.AddWithValue("@codiceFiscale", autore.CodiceFiscale ?? "");
                cmd.Parameters.AddWithValue("@comuneDiNascita", autore.ComuneDiNascita);
                cmd.Parameters.AddWithValue("@sesso", autore.Sesso);

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
        internal static List<ClsAutore> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsAutore> autori = new List<ClsAutore>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                string query = "SELECT * FROM autori";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsAutore autore = new ClsAutore();
                    autore.Nome = dt.Rows[i]["nome"].ToString();
                    autore.Cognome = dt.Rows[i]["cognome"].ToString();
                    autore.ÈVerificato = (bool)dt.Rows[i]["èVerificato"];
                    autore.Username = dt.Rows[i]["username"].ToString();
                    autore.Password = dt.Rows[i]["password"].ToString();
                    autore.Email = dt.Rows[i]["email"].ToString();
                    autori.Add(autore);
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
            DataTable dt = null;
            List<ClsAutore> autori = new List<ClsAutore>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(cognome))
                errore = "Cognome non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    string query = "SELECT * FROM autori WHERE cognome LIKE @cognome";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@cognome", "%" + cognome + "%");

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsAutore autore = new ClsAutore();
                        autore.Nome = dt.Rows[i]["nome"].ToString();
                        autore.Cognome = dt.Rows[i]["cognome"].ToString();
                        autore.ÈVerificato = (bool)dt.Rows[i]["èVerificato"];
                        autore.Username = dt.Rows[i]["username"].ToString();
                        autore.Password = dt.Rows[i]["password"].ToString();
                        autore.Email = dt.Rows[i]["email"].ToString();
                        autori.Add(autore);
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
            DataTable dt = null;
            List<ClsAutore> autori = new List<ClsAutore>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM autori WHERE èVerificato = 1";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsAutore autore = new ClsAutore();
                    autore.Nome = dt.Rows[i]["nome"].ToString();
                    autore.Cognome = dt.Rows[i]["cognome"].ToString();
                    autore.ÈVerificato = (bool)dt.Rows[i]["èVerificato"];
                    autore.Username = dt.Rows[i]["username"].ToString();
                    autore.Password = dt.Rows[i]["password"].ToString();
                    autore.Email = dt.Rows[i]["email"].ToString();
                    autori.Add(autore);
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
        internal static long Update(ref MySqlConnection conn, long ID, ClsAutore autore, out string errore)
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
                    string sql = @"UPDATE autori SET nome=@nome, cognome=@cognome, èVerificato=@èVerificato, 
                                 username=@username, password=@password, email=@email, dataDiNascita=@dataDiNascita, 
                                 codiceFiscale=@codiceFiscale, comuneDiNascita=@comuneDiNascita, sesso=@sesso 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@nome", autore.Nome ?? "");
                    cmd.Parameters.AddWithValue("@cognome", autore.Cognome ?? "");
                    cmd.Parameters.AddWithValue("@èVerificato", autore.ÈVerificato);
                    cmd.Parameters.AddWithValue("@username", autore.Username ?? "");
                    cmd.Parameters.AddWithValue("@password", autore.Password ?? "");
                    cmd.Parameters.AddWithValue("@email", autore.Email ?? "");
                    cmd.Parameters.AddWithValue("@dataDiNascita", autore.DataDiNascita);
                    cmd.Parameters.AddWithValue("@codiceFiscale", autore.CodiceFiscale ?? "");
                    cmd.Parameters.AddWithValue("@comuneDiNascita", autore.ComuneDiNascita);
                    cmd.Parameters.AddWithValue("@sesso", autore.Sesso);

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
                    string sql = "DELETE FROM autori WHERE ID=@ID";

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
                string query = "SELECT COUNT(*) FROM autori";

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