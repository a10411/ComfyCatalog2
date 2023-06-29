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
    public class FavouriteService
    {

        #region GET

        public static async Task<List<Product>> GetFavouritesByUser(string conString, int userID)
        {
            var favList = new List<Product>();
            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT p.* FROM Product p INNER JOIN Favourite f ON p.productID = f.productID WHERE f.userID = {userID}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product prod = new Product(rdr);
                    favList.Add(prod);

                }
                rdr.Close();   
                con.Close();
            }
            return favList;
        }

        public static async Task<List<Product>> GetAllFavourites(string conString)
        {
            var favList = new List<Product>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT p.* FROM Product p INNER JOIN Favourite f ON p.productID = f.productID", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product prod = new Product(rdr);
                    favList.Add(prod);
                }
                rdr.Close();
                con.Close();
            }

            return favList;
        }

        public static async Task<List<Favourite>> GetAllFavRelations(string conString)
        {
            var favList = new List<Favourite>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Favourite", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Favourite prod = new Favourite(rdr);
                    favList.Add(prod);
                }
                rdr.Close();
                con.Close();
            }

            return favList;
        }
        #endregion


        #region POST



        #endregion


        #region PATCH/PUT



        #endregion


        #region DELETE
        public static async Task<Boolean> DeleteFavourite(string conString, int userID, int productID)
        {
            try
            {
                using(SqlConnection con = new SqlConnection( conString ))
                {
                    string deleteFav = $"DELETE FROM Favourite WHERE userID={userID} AND productID={productID}";
                    using (SqlCommand queryDeleteFav = new SqlCommand(deleteFav))
                    {
                        queryDeleteFav.Connection = con;
                        con.Open();
                        queryDeleteFav.ExecuteNonQuery();
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


        #endregion

    }
}
