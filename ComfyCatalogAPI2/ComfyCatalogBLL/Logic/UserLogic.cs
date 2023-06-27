using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComfyCatalogDAL.Services;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;

namespace ComfyCatalogBLL.Logic
{
    /// <summary>
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo aos gestores
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class UserLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todos os Gestores presentes na base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (GET - User)
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <returns>Response com Status Code, mensagem e dados (Utilizadores recebidos do DAL)</returns>
        public static async Task<Response> GetAllUsers(string conString)
        {
            Response response = new Response();
            List<User> usersList = await ComfyCatalogDAL.Services.UserService.GetAllUsers(conString);
            if (usersList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = usersList;
            }
            return response;
        }

        public static async Task<Response> GetUser(string conString, int userID)
        {
            Response response = new Response();
            User user = await UserService.GetUser(conString, userID);
            if (user.UserID > 0) 
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = user;
            }
            return response;
        }

        public static async Task<Response> GetUserIDFromCredentials(string conString, string username)
        {
            Response response = new Response();
            int userID = await ComfyCatalogDAL.Services.UserService.GetUserIDFromCredentials(conString, username);
            if (userID > 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "UserID obtido";
                response.Data = userID;
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa ao Login do User
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para resopnder ao request do utilizador (POST - User (LoginUser))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="username">Email do utilizador que pretende fazer login (passado por parâmetro no ComfyCatalogAPI, ao chamar esta função)</param>
        /// <param name="password">Password do utilizador que pretende fazer login (passado por parâmetro no ComfyCatalogAPI, ao chamar esta função)</param>
        /// <returns>Response com Status Code e mensagem (Status Code de sucesso, not found caso não exista um utilizador na BD com estes dados ou erro interno)</returns>
        public static async Task<Response> LoginUser(string conString, string username, string password)
        {
            Response response = new Response();
            try
            {
                Boolean result = await UserService.LoginUser(conString, username, password);
                if (result)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "User autenticado";
                }
                else
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Credenciais inválidas";
                }
                return response;
            }
            catch (Exception ex) 
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa ao Registo do User
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="username">Username do utilizador que pretende fazer registo (passado por parâmetro no ComfyCatalogAPI, ao chamar esta função)</param>
        /// <param name="password">Password do utilizador que pretende fazer registo (passado por parâmetro no ComfyCatalogAPI, ao chamar esta função)</param>
        /// <returns>Response com Status Code e mensagem (Status Code de sucesso ou erro interno)</returns>

        public static async Task<Response> RegisterUser(string conString, string username, string password)
        {
            Response response = new Response();
            try
            {
                Boolean result = await UserService.RegisterUser(conString, username, password);
                if (result)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "User registado";
                }
                else
                {
                    response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                    response.Message = "Não foi possível registar o utilizador.";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode= StatusCodes.INTERNALSERVERERROR;
                response.Message= ex.ToString();
            }
            return response;
        }


    }
}
