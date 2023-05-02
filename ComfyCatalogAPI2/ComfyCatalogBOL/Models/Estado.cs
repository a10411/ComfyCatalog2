using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Estado
    /// Implementa a class (ou modelo) Estado e os seus construtores
    /// </summary>
    public class Estado
    {
        public int EstadoID { get; set; }
        public string EstadoName { get; set; }

        public Estado() { }

        /// <summary>
        /// Construtor que visa criar um Estado convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Estado(SqlDataReader rdr) 
        {
            this.EstadoID = Convert.ToInt32(rdr["estadoID"]);
            this.EstadoName = rdr["estado"].ToString() ?? String.Empty;

        }
    }
}
