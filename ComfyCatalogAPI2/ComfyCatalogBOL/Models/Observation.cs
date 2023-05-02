using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relative a uma Observation (Anúncio/Observação)
    /// Implementa a class (ou modelo) Observation e os seus construtores
    /// </summary>
    public class Observation
    {
        public int ObservationID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; } 
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date_Hour { get; set; }


        public Observation() { }

        /// <summary>
        /// Construtor que visa criar um Announcement convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Observation(int obsID, int productID, string title, string body, DateTime date_Hour)
        {
            ObservationID = obsID;
            ProductID = productID;
            Title = title;
            Body = body;
            Date_Hour = date_Hour;
        }

        public Observation(SqlDataReader rdr)
        {
            this.ObservationID = Convert.ToInt32(rdr["obsID"]);
            this.ProductID = Convert.ToInt32(rdr["productID"]);
            this.UserID = Convert.ToInt32(rdr["userID"]);
            this.Title = rdr["title"].ToString() ?? String.Empty;
            this.Body = rdr["body"].ToString() ?? String.Empty;
            this.Date_Hour = Convert.ToDateTime(rdr["date_hour"].ToString());
        }


    }
}
