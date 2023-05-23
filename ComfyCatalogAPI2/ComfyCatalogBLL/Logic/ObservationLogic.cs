using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL;
using ComfyCatalogDAL.Services;

namespace ComfyCatalogBLL.Logic
{
    /// <summary>
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo às Observações
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class ObservationLogic
    {
        public static async Task <Response> GetObservation(string conString)
        {
            Response response = new Response();
            List<Observation> obsList = await ObservationService.GetAllObservations(conString);
            if(obsList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = obsList;
            }
            return response;
        }

        public static async Task<Response> GetObservationByProduct(string conString, int productID)
        {
            Response response = new Response();
            List<Observation> obsList = await ObservationService.GetObservationByProduct(conString, productID);
            if (obsList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = obsList;
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à criação de uma Obsercvação na base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (POST - Observação)
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="obsToAdd">Comunicado a adicionar (productID, userID, titulo, corpo, etc.)</param>
        /// <returns>Response com Status Code e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro</returns>
        public static async Task<Response> AddObservation(string conString, Observation obsToAdd)
        {
            Response response = new Response();
            try
            {
                if(await ObservationService.AddObservation(conString, obsToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Observation was added";

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }
        /// <summary>
        /// Trata da parte lógica relativa à atualização de uma observação que resida na base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (PATCH - Observation (UpdateObservation))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto MonitumAPI</param>
        /// <param name="obsToUpdate">Observation inserida pelo user para atualizar</param>
        /// <returns>Response com Status Code, mensagem e dados (Observação atualizada)</returns>
        public static async Task<Response> UpdateObservation(string conString, Observation obsToUpdate)
        {
            Response response = new Response();
            try
            {
                Observation obsReturned = await ObservationService.UpdateObservation(conString, obsToUpdate);
                if (obsReturned.ObservationID == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Observation was not found";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Observation was updated";
                    response.Data = obsReturned;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à remoção de uma observação feita por determinado user relativa a um produto da base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (DELETE - Observation (DeleteObservationByProduct))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="obsID">ID da observação a remover</param>
        /// <returns>Response com Status Code e mensagem (indicando que a observação foi apagado)</returns>

        public static async Task<Response> DeleteObservation(string conString, int observationId)
        {
            Response response = new Response();
            try
            {
                if(await ObservationService.DeleteObservation(conString, observationId))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Observation was deleted from this product";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode= StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }


    }
}
