using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadyToRead;

namespace ReadyToRead
{
    internal static class ClsLibroBL //Urbani
    {
        private static ClsLibro CreaLibroDaRiga(DataRow r)
        {
            ClsLibro l = new ClsLibro();
            l.Isbn = r["isbn"] == DBNull.Value ? "" : r["isbn"].ToString();
            l.ProdottoID = r["prodottoID"] == DBNull.Value ? 0 : Convert.ToInt32(r["prodottoID"]);
            l.NumeroPagine = r["numero_pagine"] == DBNull.Value ? 0 : Convert.ToInt32(r["numero_pagine"]);
            l.Sinossi = r["sinossi"] == DBNull.Value ? "" : r["sinossi"].ToString();
            l.ImgCopertina = r["path_copertina"] == DBNull.Value ? "" : r["path_copertina"].ToString();
            l.EBook = r["path_file"] == DBNull.Value ? "" : r["path_file"].ToString();
            l.Edizione = r["edizione"] == DBNull.Value ? "" : r["edizione"].ToString();
            l.Lingua = r["lingua"] == DBNull.Value ? "" : r["lingua"].ToString();
            if (r["anno_pubblicazione"] != DBNull.Value)
                l.AnnoPubblicazione = Convert.ToDateTime(r["anno_pubblicazione"]);
            if (r.Table.Columns.Contains("nome"))
                l.Nome = r["nome"] == DBNull.Value ? "" : r["nome"].ToString();
            if (r.Table.Columns.Contains("prezzo"))
                l.Prezzo = r["prezzo"] == DBNull.Value ? 0f : Convert.ToSingle(r["prezzo"]);
            if (r.Table.Columns.Contains("descrizione"))
                l.Descrizione = r["descrizione"] == DBNull.Value ? "" : r["descrizione"].ToString();
            return l;
        }

        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsLibro libro, out string errore)
        {
            long prodottoID = 0;
            errore = String.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sqlProd = @"INSERT INTO prodotti (nome, stato_disponibilita, prezzo, descrizione)
                                   VALUES (@nome, 'disponibile', @prezzo, @descrizione)";
                MySqlCommand cmdP = new MySqlCommand(sqlProd, conn);
                cmdP.Parameters.AddWithValue("@nome", libro.Nome ?? "");
                cmdP.Parameters.AddWithValue("@prezzo", libro.Prezzo);
                cmdP.Parameters.AddWithValue("@descrizione", libro.Descrizione ?? "");
                cmdP.ExecuteNonQuery();
                prodottoID = cmdP.LastInsertedId;

                string sqlLibro = @"INSERT INTO libri 
                                    (isbn, path_copertina, numero_pagine, sinossi, path_file, tipo,
                                     edizione, lingua, anno_pubblicazione, prodottoID)
                                    VALUES 
                                    (@isbn, @pathCopertina, @numeroPagine, @sinossi, @pathFile, @tipo,
                                     @edizione, @lingua, @annoPubblicazione, @prodottoID)";
                MySqlCommand cmdL = new MySqlCommand(sqlLibro, conn);
                cmdL.Parameters.AddWithValue("@isbn", libro.Isbn ?? "");
                cmdL.Parameters.AddWithValue("@pathCopertina", libro.ImgCopertina ?? "");
                cmdL.Parameters.AddWithValue("@numeroPagine", libro.NumeroPagine);
                cmdL.Parameters.AddWithValue("@sinossi", libro.Sinossi ?? "");
                cmdL.Parameters.AddWithValue("@pathFile", libro.EBook ?? "");
                cmdL.Parameters.AddWithValue("@tipo", "fisico");
                cmdL.Parameters.AddWithValue("@edizione", libro.Edizione ?? "");
                cmdL.Parameters.AddWithValue("@lingua", libro.Lingua ?? "");
                cmdL.Parameters.AddWithValue("@annoPubblicazione", libro.AnnoPubblicazione.Date);
                cmdL.Parameters.AddWithValue("@prodottoID", prodottoID);
                cmdL.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
                prodottoID = 0;
            }

