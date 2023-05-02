using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{   
    /// <summary>
    /// Business Object Layer relativa a um Administrador (Admin)
    /// Implementa a class (ou modelo) Admin e os seus construtores
    /// </summary>
    public class Admin
    {
        public int AdminID { get; set; }
        public string Username { get; set; }
        public string Password_Hash { get; set; }
        public string Password_Salt { get; set; }

        public Admin() { }


        /// <summary>
        /// Construtor que visa criar um Admin convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Admin(SqlDataReader rdr)
        {
            this.AdminID = Convert.ToInt32(rdr["adminID"]);
            this.Username = rdr["username"].ToString() ?? String.Empty;
            this.Password_Hash = rdr["password_hash"].ToString() ?? String.Empty;
            this.Password_Salt = rdr["password_salt"].ToString() ?? String.Empty;
        }
    }
    
}
