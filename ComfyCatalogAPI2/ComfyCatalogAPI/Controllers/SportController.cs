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
    /// <summary>
    /// Controller para a definição de rotas da API para o CRUD relativo aos Produtos
    /// Rota base = api/Comunicados (api é localhost ou é um link, se estiver publicada)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SportController : Controller
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>

        public SportController(IConfiguration configuration)
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
        //[Authorize(Roles = "user")]
        [Route("/api/GetAllSports")]
        public async Task<IActionResult> GetAllSports()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SportLogic.GetAllSports(CS);
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
        //[Authorize(Roles = "user")]
        [Route("/api/GetSport")]
        public async Task<IActionResult> GetSport(int sportID)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SportLogic.GetSport(CS, sportID);
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
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("/api/AddSport")]
        public async Task<IActionResult> AddSport(Sport sportToAdd)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await SportLogic.AddSport(CS, sportToAdd);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
        
    }
}
