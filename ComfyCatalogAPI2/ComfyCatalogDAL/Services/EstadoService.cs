using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogDAL.Services
{
    public class EstadoService
    {
        public static async Task<List<Estado>>  GetAllEstados(string conString)
        {
            var estadoList = new List<Estado>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Estado", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Estado estado = new Estado(rdr);
                    estadoList.Add(estado);
                }
                rdr.Close();
                con.Close();
            }
            return estadoList;
        }
    }
}
