using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Produto Favorito (Favourite)
    /// Implementa a class (ou modelo) Favourite e os seus construtores
    /// </summary>
    public class Favourite
    {

        public int FavouriteID  { get; set; }
        public int UserID { get; set; } 
        public int ProductID { get; set; }
        public Favourite() { }


        /// <summary>
        /// Construtor que visa criar um Favourite convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Favourite(SqlDataReader rdr)
        {
            this.FavouriteID = Convert.ToInt32(rdr["favouriteID"]);
            this.UserID = Convert.ToInt32(rdr["userID"]);
            this.ProductID = Convert.ToInt32(rdr["productID"]);
        }


    }
}
