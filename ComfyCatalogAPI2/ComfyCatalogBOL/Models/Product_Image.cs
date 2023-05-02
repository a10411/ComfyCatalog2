using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a uma relação entre um Produto e uma Imagem (Product_Image)
    /// Implementa a class (ou modelo) Product_Image e os seus construtores
    /// </summary>
    public class Product_Image
    {
        public int ProductImage { get; set; }
        public int ProductID { get; set;}
        public int ImageID { get; set; }

        public Product_Image() { }

        /// <summary>
        /// Construtor que visa criar um Product_Image convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Product_Image(SqlDataReader rdr)
        {
            this.ProductImage = Convert.ToInt32(rdr["product_imageID"]);
            this.ProductID = Convert.ToInt32(rdr["productID"]);
            this.ImageID = Convert.ToInt32(rdr["imageID"]);
        }

    }
}
