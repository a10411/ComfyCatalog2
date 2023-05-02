using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a uma Brand (Marca)
    /// Implementa a class (ou modelo) Brand e os seus construtores
    /// </summary>
    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public Brand() { }

        /// <summary>
        /// Construtor que visa criar uma Brand convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Brand(SqlDataReader rdr) 
        {
            this.BrandID = Convert.ToInt32(rdr["brandID"]);
            this.BrandName = rdr["brandName"].ToString() ?? string.Empty;
        }
    }
}
