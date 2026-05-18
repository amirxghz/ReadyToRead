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
    internal static class ClsGiftCardBL //Urbani
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsGiftCard giftCard, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sql = @"INSERT INTO giftcards (nomeDestinatario, nome, prezzo, descrizione, lingua) 
                             VALUES (@nomeDestinatario, @nome, @prezzo, @descrizione, @lingua)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nomeDestinatario", giftCard.NomeDestinatario ?? "");
                cmd.Parameters.AddWithValue("@nome", giftCard.Nome ?? "");
                cmd.Parameters.AddWithValue("@prezzo", giftCard.Prezzo);
                cmd.Parameters.AddWithValue("@descrizione", giftCard.Descrizione ?? "");
                cmd.Parameters.AddWithValue("@lingua", giftCard.Lingua ?? "");

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
        
        internal static List<ClsGiftCard> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsGiftCard> giftCards = new List<ClsGiftCard>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM giftcards";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsGiftCard giftCard = new ClsGiftCard();
                    giftCard.ProdottoID = (int)dt.Rows[i]["prodottoID"];
                    giftCard.NomeDestinatario = dt.Rows[i]["nomeDestinatario"].ToString();
                    giftCard.Nome = dt.Rows[i]["nome"].ToString();
                    giftCard.Prezzo = (float)dt.Rows[i]["prezzo"];
                    giftCard.Descrizione = dt.Rows[i]["descrizione"].ToString();
                    giftCard.Lingua = dt.Rows[i]["lingua"].ToString();
                    giftCards.Add(giftCard);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return giftCards;
        }
        
        internal static List<ClsGiftCard> GetByNomeDestinatario(ref MySqlConnection conn, string nome, out string errore)
        {
            DataTable dt = null;
            List<ClsGiftCard> giftCards = new List<ClsGiftCard>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(nome))
                errore = "Nome non valido";
            else
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM giftcards WHERE nomeDestinatario LIKE @nome";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@nome", "%" + nome + "%");

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsGiftCard giftCard = new ClsGiftCard();
                        giftCard.ProdottoID = (int)dt.Rows[i]["prodottoID"];
                        giftCard.NomeDestinatario = dt.Rows[i]["nomeDestinatario"].ToString();
                        giftCard.Nome = dt.Rows[i]["nome"].ToString();
                        giftCard.Prezzo = (float)dt.Rows[i]["prezzo"];
                        giftCard.Descrizione = dt.Rows[i]["descrizione"].ToString();
                        giftCard.Lingua = dt.Rows[i]["lingua"].ToString();
                        giftCards.Add(giftCard);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return giftCards;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsGiftCard giftCard, out string errore)
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

                    string sql = @"UPDATE giftcards SET nomeDestinatario=@nomeDestinatario, nome=@nome, prezzo=@prezzo, 
                                 descrizione=@descrizione, lingua=@lingua 
                                 WHERE prodottoID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@nomeDestinatario", giftCard.NomeDestinatario ?? "");
                    cmd.Parameters.AddWithValue("@nome", giftCard.Nome ?? "");
                    cmd.Parameters.AddWithValue("@prezzo", giftCard.Prezzo);
                    cmd.Parameters.AddWithValue("@descrizione", giftCard.Descrizione ?? "");
                    cmd.Parameters.AddWithValue("@lingua", giftCard.Lingua ?? "");

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

                    string sql = "DELETE FROM giftcards WHERE prodottoID=@ID";

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

                string query = "SELECT COUNT(*) FROM giftcards";

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