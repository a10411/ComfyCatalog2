using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Product (Produto)
    /// Implementa a class (ou modelo) Product e os seus construtores
    /// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        public int BrandID { get; set; }
        public int EstadoID { get; set; }
        public string ProductName { get; set; }
        public string Composition { get; set; }
        public string Colour { get; set; }
        public int ClientNumber { get; set; }
        public string ProductType { get; set; }  
        public int SportID { get; set; }
        
        public Product() { }

        /// <summary>
        /// Construtor que visa criar um Product convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Product(SqlDataReader rdr)
        {
            this.ProductID = Convert.ToInt32(rdr["productID"]);
            this.BrandID = Convert.ToInt32(rdr["brandID"]);
            this.EstadoID = Convert.ToInt32(rdr["estadoID"]);
            this.ProductName = rdr["productName"].ToString() ?? string.Empty;
            this.Composition = rdr["composition"].ToString() ?? string.Empty;
            this.Colour = rdr["colour"].ToString() ?? string.Empty;
            this.ClientNumber = Convert.ToInt32(rdr["clientNumber"]);
            this.ProductType = rdr["productType"].ToString() ?? string.Empty;
            this.SportID = Convert.ToInt32(rdr["sportID"]);


        }
    }
}
