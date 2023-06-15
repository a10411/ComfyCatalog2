using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    public class Sport
    {
        public int SportID { get; set; }   
        public string SportName { get; set; }


        public Sport() { }

        public Sport(SqlDataReader rdr)
        {
            this.SportID = Convert.ToInt32(rdr["sportID"]);
            this.SportName = rdr["sportName"].ToString() ?? String.Empty;
        }

    }
}
