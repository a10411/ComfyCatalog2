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
        public int SportID { get; set; }
        public string NomeCliente { get; set; }
        public string Composition { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Certification { get; set; }  
        public string KnittingType { get; set; }
     
        
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
            this.SportID = Convert.ToInt32(rdr["sportID"]);
            this.NomeCliente = rdr["nomeCliente"].ToString() ?? string.Empty;
            this.Composition = rdr["composition"].ToString() ?? string.Empty;
            this.Color = rdr["color"].ToString() ?? string.Empty;
            this.Size = rdr["size"].ToString() ?? string.Empty;
            this.Certification = rdr["certification"].ToString() ?? string.Empty;
            this.KnittingType = rdr["knittingType"].ToString() ?? string.Empty;
          
            



        }
    }
}
