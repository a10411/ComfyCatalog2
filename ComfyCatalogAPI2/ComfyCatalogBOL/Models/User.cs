using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a um Administrador (Admin)
    /// Implementa a class (ou modelo) Admin e os seus construtores
    /// </summary>
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password_Hash { get; set; }
        public string Password_Salt { get; set; }

        public User() { }

        /// <summary>
        /// Construtor que visa criar um User convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public User(SqlDataReader rdr)
        {
            this.UserID = Convert.ToInt32(rdr["userID"]);
            this.Username = rdr["username"].ToString() ?? String.Empty;
            this.Password_Hash = rdr["password_hash"].ToString() ?? String.Empty;
            this.Password_Salt = rdr["password_salt"].ToString() ?? String.Empty;
        }


    }



}
