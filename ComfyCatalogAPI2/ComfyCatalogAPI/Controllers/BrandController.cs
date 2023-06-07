using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ComfyCatalogBLL.Logic;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL;
using Microsoft.AspNetCore.Authorization;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
using System.Text.RegularExpressions;

namespace ComfyCatalogAPI.Controllers
{
    /// <summary>
    /// Controller para a definição de rotas da API para o CRUD relativo às Brands (marcas)
    /// Rota base = api/Comunicados (api é localhost ou é um link, se estiver publicada)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BrandController : Controller
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>

        public BrandController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Request GET relativo às Marcas
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de marcas</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("/api/GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await BrandLogic.GetAllBrands(CS);
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
        [Route("/api/GetBrand")]
        public async Task<IActionResult> GetBrand(int brandID)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await BrandLogic.GetBrand(CS, brandID);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request POST relativo a Brand (adicionar uma marca ao Catalogo)
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará uma resposta com status code 200 (sucesso)</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        //[Authorize]
        [HttpPost]
        [Route("/api/AddBrand")]
        public async Task<IActionResult> AddBrand(Brand brandToAdd)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await BrandLogic.AddBrand(CS, brandToAdd);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

        /// <summary>
        /// Request PATCH relativo a uma Brand, que o utilizador pretenda atualizar
        /// </summary>
        /// <param name="brandToUpdate">Sala que o gestor pretende atualizar na BD</param>
        /// <returns>Retorna a resposta obtida pelo BLL para o gestor. Idealmente, retornará a sala atualizada, com um status code 200 (sucesso).</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        //[Authorize]
        [HttpPatch]
        public async Task <IActionResult> UpdateBrand(Brand brandToUpdate)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await BrandLogic.UpdateBrand(CS, brandToUpdate);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }

    }
}
