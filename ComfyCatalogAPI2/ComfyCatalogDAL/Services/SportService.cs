using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ComfyCatalogDAL.Services
{
    public class SportService
    {
        public static async Task<List<Sport>> GetAllSports(string conString)
        {
            var sportList = new List<Sport>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Sport", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Sport sport = new Sport(rdr);
                    sportList.Add(sport);
                }
                rdr.Close();
                con.Close();
            }
            return sportList;
        }

        public static async Task<Sport> GetSport(string conString, int sportID)
        {
            Sport sport = new Sport();
            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Sport WHERE sportID ={sportID}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    sport = new Sport(rdr);
                }
                rdr.Close();
                con.Close();
            }
            return sport;
        }

        public static async Task<Boolean> AddSport(string conString, Sport sportToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection( conString))
                {
                    string addSport = "INSERT INTO Sport (sportName) VALUES (@sportName)";
                    using(SqlCommand queryAddSport = new SqlCommand(addSport))
                    {
                        queryAddSport.Connection = con;
                        queryAddSport.Parameters.Add("@sportName", SqlDbType.Char).Value = sportToAdd.SportName;
                        con.Open();
                        queryAddSport.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
