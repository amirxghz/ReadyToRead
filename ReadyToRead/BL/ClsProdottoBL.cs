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
    internal static class ClsProdottoBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsProdotto prodotto, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO prodotti (nome, stato_disponibilita, prezzo, descrizione) 
                             VALUES (@nome, @stato_disponibilita, @prezzo, @descrizione)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", prodotto.Nome ?? "");
                cmd.Parameters.AddWithValue("@stato_disponibilita", prodotto.Disponibilita.ToString() ?? "");
                cmd.Parameters.AddWithValue("@prezzo", prodotto.Prezzo);
                cmd.Parameters.AddWithValue("@descrizione", prodotto.Descrizione ?? "");

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
        internal static List<ClsProdotto> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsProdotto> prodotti = new List<ClsProdotto>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM prodotti";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsProdotto prodotto = new ClsProdotto();
                    prodotto.ProdottoID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    prodotto.Nome = dt.Rows[i]["nome"].ToString();
                    prodotto.Disponibilita = (ClsProdotto.eStatoDisponibilita)dt.Rows[i]["stato_disponibilita"];
                    prodotto.Prezzo = Convert.ToSingle(dt.Rows[i]["prezzo"]);
                    prodotto.Descrizione = dt.Rows[i]["descrizione"].ToString();
                    prodotti.Add(prodotto);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return prodotti;
        }

        internal static ClsProdotto GetByID(ref MySqlConnection conn, long ID, out string errore)
        {
            DataTable dt = null;
            ClsProdotto prodotto = null;
            errore = string.Empty;

            if (ID <= 0)
                errore = "ID non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM prodotti WHERE ID=@ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ID", ID);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        prodotto = new ClsProdotto();
                        prodotto.ProdottoID = Convert.ToInt32(dt.Rows[0]["ID"]);
                        prodotto.Nome = dt.Rows[0]["nome"].ToString();
                        prodotto.Disponibilita = (ClsProdotto.eStatoDisponibilita)dt.Rows[0]["stato_disponibilita"];
                        prodotto.Prezzo = Convert.ToSingle(dt.Rows[0]["prezzo"]);
                        prodotto.Descrizione = dt.Rows[0]["descrizione"].ToString();
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return prodotto;
        }

        internal static List<ClsProdotto> GetByNome(ref MySqlConnection conn, string nome, out string errore)
        {
            DataTable dt = null;
            List<ClsProdotto> prodotti = new List<ClsProdotto>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(nome))
                errore = "Nome non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM prodotti WHERE nome LIKE @nome";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@nome", "%" + nome + "%");

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsProdotto prodotto = new ClsProdotto();
                        prodotto.ProdottoID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        prodotto.Nome = dt.Rows[i]["nome"].ToString();
                        prodotto.Disponibilita = (ClsProdotto.eStatoDisponibilita)dt.Rows[i]["stato_disponibilita"];
                        prodotto.Prezzo = Convert.ToSingle(dt.Rows[i]["prezzo"]);
                        prodotto.Descrizione = dt.Rows[i]["descrizione"].ToString();
                        prodotti.Add(prodotto);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return prodotti;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsProdotto prodotto, out string errore)
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

                    string sql = @"UPDATE prodotti SET nome=@nome, stato_disponibilita=@stato_disponibilita, 
                                 prezzo=@prezzo, descrizione=@descrizione 
                                 WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@nome", prodotto.Nome ?? "");
                    cmd.Parameters.AddWithValue("@stato_disponibilita", prodotto.Disponibilita.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@prezzo", prodotto.Prezzo);
                    cmd.Parameters.AddWithValue("@descrizione", prodotto.Descrizione ?? "");

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

                    string sql = "DELETE FROM prodotti WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM prodotti";

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