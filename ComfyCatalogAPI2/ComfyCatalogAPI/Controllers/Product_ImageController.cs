using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ComfyCatalogBLL.Logic;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL;
using Microsoft.AspNetCore.Authorization;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace ComfyCatalogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Product_ImageController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Product_ImageController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        /// <summary>
        /// Request GET relativo às Imagens
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de Imagens</returns> 
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        public async Task<IActionResult> GetAllImageRelations()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await Product_ImageLogic.GetAllGetAllImageRelations(CS);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
    }
}
