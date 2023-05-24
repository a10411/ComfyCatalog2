using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ComfyCatalogBLL.Logic;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL;
using Microsoft.AspNetCore.Authorization;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace ComfyCatalogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObservationController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;

        public ObservationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Request GET relativo às obsevações
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de Observation</returns> 
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        public async Task<IActionResult> GetAllObservations()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ObservationLogic.GetAllObservations(CS);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS) 
            {
                return StatusCode((int)response.StatusCode);

            }
            return new JsonResult(response);
        }

     
        /// <summary>
        /// Request GET relativo às obsevações
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de Observation</returns> 
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("/api/GetObservationByUserID")]
        public async Task<IActionResult> GetObservationsByUserID(int userID)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ObservationLogic.GetObservationsByUserID(CS, userID);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }   
            return new JsonResult(response);
        }
      
        [HttpGet]
        [Route("/ObservationByProduct/observation/{productID}")]
        public async Task<IActionResult> GetObservationByProduct(int productID)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ObservationLogic.GetObservationByProduct(CS, productID);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request POST relativo às Observações
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o Utilizador. Idealmente, retornará uma response que diz que o POST foi bem sucedido.</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpPost]
        [Route("/api/AddObservation")]
        public async Task<IActionResult> AddObservation(Observation obsToAdd)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ObservationLogic.AddObservation(CS, obsToAdd);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request PATCH relativo a uma Observação, que o utilizador pretenda atualizar
        /// </summary>
        /// <param name="obsToUpdate">Comunicado que o utilizador pretende atualizar na BD</param>
        /// <returns>Retorna a resposta obtida pelo BLL para o utlizador. Idealmente, retornará uma Observação atualizada, com um status code 200 (sucesso).</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpPatch]
        public async Task<IActionResult> UpdateObservation(Observation obsToUpdate)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ObservationLogic.UpdateObservation(CS, obsToUpdate);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS) 
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);            
        }

        /// <summary>
        /// Request DELETE relativo a uma observação de um certo produto, que o utilizador pretenda apagar
        /// </summary>
        /// <param name="ObsID">ID do horário a remover da base de dados</param>
        /// <returns>Retorna a response obtida pelo BLL para o utlizador. Idealmente, retornará uma response que diz que o DELETE foi bem sucedido.</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpDelete]
        [Route("/api/DeleteObservation/{observationId}")]
        public async Task <IActionResult> DeleteObservation(int observationId)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ObservationLogic.DeleteObservation(CS, observationId);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        



    }
}
