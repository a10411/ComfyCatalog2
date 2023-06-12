using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL.Auth;
using System.Net.Http.Headers;

namespace ComfyCatalogDAL.Services
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes ao Gestor
    /// Isto é, todos os acessos à base de dados relativos ao gestor estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter todos os registos de utilizadores lá criados (tabela User)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogmAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista de utilizadores</returns>


        #region GET
        public static async Task<List<User>> GetAllUsers(string conString)
        {
            var userList = new List<User>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [User]", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    User user = new User(rdr);
                    userList.Add(user);
                }
                rdr.Close();
                con.Close();
            }
            return userList;
        }

        public static async Task<User> GetUser(string conString, int userID)
        {
            User user = new User();
            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM [USER] WHERE userID ={userID}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user = new User(rdr);
                }
                rdr.Close();
                con.Close();
            }
            return user;
        }

        public static async Task<int> GetUserIDFromCredentials(string conString, string username)
        {
            int userID = 0;

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = $"SELECT UserID FROM [User] WHERE username = '{username}'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    userID = rdr.GetInt32(0);
                }
                rdr.Close();
                con.Close();
            }

            return userID;
        }

        private static async Task<bool> UsernameExists(string conString, string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = "SELECT COUNT(*) FROM [User] WHERE username = @username";
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


        /// <summary>
        /// Método que visa aceder à base de dados SQL via query e confirmar se os dados de email e password são válidos e pertencem a um utilizador existente na BD (efetuar Login)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="email">Email do user que se pretende autenticar</param>
        /// <param name="password">Password do utilizador que se pretende autenticar</param>
        /// <returns>True caso dados estejam corretos (autenticação realizada), false caso dados incorretos ou erro interno</returns>
        public static async Task<Boolean> LoginUser(string conString, string username, string password)
        {
            try
            {
                using(SqlConnection con = new SqlConnection( conString ))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM [User] WHERE username = '{username}'", con);
                    
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.Read())
                    {
                        string hashedPWFromDb = rdr["password_hash"].ToString();
                        string salt = rdr["password_salt"].ToString();
                        rdr.Close();
                        con.Close();
                        if(HashSalt.CompareHashedPasswords(password, hashedPWFromDb, salt))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    rdr.Close ();
                    con.Close();
                    return false;
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
        /// Método que visa aceder à base de dados SQL via query e adicionar um novo utilizador, encriptando a sua password (registo)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="email">Email do utilizador que se pretende registar</param>
        /// <param name="password">Password do utilizador que se pretende registar</param>
        /// <returns>True caso utilizador tenha sido introduzido, erro interno caso tenha existido algum erro</returns>
        public static async Task<Boolean> RegisterUser(string conString, string username, string password)
        {
            if (await UsernameExists(conString, username))
            {
                return false; // Username already exists
            }
            string salt = HashSalt.GenerateSalt();
            byte[] hashedPW = HashSalt.GetHash(password, salt);
            try
            {
                using(SqlConnection con = new SqlConnection(conString))
                {
                    string addUser = "INSERT INTO [User] (username, password_hash, password_salt) VALUES (@username, @password_hash, @password_salt)";
                    using (SqlCommand queryAddUser = new SqlCommand(addUser))
                    {
                        queryAddUser.Connection = con;
                        queryAddUser.Parameters.Add("@username", SqlDbType.Char).Value = username;
                        queryAddUser.Parameters.Add("@password_hash", SqlDbType.VarChar).Value = Convert.ToBase64String(hashedPW);
                        queryAddUser.Parameters.Add("@password_salt", SqlDbType.VarChar).Value = salt;
                        con.Open();
                        queryAddUser.ExecuteNonQuery();
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
