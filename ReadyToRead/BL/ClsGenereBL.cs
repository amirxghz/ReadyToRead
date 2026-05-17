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
    internal static class ClsGenereBL //Urbani
    {
        private static ClsGenere CreaGenereDaRiga(DataRow r)
        {
            ClsGenere g = new ClsGenere();
            g.ID = Convert.ToInt64(r["ID"]);
            g.Nome = r["nome"] == DBNull.Value ? "" : r["nome"].ToString();
            g.Descrizione = r["descrizione"] == DBNull.Value ? "" : r["descrizione"].ToString();
            g.Target = r["target"] == DBNull.Value ? "" : r["target"].ToString();

            string tipStr = r["tipologia"] == DBNull.Value ? "" : r["tipologia"].ToString();
            if (!string.IsNullOrEmpty(tipStr))
            {
                ClsGenere.eTIPO_GENERE t;
                if (Enum.TryParse(tipStr, true, out t))
                    g.Tipologia = t;
            }
            return g;
        }

        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsGenere genere, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO generi (nome, descrizione, target, tipologia) 
                             VALUES (@nome, @descrizione, @target, @tipologia)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", genere.Nome ?? "");
                cmd.Parameters.AddWithValue("@descrizione", genere.Descrizione ?? "");
                cmd.Parameters.AddWithValue("@target", genere.Target ?? "");
                cmd.Parameters.AddWithValue("@tipologia", genere.Tipologia.ToString());

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
            List<ClsGenere> generi = new List<ClsGenere>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM generi", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                    generi.Add(CreaGenereDaRiga(dt.Rows[i]));

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
            List<ClsGenere> generi = new List<ClsGenere>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(nome))
            {
                errore = "Nome non valido";
                return generi;
            }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM generi WHERE nome LIKE @nome", conn);
                da.SelectCommand.Parameters.AddWithValue("@nome", "%" + nome + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                    generi.Add(CreaGenereDaRiga(dt.Rows[i]));

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
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
            {
                errore = "ID non valido";
                return esito;
            }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"UPDATE generi SET nome=@nome, descrizione=@descrizione, 
                             target=@target, tipologia=@tipologia 
                             WHERE ID=@ID";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@nome", genere.Nome ?? "");
                cmd.Parameters.AddWithValue("@descrizione", genere.Descrizione ?? "");
                cmd.Parameters.AddWithValue("@target", genere.Target ?? "");
                cmd.Parameters.AddWithValue("@tipologia", genere.Tipologia.ToString());

                esito = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
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
                return esito;
            }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM generi WHERE ID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", ID);
                esito = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
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

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM generi", conn);
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
