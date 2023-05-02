using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ComfyCatalogDAL.Services
{
    public class Product_ImageService
    {
        public static async Task<List<Product_Image>> GetAllImageRelations(string conString)
        {
            var relationList = new List<Product_Image>();
            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product_Image", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product_Image PI = new Product_Image(rdr);
                     relationList.Add(PI);
                }
                rdr.Close();
                con.Close();
            }
            return relationList;
        }
    }
}