            return prodottoID;
        }
        #endregion

        #region READ
        internal static List<ClsLibro> GetAll(ref MySqlConnection conn, out string errore)
        {
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = @"SELECT l.*, p.nome, p.prezzo, p.descrizione, p.stato_disponibilita
                                 FROM libri l
                                 INNER JOIN prodotti p ON l.prodottoID = p.ID";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                    libri.Add(CreaLibroDaRiga(dt.Rows[i]));

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return libri;
        }

        internal static List<ClsLibro> GetByNome(ref MySqlConnection conn, string nome, out string errore)
        {
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(nome))
            {
                errore = "Nome non valido";
                return libri;
            }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = @"SELECT l.*, p.nome, p.prezzo, p.descrizione, p.stato_disponibilita
                                 FROM libri l
                                 INNER JOIN prodotti p ON l.prodottoID = p.ID
                                 WHERE p.nome LIKE @nome";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@nome", "%" + nome + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                    libri.Add(CreaLibroDaRiga(dt.Rows[i]));

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return libri;
        }

        internal static List<ClsLibro> GetByISBN(ref MySqlConnection conn, string isbn, out string errore)
        {
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(isbn))
            {
                errore = "ISBN non valido";
                return libri;
            }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = @"SELECT l.*, p.nome, p.prezzo, p.descrizione, p.stato_disponibilita
                                 FROM libri l
                                 INNER JOIN prodotti p ON l.prodottoID = p.ID
                                 WHERE l.isbn = @isbn";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@isbn", isbn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                    libri.Add(CreaLibroDaRiga(dt.Rows[i]));

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return libri;
        }

        internal static List<ClsLibro> GetByPriceRange(ref MySqlConnection conn, float minPrezzo, float maxPrezzo, out string errore)
        {
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            if (minPrezzo < 0 || maxPrezzo < 0)
            {
                errore = "Prezzo non valido";
                return libri;
            }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = @"SELECT l.*, p.nome, p.prezzo, p.descrizione, p.stato_disponibilita
                                 FROM libri l
                                 INNER JOIN prodotti p ON l.prodottoID = p.ID
                                 WHERE p.prezzo BETWEEN @minPrezzo AND @maxPrezzo";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@minPrezzo", minPrezzo);
                da.SelectCommand.Parameters.AddWithValue("@maxPrezzo", maxPrezzo);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                    libri.Add(CreaLibroDaRiga(dt.Rows[i]));

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return libri;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long prodottoID, ClsLibro libro, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (prodottoID <= 0)
            {
                errore = "ID non valido";
                return esito;
            }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sqlProd = @"UPDATE prodotti SET nome=@nome, prezzo=@prezzo, descrizione=@descrizione 
                                   WHERE ID=@prodottoID";
                MySqlCommand cmdP = new MySqlCommand(sqlProd, conn);
                cmdP.Parameters.AddWithValue("@prodottoID", prodottoID);
                cmdP.Parameters.AddWithValue("@nome", libro.Nome ?? "");
                cmdP.Parameters.AddWithValue("@prezzo", libro.Prezzo);
                cmdP.Parameters.AddWithValue("@descrizione", libro.Descrizione ?? "");
                cmdP.ExecuteNonQuery();

                string sqlLibro = @"UPDATE libri SET 
                                    isbn=@isbn, path_copertina=@pathCopertina, numero_pagine=@numeroPagine,
                                    sinossi=@sinossi, path_file=@pathFile, edizione=@edizione,
                                    lingua=@lingua, anno_pubblicazione=@annoPubblicazione
                                    WHERE prodottoID=@prodottoID";
                MySqlCommand cmdL = new MySqlCommand(sqlLibro, conn);
                cmdL.Parameters.AddWithValue("@prodottoID", prodottoID);
                cmdL.Parameters.AddWithValue("@isbn", libro.Isbn ?? "");
                cmdL.Parameters.AddWithValue("@pathCopertina", libro.ImgCopertina ?? "");
                cmdL.Parameters.AddWithValue("@numeroPagine", libro.NumeroPagine);
                cmdL.Parameters.AddWithValue("@sinossi", libro.Sinossi ?? "");
                cmdL.Parameters.AddWithValue("@pathFile", libro.EBook ?? "");
                cmdL.Parameters.AddWithValue("@edizione", libro.Edizione ?? "");
                cmdL.Parameters.AddWithValue("@lingua", libro.Lingua ?? "");
                cmdL.Parameters.AddWithValue("@annoPubblicazione", libro.AnnoPubblicazione.Date);
                esito = cmdL.ExecuteNonQuery();

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
        internal static long Delete(ref MySqlConnection conn, long prodottoID, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (prodottoID <= 0)
            {
                errore = "ID non valido";
                return esito;
            }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM prodotti WHERE ID=@prodottoID", conn);
                cmd.Parameters.AddWithValue("@prodottoID", prodottoID);
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

                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM libri", conn);
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

        #region FILE COPERTINA ED EBOOOK
        internal static string SalvaCopertina(string pathSorgente, string titoloLibro, out string errore)
        {
            errore = string.Empty;
            try
            {
                string estenzioneFile = Path.GetExtension(pathSorgente);
                string nomeFile = "copertina_" + NomeFile(titoloLibro) + estenzioneFile;
                string resourceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                Directory.CreateDirectory(resourceDir);
                string destPath = Path.Combine(resourceDir, nomeFile);
                File.Copy(pathSorgente, destPath, overwrite: true);
                return Path.Combine("Resources", nomeFile);
            }
            catch (Exception ex)
            {
                errore = ex.Message;
                return Path.Combine("Resources", "libroCopertina_DEFAULT");
            }
        }
        internal static string SalvaEBook(string pathSorgente, string titoloLibro, out string errore)
        {
            errore = string.Empty;
            try
            {
                string estenzioneFile = Path.GetExtension(pathSorgente);
                string nomeFile = "e_book_" + NomeFile(titoloLibro) + estenzioneFile;
                string resourceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                Directory.CreateDirectory(resourceDir);
                string destPath = Path.Combine(resourceDir, nomeFile);
                File.Copy(pathSorgente, destPath, overwrite: true);
                return Path.Combine("Resources", nomeFile);
            }
            catch (Exception ex)
            {
                errore = ex.Message;
                return Path.Combine("Resources", "e_book_DEFAULT");
            }
        }

        private static string NomeFile(string nome)
        {
            return nome.Replace(' ', '_');
        }
        #endregion
    }
}
