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
    internal static class ClsLibroBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsLibro libro, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                conn.Open();

                string sql = @"INSERT INTO libri (isbn, annoPubblicazione, numeroPagine, sinossi, edizione, imgCopertina, nome, prezzo, descrizione, lingua) 
                             VALUES (@isbn, @annoPubblicazione, @numeroPagine, @sinossi, @edizione, @imgCopertina, @nome, @prezzo, @descrizione, @lingua)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@isbn", libro.Isbn ?? "");
                cmd.Parameters.AddWithValue("@annoPubblicazione", libro.AnnoPubblicazione);
                cmd.Parameters.AddWithValue("@numeroPagine", libro.NumeroPagine);
                cmd.Parameters.AddWithValue("@sinossi", libro.Sinossi ?? "");
                cmd.Parameters.AddWithValue("@edizione", libro.Edizione ?? "");
                cmd.Parameters.AddWithValue("@imgCopertina", libro.ImgCopertina ?? "");
                cmd.Parameters.AddWithValue("@nome", libro.Nome ?? "");
                cmd.Parameters.AddWithValue("@prezzo", libro.Prezzo);
                cmd.Parameters.AddWithValue("@descrizione", libro.Descrizione ?? "");
                cmd.Parameters.AddWithValue("@lingua", string.Join(",", libro.Lingua ?? new List<string>()));

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
        internal static List<ClsLibro> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            try
            {
                conn.Open();

                string query = "SELECT * FROM libri";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsLibro libro = new ClsLibro();
                    libro.ProdottoID = (int)dt.Rows[i]["prodottoID"];
                    libro.Isbn = dt.Rows[i]["isbn"].ToString();
                    libro.AnnoPubblicazione = (DateTime)dt.Rows[i]["annoPubblicazione"];
                    libro.NumeroPagine = (int)dt.Rows[i]["numeroPagine"];
                    libro.Sinossi = dt.Rows[i]["sinossi"].ToString();
                    libro.Edizione = dt.Rows[i]["edizione"].ToString();
                    libro.ImgCopertina = dt.Rows[i]["imgCopertina"].ToString();
                    libri.Add(libro);
                }

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
            DataTable dt = null;
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            if (minPrezzo < 0 || maxPrezzo < 0)
                errore = "Prezzo non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM libri WHERE prezzo BETWEEN @minPrezzo AND @maxPrezzo";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@minPrezzo", minPrezzo);
                    da.SelectCommand.Parameters.AddWithValue("@maxPrezzo", maxPrezzo);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsLibro libro = new ClsLibro();
                        libro.ProdottoID = (int)dt.Rows[i]["prodottoID"];
                        libro.Isbn = dt.Rows[i]["isbn"].ToString();
                        libro.AnnoPubblicazione = (DateTime)dt.Rows[i]["annoPubblicazione"];
                        libro.NumeroPagine = (int)dt.Rows[i]["numeroPagine"];
                        libro.Sinossi = dt.Rows[i]["sinossi"].ToString();
                        libro.Edizione = dt.Rows[i]["edizione"].ToString();
                        libro.ImgCopertina = dt.Rows[i]["imgCopertina"].ToString();
                        libri.Add(libro);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return libri;
        }

        internal static List<ClsLibro> GetByISBN(ref MySqlConnection conn, string isbn, out string errore)
        {
            DataTable dt = null;
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(isbn))
                errore = "ISBN non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM libri WHERE isbn=@isbn";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@isbn", isbn);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsLibro libro = new ClsLibro();
                        libro.ProdottoID = (int)dt.Rows[i]["prodottoID"];
                        libro.Isbn = dt.Rows[i]["isbn"].ToString();
                        libro.AnnoPubblicazione = (DateTime)dt.Rows[i]["annoPubblicazione"];
                        libro.NumeroPagine = (int)dt.Rows[i]["numeroPagine"];
                        libro.Sinossi = dt.Rows[i]["sinossi"].ToString();
                        libro.Edizione = dt.Rows[i]["edizione"].ToString();
                        libro.ImgCopertina = dt.Rows[i]["imgCopertina"].ToString();
                        libri.Add(libro);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return libri;
        }
        
        internal static List<ClsLibro> GetByNome(ref MySqlConnection conn, string nome, out string errore)
        {
            DataTable dt = null;
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            if (string.IsNullOrEmpty(nome))
                errore = "Nome non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM libri WHERE nome LIKE @nome";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@nome", "%" + nome + "%");

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsLibro libro = new ClsLibro();
                        libro.ProdottoID = (int)dt.Rows[i]["prodottoID"];
                        libro.Isbn = dt.Rows[i]["isbn"].ToString();
                        libro.AnnoPubblicazione = (DateTime)dt.Rows[i]["annoPubblicazione"];
                        libro.NumeroPagine = (int)dt.Rows[i]["numeroPagine"];
                        libro.Sinossi = dt.Rows[i]["sinossi"].ToString();
                        libro.Edizione = dt.Rows[i]["edizione"].ToString();
                        libro.ImgCopertina = dt.Rows[i]["imgCopertina"].ToString();
                        libri.Add(libro);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return libri;
        }
        
        internal static List<ClsLibro> GetByAnnoPubblicazione(ref MySqlConnection conn, int anno, out string errore)
        {
            DataTable dt = null;
            List<ClsLibro> libri = new List<ClsLibro>();
            errore = string.Empty;

            if (anno < 1900)
                errore = "Anno non valido";
            else
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM libri WHERE YEAR(annoPubblicazione)=@anno";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@anno", anno);

                    dt = new DataTable();
                    da.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClsLibro libro = new ClsLibro();
                        libro.ProdottoID = (int)dt.Rows[i]["prodottoID"];
                        libro.Isbn = dt.Rows[i]["isbn"].ToString();
                        libro.AnnoPubblicazione = (DateTime)dt.Rows[i]["annoPubblicazione"];
                        libro.NumeroPagine = (int)dt.Rows[i]["numeroPagine"];
                        libro.Sinossi = dt.Rows[i]["sinossi"].ToString();
                        libro.Edizione = dt.Rows[i]["edizione"].ToString();
                        libro.ImgCopertina = dt.Rows[i]["imgCopertina"].ToString();
                        libri.Add(libro);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errore = ex.Message;
                }
            }

            return libri;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsLibro libro, out string errore)
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

                    string sql = @"UPDATE libri SET isbn=@isbn, annoPubblicazione=@annoPubblicazione, numeroPagine=@numeroPagine, 
                                 sinossi=@sinossi, edizione=@edizione, imgCopertina=@imgCopertina, nome=@nome, prezzo=@prezzo, 
                                 descrizione=@descrizione, lingua=@lingua 
                                 WHERE prodottoID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@isbn", libro.Isbn ?? "");
                    cmd.Parameters.AddWithValue("@annoPubblicazione", libro.AnnoPubblicazione);
                    cmd.Parameters.AddWithValue("@numeroPagine", libro.NumeroPagine);
                    cmd.Parameters.AddWithValue("@sinossi", libro.Sinossi ?? "");
                    cmd.Parameters.AddWithValue("@edizione", libro.Edizione ?? "");
                    cmd.Parameters.AddWithValue("@imgCopertina", libro.ImgCopertina ?? "");
                    cmd.Parameters.AddWithValue("@nome", libro.Nome ?? "");
                    cmd.Parameters.AddWithValue("@prezzo", libro.Prezzo);
                    cmd.Parameters.AddWithValue("@descrizione", libro.Descrizione ?? "");
                    cmd.Parameters.AddWithValue("@lingua", string.Join(",", libro.Lingua ?? new List<string>()));

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

                    string sql = "DELETE FROM libri WHERE prodottoID=@ID";

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

                string query = "SELECT COUNT(*) FROM libri";

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