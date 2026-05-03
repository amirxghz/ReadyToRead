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
    internal static class ClsClienteBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsCliente cliente, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                conn.Open();

                string sql = @"INSERT INTO clienti (email, password, indirizzo, cap, username, nome, cognome, dataDiNascita, codiceFiscale, comuneDiNascita, sesso) 
                             VALUES (@email, @password, @indirizzo, @cap, @username, @nome, @cognome, @dataDiNascita, @codiceFiscale, @comuneDiNascita, @sesso)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@email", cliente.Email ?? "");
                cmd.Parameters.AddWithValue("@password", cliente.Password ?? "");
                cmd.Parameters.AddWithValue("@indirizzo", cliente.Indirizzo ?? "");
                cmd.Parameters.AddWithValue("@cap", cliente.CAP ?? "");
                cmd.Parameters.AddWithValue("@username", cliente.Username ?? "");
                cmd.Parameters.AddWithValue("@nome", cliente.Nome ?? "");
                cmd.Parameters.AddWithValue("@cognome", cliente.Cognome ?? "");
                cmd.Parameters.AddWithValue("@dataDiNascita", cliente.DataDiNascita);
                cmd.Parameters.AddWithValue("@codiceFiscale", cliente.CodiceFiscale ?? "");
                cmd.Parameters.AddWithValue("@comuneDiNascita", cliente.ComuneDiNascita);
                cmd.Parameters.AddWithValue("@sesso", cliente.Sesso);

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
        
        internal static List<ClsCliente> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsCliente> clienti = new List<ClsCliente>();
            errore = string.Empty;

            try
            {
                conn.Open();

                string query = "SELECT * FROM clienti";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsCliente cliente = new ClsCliente();
                    cliente.Email = dt.Rows[i]["email"].ToString();
                    cliente.Password = dt.Rows[i]["password"].ToString();
                    cliente.Indirizzo = dt.Rows[i]["indirizzo"].ToString();
                    cliente.CAP = dt.Rows[i]["cap"].ToString();
                    cliente.Username = dt.Rows[i]["username"].ToString();
                    cliente.Nome = dt.Rows[i]["nome"].ToString();
                    cliente.Cognome = dt.Rows[i]["cognome"].ToString();
                    clienti.Add(cliente);
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
            DataTable dt = null;
            List<ClsCliente> clienti = new List<ClsCliente>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(email))
                errore = "Email non valida";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM clienti WHERE email LIKE @email";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@email", "%" + email + "%");

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsCliente cliente = new ClsCliente();
                        cliente.Email = dt.Rows[i]["email"].ToString();
                        cliente.Password = dt.Rows[i]["password"].ToString();
                        cliente.Indirizzo = dt.Rows[i]["indirizzo"].ToString();
                        cliente.CAP = dt.Rows[i]["cap"].ToString();
                        cliente.Username = dt.Rows[i]["username"].ToString();
                        cliente.Nome = dt.Rows[i]["nome"].ToString();
                        cliente.Cognome = dt.Rows[i]["cognome"].ToString();
                        clienti.Add(cliente);
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
            DataTable dt = null;
            List<ClsCliente> clienti = new List<ClsCliente>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(cap))
                errore = "CAP non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM clienti WHERE cap=@cap";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@cap", cap);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsCliente cliente = new ClsCliente();
                        cliente.Email = dt.Rows[i]["email"].ToString();
                        cliente.Password = dt.Rows[i]["password"].ToString();
                        cliente.Indirizzo = dt.Rows[i]["indirizzo"].ToString();
                        cliente.CAP = dt.Rows[i]["cap"].ToString();
                        cliente.Username = dt.Rows[i]["username"].ToString();
                        cliente.Nome = dt.Rows[i]["nome"].ToString();
                        cliente.Cognome = dt.Rows[i]["cognome"].ToString();
                        clienti.Add(cliente);
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
        internal static long Update(ref MySqlConnection conn, long ID, ClsCliente cliente, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    conn.Open();

                    string sql = @"UPDATE cliente SET email=@email, password=@password, indirizzo=@indirizzo, 
                                 cap=@cap, username=@username, nome=@nome, cognome=@cognome, dataDiNascita=@dataDiNascita, 
                                 codiceFiscale=@codiceFiscale, comuneDiNascita=@comuneDiNascita, sesso=@sesso 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@email", cliente.Email ?? "");
                    cmd.Parameters.AddWithValue("@password", cliente.Password ?? "");
                    cmd.Parameters.AddWithValue("@indirizzo", cliente.Indirizzo ?? "");
                    cmd.Parameters.AddWithValue("@cap", cliente.CAP ?? "");
                    cmd.Parameters.AddWithValue("@username", cliente.Username ?? "");
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome ?? "");
                    cmd.Parameters.AddWithValue("@cognome", cliente.Cognome ?? "");
                    cmd.Parameters.AddWithValue("@dataDiNascita", cliente.DataDiNascita);
                    cmd.Parameters.AddWithValue("@codiceFiscale", cliente.CodiceFiscale ?? "");
                    cmd.Parameters.AddWithValue("@comuneDiNascita", cliente.ComuneDiNascita);
                    cmd.Parameters.AddWithValue("@sesso", cliente.Sesso);

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
                    conn.Open();

                    string sql = "DELETE FROM clienti WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM clienti";

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