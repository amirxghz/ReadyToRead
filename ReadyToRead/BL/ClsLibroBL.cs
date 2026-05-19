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
        private const string SELECT_BASE =
            @"SELECT l.isbn,
                     l.numero_pagine,
                     l.sinossi,
                     l.path_copertina,
                     l.path_file,
                     l.tipo,
                     l.edizione,
                     l.lingua        AS libro_lingua,
                     l.anno_pubblicazione,
                     l.prodottoID,
                     l.ID            AS libroID,
                     p.nome,
                     p.prezzo,
                     p.descrizione,
                     p.stato_disponibilita
              FROM libri l
              INNER JOIN prodotti p ON l.prodottoID = p.ID";

        private static ClsLibro CreaLibroDaRiga(DataRow r)
        {
            ClsLibro l = new ClsLibro();
            l.Isbn        = r["isbn"] == DBNull.Value ? "" : r["isbn"].ToString();
            l.ProdottoID  = r.Table.Columns.Contains("libroID")
                            ? Convert.ToInt32(r["libroID"])
                            : Convert.ToInt32(r["prodottoID"]);
            l.NumeroPagine= r["numero_pagine"] == DBNull.Value ? 0 : Convert.ToInt32(r["numero_pagine"]);
            l.Sinossi     = r["sinossi"] == DBNull.Value ? "" : r["sinossi"].ToString();
            l.ImgCopertina= r["path_copertina"] == DBNull.Value ? "" : r["path_copertina"].ToString();
            l.EBook       = r["path_file"] == DBNull.Value ? "" : r["path_file"].ToString();
            l.Edizione    = r["edizione"] == DBNull.Value ? "" : r["edizione"].ToString();
            l.Lingua      = r["libro_lingua"] == DBNull.Value ? "" : r["libro_lingua"].ToString();
            if (r["anno_pubblicazione"] != DBNull.Value)
                l.AnnoPubblicazione = Convert.ToDateTime(r["anno_pubblicazione"]);

            l.Nome        = r["nome"] == DBNull.Value ? "" : r["nome"].ToString();
            l.Prezzo      = r["prezzo"] == DBNull.Value ? 0f : Convert.ToSingle(r["prezzo"]);
            l.Descrizione = r["descrizione"] == DBNull.Value ? "" : r["descrizione"].ToString();
            l.Quantita = r["quantita"] == DBNull.Value ? 0 : Convert.ToInt32(r["quantita"]);
            return l;
        }

        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsLibro libro, out string errore)
        {
            long prodottoID = 0;
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sqlProd = @"INSERT INTO prodotti (nome, stato_disponibilita, prezzo, descrizione, quantita)
                                   VALUES (@nome, 'disponibile', @prezzo, @descrizione, @quantita)";
                MySqlCommand cmdP = new MySqlCommand(sqlProd, conn);
                cmdP.Parameters.AddWithValue("@nome",        libro.Nome ?? "");
                cmdP.Parameters.AddWithValue("@prezzo",      libro.Prezzo);
                cmdP.Parameters.AddWithValue("@descrizione", libro.Descrizione ?? "");
                cmdP.Parameters.AddWithValue("@quantita", libro.Quantita);
                cmdP.ExecuteNonQuery();
                prodottoID = cmdP.LastInsertedId;


                string sqlLibro = @"INSERT INTO libri
                                    (isbn, path_copertina, numero_pagine, sinossi, path_file,
                                     tipo, edizione, lingua, anno_pubblicazione, prodottoID)
                                    VALUES
                                    (@isbn, @pathCopertina, @numeroPagine, @sinossi, @pathFile,
                                     'fisico', @edizione, @lingua, @annoPubblicazione, @prodottoID)";
                MySqlCommand cmdL = new MySqlCommand(sqlLibro, conn);
                cmdL.Parameters.AddWithValue("@isbn",             libro.Isbn ?? "");
                cmdL.Parameters.AddWithValue("@pathCopertina",    libro.ImgCopertina ?? "");
                cmdL.Parameters.AddWithValue("@numeroPagine",     libro.NumeroPagine);
                cmdL.Parameters.AddWithValue("@sinossi",          libro.Sinossi ?? "");
                cmdL.Parameters.AddWithValue("@pathFile",         libro.EBook ?? "");
                cmdL.Parameters.AddWithValue("@edizione",         libro.Edizione ?? "");
                cmdL.Parameters.AddWithValue("@lingua",           libro.Lingua ?? "");
                cmdL.Parameters.AddWithValue("@annoPubblicazione",
                    libro.AnnoPubblicazione == default(DateTime)
                        ? (object)DBNull.Value
                        : libro.AnnoPubblicazione.Date);
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

                string query =
                    @"SELECT l.isbn,
                             l.numero_pagine,
                             l.sinossi,
                             l.path_copertina,
                             l.path_file,
                             l.tipo,
                             l.edizione,
                             l.lingua        AS libro_lingua,
                             l.anno_pubblicazione,
                             l.prodottoID,
                             p.nome,
                             p.prezzo,
                             p.descrizione,
                             p.quantita,
                             p.stato_disponibilita
                      FROM libri l
                      INNER JOIN prodotti p ON l.prodottoID = p.ID
                      WHERE l.prodottoID IN (
                          SELECT MIN(prodottoID) FROM libri GROUP BY isbn
                      )";

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

            if (string.IsNullOrEmpty(nome)) { errore = "Nome non valido"; return libri; }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query =
                    @"SELECT l.isbn, l.numero_pagine, l.sinossi, l.path_copertina, l.path_file,
                             l.tipo, l.edizione, l.lingua AS libro_lingua, l.anno_pubblicazione,
                             l.prodottoID, p.nome, p.prezzo, p.descrizione, p.quantita, p.stato_disponibilita
                      FROM libri l
                      INNER JOIN prodotti p ON l.prodottoID = p.ID
                      WHERE p.nome LIKE @nome
                        AND l.prodottoID IN (SELECT MIN(prodottoID) FROM libri GROUP BY isbn)";

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

            if (string.IsNullOrEmpty(isbn)) { errore = "ISBN non valido"; return libri; }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query =
                    @"SELECT l.isbn, l.numero_pagine, l.sinossi, l.path_copertina, l.path_file,
                             l.tipo, l.edizione, l.lingua AS libro_lingua, l.anno_pubblicazione,
                             l.prodottoID, p.nome, p.prezzo, p.descrizione, p.quantita, p.stato_disponibilita
                      FROM libri l
                      INNER JOIN prodotti p ON l.prodottoID = p.ID
                      WHERE l.isbn = @isbn
                      LIMIT 1";

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

            if (minPrezzo < 0 || maxPrezzo < 0) { errore = "Prezzo non valido"; return libri; }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query =
                    @"SELECT l.isbn, l.numero_pagine, l.sinossi, l.path_copertina, l.path_file,
                             l.tipo, l.edizione, l.lingua AS libro_lingua, l.anno_pubblicazione,
                             l.prodottoID, p.nome, p.prezzo, p.descrizione, p.quantita, p.stato_disponibilita
                      FROM libri l
                      INNER JOIN prodotti p ON l.prodottoID = p.ID
                      WHERE p.prezzo BETWEEN @minPrezzo AND @maxPrezzo
                        AND l.prodottoID IN (SELECT MIN(prodottoID) FROM libri GROUP BY isbn)";

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

        internal static int CountByISBN(ref MySqlConnection conn, string isbn, out string errore)
        {
            int count = 0;
            errore = string.Empty;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM libri WHERE isbn = @isbn", conn);
                cmd.Parameters.AddWithValue("@isbn", isbn);
                object res = cmd.ExecuteScalar();
                if (res != null) count = Convert.ToInt32(res);

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return count;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long prodottoID, ClsLibro libro, out string errore)
        {
            long esito = 0;
            errore = string.Empty;

            if (prodottoID <= 0) { errore = "ID non valido"; return esito; }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string sqlP = @"UPDATE prodotti SET nome=@nome, prezzo=@prezzo, descrizione=@descrizione, quantita=@quantita
                                WHERE ID=@prodottoID";
                MySqlCommand cmdP = new MySqlCommand(sqlP, conn);
                cmdP.Parameters.AddWithValue("@prodottoID",  prodottoID);
                cmdP.Parameters.AddWithValue("@nome",        libro.Nome ?? "");
                cmdP.Parameters.AddWithValue("@prezzo",      libro.Prezzo);
                cmdP.Parameters.AddWithValue("@descrizione", libro.Descrizione ?? "");
                cmdP.Parameters.AddWithValue("@quantita", libro.Quantita);
                cmdP.ExecuteNonQuery();

                string sqlL = @"UPDATE libri SET
                                    isbn=@isbn,
                                    path_copertina=@pathCopertina,
                                    numero_pagine=@numeroPagine,
                                    sinossi=@sinossi,
                                    path_file=@pathFile,
                                    edizione=@edizione,
                                    lingua=@lingua,
                                    anno_pubblicazione=@annoPubblicazione
                                WHERE ID=@prodottoID";
                MySqlCommand cmdL = new MySqlCommand(sqlL, conn);
                cmdL.Parameters.AddWithValue("@prodottoID",       prodottoID);
                cmdL.Parameters.AddWithValue("@isbn",             libro.Isbn ?? "");
                cmdL.Parameters.AddWithValue("@pathCopertina",    libro.ImgCopertina ?? "");
                cmdL.Parameters.AddWithValue("@numeroPagine",     libro.NumeroPagine);
                cmdL.Parameters.AddWithValue("@sinossi",          libro.Sinossi ?? "");
                cmdL.Parameters.AddWithValue("@pathFile",         libro.EBook ?? "");
                cmdL.Parameters.AddWithValue("@edizione",         libro.Edizione ?? "");
                cmdL.Parameters.AddWithValue("@lingua",           libro.Lingua ?? "");
                cmdL.Parameters.AddWithValue("@annoPubblicazione",
                    libro.AnnoPubblicazione == default(DateTime)
                        ? (object)DBNull.Value
                        : libro.AnnoPubblicazione.Date);
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

            if (prodottoID <= 0) { errore = "ID non valido"; return esito; }

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                
                MySqlCommand cmdGetFK = new MySqlCommand(
                    "SELECT prodottoID FROM libri WHERE ID=@id", conn);
                cmdGetFK.Parameters.AddWithValue("@id", prodottoID);
                object fk = cmdGetFK.ExecuteScalar();

                MySqlCommand cmdL = new MySqlCommand(
                    "DELETE FROM libri WHERE ID=@id", conn);
                cmdL.Parameters.AddWithValue("@id", prodottoID);
                cmdL.ExecuteNonQuery();

                if (fk != null)
                {
                    long fkProdottoID = Convert.ToInt64(fk);
                    MySqlCommand cmdCheck = new MySqlCommand(
                        "SELECT COUNT(*) FROM libri WHERE prodottoID=@pid", conn);
                    cmdCheck.Parameters.AddWithValue("@pid", fkProdottoID);
                    int rimanenti = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (rimanenti == 0)
                    {
                        MySqlCommand cmdP = new MySqlCommand(
                            "DELETE FROM prodotti WHERE ID=@pid", conn);
                        cmdP.Parameters.AddWithValue("@pid", fkProdottoID);
                        esito = cmdP.ExecuteNonQuery();
                    }
                    else
                        esito = 1;
                }

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
                object res = cmd.ExecuteScalar();
                if (res != null) count = Convert.ToInt32(res);

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return count;
        }
        #endregion

        #region FILE 
        internal static string SalvaCopertina(string pathSorgente, string titoloLibro, out string errore)
        {
            errore = string.Empty;
            try
            {
                string ext = Path.GetExtension(pathSorgente);
                string nomeFile = "copertina_" + SanitizzaNomeFile(titoloLibro) + ext;
                string resourceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                Directory.CreateDirectory(resourceDir);
                string destPath = Path.Combine(resourceDir, nomeFile);
                File.Copy(pathSorgente, destPath, overwrite: true);
                return Path.Combine("Resources", nomeFile);
            }
            catch (Exception ex)
            {
                errore = ex.Message;
                return string.Empty;
            }
        }

        internal static string SalvaEBook(string pathSorgente, string titoloLibro, out string errore)
        {
            errore = string.Empty;
            try
            {
                string ext = Path.GetExtension(pathSorgente);
                string nomeFile = "e_book_" + SanitizzaNomeFile(titoloLibro) + ext;
                string resourceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                Directory.CreateDirectory(resourceDir);
                string destPath = Path.Combine(resourceDir, nomeFile);
                File.Copy(pathSorgente, destPath, overwrite: true);
                return Path.Combine("Resources", nomeFile);
            }
            catch (Exception ex)
            {
                errore = ex.Message;
                return string.Empty;
            }
        }

        private static string SanitizzaNomeFile(string nome)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                nome = nome.Replace(c, '_');
            return nome.Replace(' ', '_');
        }
        #endregion
    }
}
