using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a uma Imagem
    /// Implementa a class (ou modelo) Image e os seus construtores
    /// </summary>
    public class Image
    {
        public int ImageID { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }


        public Image() { }

        /// <summary>
        /// Construtor que visa criar uma Image convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Image(SqlDataReader rdr)
        {
            this.ImageID = Convert.ToInt32(rdr["imageID"]);
            this.ImagePath = rdr["imagePath"].ToString() ?? String.Empty;
            this.ImageName = rdr["imageName"].ToString() ?? String.Empty;
           
        }
    }
}
