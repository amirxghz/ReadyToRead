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
    internal static class ClsCasaBL
    {
        #region CREATE
        internal static long Create(ref MySqlConnection conn, ClsCasa casa, out string errore)
        {
            long ID = 0;
            errore = String.Empty;

            try
            {
                conn.Open();

                string sql = @"INSERT INTO houses (ragioneSociale, indirizzoSedeLegaleID, indirizzoSedeOperativaID, tipoAzienda, esclusiva, tipologia) 
                             VALUES (@ragioneSociale, @indirizzoSedeLegaleID, @indirizzoSedeOperativaID, @tipoAzienda, @esclusiva, @tipologia)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
     

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ragioneSociale", casa.RagioneSociale.ToString() ?? "");
                cmd.Parameters.AddWithValue("@indirizzoSedeLegaleID", casa.IndirizzoSedeLegaleID.ToString() ?? "");
                cmd.Parameters.AddWithValue("@indirizzoSedeOperativaID", casa.IndirizzoSedeOperativaID.ToString() ?? "");
                cmd.Parameters.AddWithValue("@tipoAzienda", casa.TipoAzienda.ToString() ?? "");
                cmd.Parameters.AddWithValue("@esclusiva", casa.Esclusiva.ToString() ?? "");
                cmd.Parameters.AddWithValue("@tipologia", casa.Tipologia.ToString() ?? "");

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
        internal static List<ClsCasa> GetAll(ref MySqlConnection conn, out string errore)
        {
            DataTable dt = null;
            List<ClsCasa> houses = new List<ClsCasa>();
            errore = string.Empty;

            try
            {
                conn.Open();

                string query = "SELECT * FROM houses";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsCasa casa= new ClsCasa();
                    casa.RagioneSociale = dt.Rows[i]["ragionesociale"].ToString();
                    casa.IndirizzoSedeLegaleID = Convert.ToInt32(dt.Rows[i]["indirizzosedelegaleID"]);
                    casa.IndirizzoSedeOperativaID = Convert.ToInt32(dt.Rows[i]["indirizzosedeoperativaID"]);
                    casa.TipoAzienda = (ClsCasa.eTIPO_AZIENDA) Convert.ToInt32( dt.Rows[i]["tipoAzienda"]);
                    casa.Esclusiva = Convert.ToBoolean(dt.Rows[i]["esclusiva"]);
                    casa.Tipologia = (ClsCasa.eTIPO_CASA)Convert.ToInt32(dt.Rows[i]["tipologia"]);
                    houses.Add(casa);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;
            }

            return houses;
        }

        internal static List<ClsCasa> GetByRagioneSociale(ref MySqlConnection conn, string ragioneSociale, out string errore)
        {
            List<ClsCasa> houses = new List<ClsCasa>();
            errore = string.Empty;

            try
            {
                conn.Open();

                string query = "SELECT * FROM houses WHERE ragionesociale LIKE @ragioneSociale";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ragioneSociale", "%" + ragioneSociale + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClsCasa casa = new ClsCasa();
                    casa.RagioneSociale = dt.Rows[i]["ragionesociale"].ToString();
                    casa.IndirizzoSedeLegaleID = Convert.ToInt32(dt.Rows[i]["indirizzosedelegaleID"]);
                    casa.IndirizzoSedeOperativaID = Convert.ToInt32(dt.Rows[i]["indirizzosedeoperativaID"]);
                    casa.TipoAzienda = (ClsCasa.eTIPO_AZIENDA)Convert.ToInt32(dt.Rows[i]["tipoAzienda"]);
                    casa.Esclusiva = Convert.ToBoolean(dt.Rows[i]["esclusiva"]);
                    casa.Tipologia = (ClsCasa.eTIPO_CASA)Convert.ToInt32(dt.Rows[i]["tipologia"]);

                    houses.Add(casa);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errore = ex.Message;

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return houses;
        }
        #endregion

        #region UPDATE
        internal static long Update(ref MySqlConnection conn, long ID, ClsCasa casa, out string errore)
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

                    string sql = @"UPDATE houses SET ragioneSociale=@ragioneSociale, 
                                indirizzoSedeLegaleID=@indirizzoSedeLegaleID, 
                                indirizzoSedeOperativaID=@indirizzoSedeOperativaID, 
                                tipoAzienda=@tipoAzienda, 
                                esclusiva=@esclusiva,
                                tipologia=@tipologia, 
                                WHERE ID=@ID";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@ragioneSociale", casa.RagioneSociale.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@indirizzoSedeLegaleID", casa.IndirizzoSedeLegaleID.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@indirizzoSedeOperativaID", casa.IndirizzoSedeOperativaID.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@tipoAzienda", casa.TipoAzienda.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@esclusiva", casa.Esclusiva.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@tipologia", casa.Tipologia.ToString() ?? "");

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

                    string sql = "DELETE FROM houses WHERE ID=@ID";

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

                string query = "SELECT COUNT(*) FROM houses";

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