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
    internal static class ClsGenereBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsGenere genere, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                conn.Open();

                string sql = @"INSERT INTO generi (nome, descrizione, target, tipologia) 
                             VALUES (@nome, @descrizione, @target, @tipologia)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", genere.Nome ?? "");
                cmd.Parameters.AddWithValue("@descrizione", genere.Descrizione ?? "");
                cmd.Parameters.AddWithValue("@target", genere.Target ?? "");
                cmd.Parameters.AddWithValue("@tipologia", genere.Tipologia.ToString() ?? "");

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
        internal static List<ClsGenere> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsGenere> generi = new List<ClsGenere>();
            errore = string.Empty;

            try
            {
                conn.Open();

                string query = "SELECT * FROM generi";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsGenere genere = new ClsGenere();
                    genere.Nome = dt.Rows[i]["nome"].ToString();
                    genere.Descrizione = dt.Rows[i]["descrizione"].ToString();
                    genere.Target = dt.Rows[i]["target"].ToString();
                    genere.Tipologia = (ClsGenere.eTIPO_GENERE) Convert.ToInt32((dt.Rows[i]["tipologia"]));
                    generi.Add(genere);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return generi;
        }
        internal static List<ClsGenere> GetByNome(ref MySqlConnection conn, string nome, out string errore)
        {
            DataTable dt = null;
            List<ClsGenere> generi = new List<ClsGenere>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(nome))
                errore = "Nome non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM generi WHERE nome LIKE @nome";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@nome", "%" + nome + "%");

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsGenere genere = new ClsGenere();
                        genere.Nome = dt.Rows[i]["nome"].ToString();
                        genere.Descrizione = dt.Rows[i]["descrizione"].ToString();
                        genere.Target = dt.Rows[i]["target"].ToString();
                        genere.Tipologia = (ClsGenere.eTIPO_GENERE)Convert.ToInt32((dt.Rows[i]["tipologia"]));
                        generi.Add(genere);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return generi;
        }
        
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsGenere genere, out string errore)
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

                    string sql = @"UPDATE generi SET nome=@nome, descrizione=@descrizione, 
                                 target=@target, tipologia=@tipologia 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@nome", genere.Nome ?? "");
                    cmd.Parameters.AddWithValue("@descrizione", genere.Descrizione ?? "");
                    cmd.Parameters.AddWithValue("@target", genere.Target ?? "");
                    cmd.Parameters.AddWithValue("@tipologia", genere.Tipologia.ToString() ?? "");

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

                    string sql = "DELETE FROM generi WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM generi";

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