using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ComfyCatalogDAL.Auth
{
    /// <summary>
    /// Classe que visa implentar todas as funções de encriptação e desencriptação de passwords
    /// </summary>
    internal class HashSalt
    {
        /// <summary>
        /// Classe que visa gerar um random salt, que acompanhará a password para a sua encriptação e posterior desencriptação
        /// </summary>
        /// <returns>Salt gerado</returns>
        public static string GenerateSalt()
        {
            byte[] salt;
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt = new byte[16]);
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Gera um hash consoante a password e o salt anteriormente gerado
        /// </summary>
        /// <param name="plainPassword">Password introduzida para gerar o hash</param>
        /// <param name="Salt">Salt gerado</param>
        /// <returns>Hash gerado</returns>
        public static byte[] GetHash(string plainPassword, string salt)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(String.Concat(salt, plainPassword));
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(byteArray);
            return hashedBytes;
        }

        /// <summary>
        /// Compara duas hashed passwords, a introduzida pelo user, e a existente na base de dados (com auxilio do Salt já existente na BD)
        /// Caso sejam iguais, retorna true, indicando que a password introduzida está correta
        /// </summary>
        /// <param name="userInputPassword">Password introduzida pelo utilizador</param>
        /// <param name="ExistingHashedBase64StringPassword">Hashed password existente na base de dados</param>
        /// <param name="SaltOnDB">Salt existente na base de dados</param>
        /// <returns>True caso iguais (password correta), false caso diferentes (password introduzida incorreta)</returns>
        public static bool CompareHashedPasswords(string userInputPassword, string ExistingHashedBase64StringPassword, string SaltOnDB)
        {
            string UserInputHashedPassword = Convert.ToBase64String(GetHash(userInputPassword, SaltOnDB));
            return ExistingHashedBase64StringPassword == UserInputHashedPassword;
        }
    }
}
