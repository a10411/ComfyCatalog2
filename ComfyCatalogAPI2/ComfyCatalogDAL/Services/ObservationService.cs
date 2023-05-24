using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ComfyCatalogDAL.Services
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes às Observações
    /// Isto é, todos os acessos à base de dados relativos às observações estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class ObservationService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter todos os registos de Observações lá criados (tabela Observation)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista de comunicados</returns>
        public static async Task<List<Observation>> GetAllObservations(string conString)
        {
            var obsList = new List<Observation>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Observation", con);
                cmd.CommandType = CommandType.Text; 
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Observation observation = new Observation(rdr);
                    obsList.Add(observation);
                }
                rdr.Close();
                con.Close();
            }
            return obsList;
        }

        public static async Task<List<Observation>> GetObservationsByUserID(string conString, int userID)
        {
            var obsList = new List<Observation>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Observation WHERE userID = @UserID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (rdr.Read())
                {
                    Observation observation = new Observation(rdr);
                    obsList.Add(observation);
                }
                rdr.Close();
                con.Close();
            }
            return obsList;
        }

        public static async Task<List<Observation>> GetObservationByProduct(string conString, int productID )
        {
            var obsList = new List<Observation>();
            using(SqlConnection con = new SqlConnection( conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Observation where productID = {productID}", con);
                cmd.CommandType = CommandType.Text; 
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    Observation observation = new Observation(rdr);
                    obsList.Add(observation);
                }
                rdr.Close();
                con.Close();
            }
            return obsList;
        }

        public static async Task<Observation> GetObservation(string conString, int obsID)
        {
            Observation obs = new Observation();
            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Observation where obsID = {obsID}", con);
                cmd.CommandType = CommandType.Text; 
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    obs = new Observation(rdr);
                }
                rdr.Close();
                con.Close();
            }
            return obs;
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um registo de uma observação
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>

        public static async Task<Boolean> AddObservation(string conString, Observation obsToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addObservation = "INSERT INTO Observation (productID, userID, title, body, date_hour) VALUES (@productID, @userID, @title, @body, @date_Hour)";
                    using (SqlCommand queryAddObservation = new SqlCommand(addObservation))
                    {
                        queryAddObservation.Connection = con;
                        queryAddObservation.Parameters.Add("@productID", SqlDbType.Int).Value = obsToAdd.ProductID;
                        queryAddObservation.Parameters.Add("@userID", SqlDbType.Int).Value= obsToAdd.UserID;
                        queryAddObservation.Parameters.Add("@title", SqlDbType.Char).Value = obsToAdd.Title;
                        queryAddObservation.Parameters.Add("@body", SqlDbType.Char).Value = obsToAdd.Body;
                        queryAddObservation.Parameters.Add("@date_Hour", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                        con.Open ();
                        queryAddObservation.ExecuteNonQuery();
                        con.Close ();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar o registo de uma Observação (atualizar uma observação relativa a um produto)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="obsToAdd">Observação a atualizar</param>
        /// <returns>Observação atualizada ou erro</returns>
        public static async Task<Observation> UpdateObservation(string conString, Observation obsUpdated)
        {
            Observation obsCurrent = await GetObservation(conString, obsUpdated.ObservationID);
            obsUpdated.ObservationID = obsUpdated.ObservationID !=0 ? obsUpdated.ObservationID : obsCurrent.ObservationID;
            obsUpdated.ProductID = obsUpdated.ProductID != 0 ? obsUpdated.ProductID : obsCurrent.ProductID;
            obsUpdated.Title = obsUpdated.Title != String.Empty && obsUpdated.Title != null ? obsUpdated.Title : obsCurrent.Title;
            obsUpdated.Body = obsUpdated.Body != String.Empty && obsUpdated.Body != null ? obsUpdated.Body : obsCurrent.Body;
            obsUpdated.UserID = obsUpdated.UserID != 0 ? obsUpdated.UserID : obsCurrent.UserID; 

            try
            {
                using(SqlConnection con = new SqlConnection (conString))
                {
                    string updateObs = "UPDATE Observation SET title = @title, body = @body where obsID = @obsID";
                    using (SqlCommand queryUpdateObs = new SqlCommand(updateObs))
                    {
                        queryUpdateObs.Connection = con;
                        queryUpdateObs.Parameters.Add("@title", SqlDbType.VarChar).Value = obsUpdated.Title;
                        queryUpdateObs.Parameters.Add("body", SqlDbType.VarChar).Value= obsUpdated.Body;
                        queryUpdateObs.Parameters.Add("obsID", SqlDbType.VarChar).Value = obsUpdated.ObservationID;
                        queryUpdateObs.Parameters.Add("userID", SqlDbType.VarChar).Value = obsUpdated.UserID;
                        con.Open();
                        queryUpdateObs.ExecuteNonQuery();
                        con.Close();
                        return await GetObservation(conString, obsUpdated.ObservationID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e apagar uma observação existente na mesma
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="obsID">ID da observação a apagar</param>
        /// <returns>True caso tudo tenha corrido bem (observação removida), algum erro caso a observação não tenha sido removida.</returns>
        public static async Task<Boolean> DeleteObservation(string conString, int observationId)
        {
            try
            {
                using(SqlConnection con = new SqlConnection (conString))
                {
                    string deleteObs = "DELETE FROM Observation WHERE obsID = @ObsID";
                    using (SqlCommand queryDeleteObs = new SqlCommand(deleteObs))
                    {
                        queryDeleteObs.Parameters.AddWithValue("@ObsID", observationId);
                        queryDeleteObs.Connection = con;
                        con.Open();
                        queryDeleteObs.ExecuteNonQuery ();
                        con.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
