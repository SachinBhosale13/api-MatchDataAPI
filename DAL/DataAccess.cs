using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models;
using System.Configuration;

namespace DAL
{
    public class DataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["MatchDatabaseConn"].ToString();
        public bool SubmitMatchData(MatchData data)
        {
            bool result = false;
            try
            {
                Int64 matchId = 0;

                matchId = InsertMatch(data);

                if (matchId == 0)
                {
                    return false;
                }
                else if (matchId > 0)
                {
                    result = InsertPlayers(data.lstPlayers, matchId);                    
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            
        }

        public Int64 InsertMatch(MatchData data)
        {
            Int64 matchId = 0;
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                SqlCommand cmd = new SqlCommand("SubmitMatchDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MatchName", data.MatchName);
                cmd.Parameters.AddWithValue("@MatchDate", data.MatchDate);
                cmd.Parameters.AddWithValue("@TeamOne", data.TeamOne);
                cmd.Parameters.AddWithValue("@TeamTwo", data.TeamTwo);
                cmd.Parameters.AddWithValue("@StartTime", data.StartTime);
                cmd.Parameters.AddWithValue("@MatchAddress", data.MatchAddress);

                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    //string mId = cmd.ExecuteScalar().ToString();

                    matchId = Convert.ToInt64(cmd.ExecuteScalar());
                }

                return matchId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public bool InsertPlayers(List<PlayersData> lstPlayers,Int64 matchId)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                DataTable dt = new DataTable();

                dt = GetPlayersDataTable(lstPlayers, matchId);

                if (dt != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        SqlCommand cmd = new SqlCommand("SubmitPlayerDetails", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PlayerList", dt);

                        if (conn != null && conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                            int rs = cmd.ExecuteNonQuery();

                            if (rs != 0)
                            {
                                return true;
                            }
                        }                        
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public DataTable GetPlayersDataTable(List<PlayersData> lstPlayers, Int64 matchId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("PlayerName");
                dt.Columns.Add("PlayerType");
                dt.Columns.Add("PlayerPosition");
                dt.Columns.Add("PlayerTeam");
                dt.Columns.Add("MatchId");

                foreach (var item in lstPlayers)
                {
                    dt.Rows.Add(item.PlayerName,item.PlayerType,item.PlayerPosition,item.PlayerTeam,matchId);                    
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CountryModel> GetCountries()
        {
            List<CountryModel> listCountryModel = new List<CountryModel>();            

            SqlConnection conn = new SqlConnection(connString);        
            
            try
            {
                SqlCommand cmd = new SqlCommand("GetCountries", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        CountryModel objCountry = new CountryModel();

                        objCountry.id = Convert.ToInt32(rdr["id"]);
                        objCountry.name = Convert.ToString(rdr["name"]);

                        

                        listCountryModel.Add(objCountry);
                    }
                } 

                return listCountryModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally 
            {
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


    }
}
