﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ComfyCatalogBLL.Logic;
using ComfyCatalogBLL.Utils;
using ComfyCatalogDAL;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
using ComfyCatalogAPI.Utils;

namespace ComfyCatalogAPI.Controllers
{
    /// <summary>
    /// Controller para a definição de rotas da API para o CRUD relativo ao User
    /// Rota base = api/User (api é localhost ou é um link, se estiver publicada)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Authorize (Roles = "user")]
        [Route("api/GetIDByToken")]
        public IActionResult GetIDByToken(string token)
        {
            try
            {
                // Validate the token or perform any necessary checks

                // Extract the user ID from the token
                JwtUtils jwt = new JwtUtils(_configuration);
                string userID = jwt.GetUserIDByToken(token);

                if (string.IsNullOrEmpty(userID))
                {
                    // User ID not found in the token
                    return BadRequest("Invalid token or user ID not found.");
                }

                // Return the user ID in the response
                return Ok(userID);
            }
            catch (Exception ex)
            {
                // An error occurred while processing the token
                return StatusCode(500, "An error occurred while retrieving the user ID.");
            }
        }


        /// <summary>
        /// Request GET relativo aos Utilizadores
        /// Apenas um admin consegue fazer este request com sucesso (Authorize(Roles="Admin"))
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de Users</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("api/GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await UserLogic.GetAllUsers(CS);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("api/GetUser")]
        public async Task<IActionResult> GetUser(int userID)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await UserLogic.GetUser(CS, userID);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }


        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("/api/GetUserIDFromCredentials")]
        public async Task<IActionResult> GetUserIDFromCredentials(string username)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await UserLogic.GetUserIDFromCredentials(CS, username);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);

        }



        /// <summary>
        /// Request POST relativo ao Login de User
        /// </summary>
        /// <param name="username">Username do gestor</param>
        /// <param name="password">Password do gestor, para posteriormente, no DAL, fazer a confirmação do hash e salt</param>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente (caso o gestor tenha introduzido as credenciais corretas), retorna uma resposta de sucesso (utilizador autenticado)</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpPost]
        [Route("/api/LoginUser")]
        
        public async Task<IActionResult> LoginUser(string username, string password)
        {
            

            string CS = _configuration.GetConnectionString("WebApiDatabase");   
            Response response = await UserLogic.LoginUser(CS, username, password);
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            JwtUtils jwt = new JwtUtils(_configuration);
            var token = jwt.GenerateJWTToken("user");
            response.Data = token;
            return new JsonResult(response);
        }

        /// <summary>
        /// Request POST relativo ao Registo de User
        /// </summary>
        /// <param name="email">Username do utilizador para registar</param>
        /// <param name="password">Password do utilizador para registar, posteriomente, no DAL, fará a conversão para hash e salt</param>
        /// <returns>Retorna a response obtida pelo DLL para o utilizador. Idealmente (caso o gestor tenha introduzido dados válidos para registo), retornará uma resposta de sucesso (utilizador registado).</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("/api/RegisterUser")]
        public async Task<IActionResult> RegisterUser(string username, string password)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await UserLogic.RegisterUser(CS, username, password);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }



    }
}
