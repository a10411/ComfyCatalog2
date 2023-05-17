using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComfyCatalogDAL.Auth;

namespace ComfyCatalogDAL.Services
{
    public class AdminService
    {
        /// <summary>
        /// Class que visa implementar todos os métodos de Data Access Layer referentes ao Administrador
        /// Isto é, todos os acessos à base de dados relativos ao administrador estarão implementados em funções implementadas dentro desta classe
        /// </summary>

        #region GET


        /// <summary>
        /// Método que visa aceder à base de dados SQL via query e confirmar se os dados de email e password são válidos e pertencem a um administrador existente na BD (efetuar Login)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="username">Username do administrador que se pretende autenticar</param>
        /// <param name="password">Password do administrador que se pretende autenticar</param>
        /// <returns>True caso dados estejam corretos (autenticação válida), false caso dados incorretos ou erro interno</returns>
        public static async Task<Boolean> LoginAdmin(string conString, string username, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM [Admin] where username = '{username}'", con);

                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        // Hash e salt handling
                        string hashedPWFromDb = rdr["password_hash"].ToString();
                        string salt = rdr["password_salt"].ToString();
                        rdr.Close();
                        con.Close();    
                        if (HashSalt.CompareHashedPasswords(password, hashedPWFromDb, salt))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    rdr.Close();
                    con.Close();
                    return false;
                }
            }
            catch
            {
                throw;
            }

        }

        private static async Task<bool> UsernameExists(string conString, string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = "SELECT COUNT(*) FROM [Admin] WHERE username = @username";
                    using (SqlCommand command = new SqlCommand(query))
                    {
                        command.Connection = con;
                        command.Parameters.Add("@username", SqlDbType.Char).Value = username;
                        con.Open();
                        int count = (int)await command.ExecuteScalarAsync();
                        con.Close();
                        return count > 0; // Returns true if the username already exists
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region POST

        /// <summary>
        /// Método que visa aceder à base de dados SQL via query e adicionar um novo administrador, encriptando a sua password (registo)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="username">Email do administrador que se pretende registar</param>
        /// <param name="password">Password do administrador que se pretende registar</param>
        /// <returns>True caso administrador tenha sido introduzido, erro interno caso tenha existido algum erro</returns>
        public static async Task<Boolean> RegisterAdmin(string conString, string username, string password)
        {
            if (await UsernameExists(conString, username))
            {
                return false; // Username already exists
            }
            string salt = HashSalt.GenerateSalt();
            byte[] hashedPW = HashSalt.GetHash(password, salt);
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addAdmin = "INSERT INTO Admin (username, password_hash, password_salt) VALUES (@username, @password_hash, @password_salt)";
                    using (SqlCommand queryAddAdmin = new SqlCommand(addAdmin))
                    {
                        queryAddAdmin.Connection = con;
                        queryAddAdmin.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                        queryAddAdmin.Parameters.Add("@password_hash", SqlDbType.VarChar).Value = Convert.ToBase64String(hashedPW);
                        queryAddAdmin.Parameters.Add("@password_salt", System.Data.SqlDbType.VarChar).Value = salt;
                        con.Open();
                        queryAddAdmin.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }

                }
            }
            catch
            {
                throw;
            }
        }


        #endregion


        #region PATCH/PUT



        #endregion


        #region DELETE



        #endregion

    }
}
