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
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace ComfyCatalogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IContentTypeProvider _contentTypeProvider;

  

        public ImageController(IConfiguration configuration, IWebHostEnvironment env, IContentTypeProvider contentTypeProvider)
        {
            _configuration = configuration;
            _env = env;
            _contentTypeProvider = contentTypeProvider;
        }
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("/api/GetAllImages")]
        public async Task<IActionResult> GetAllImages()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ImageLogic.GetAllImages(CS);
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
        [Route("/api/GetImage/{imageName}")]
        public IActionResult GetImage(string imageName, [FromServices] IContentTypeProvider contentTypeProvider)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                string contentType;

                if (!contentTypeProvider.TryGetContentType(imageName, out contentType))
                {
                    // Set a default content type if the provider cannot determine it
                    contentType = "application/octet-stream";
                }

                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, contentType);
            }

            return NotFound();
        }

        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        [Route("/api/GetImageByProductID")]
        public async Task<IActionResult> GetImageByProductID(int productID)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ImageLogic.GetImageByProductID(CS, productID);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }


        [HttpPost]
        [Route("/api/addImage")]
        public async Task<IActionResult> AddImage(IFormFile file, int productID)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ImageLogic.AddImage(CS, file, productID);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }


        /// <summary>
        /// Request DELETE relativo a uma Imagem o de um certo produto, que o utilizador pretenda apagar
        /// </summary>
        /// <param name="ImageID">ID do horário a remover da base de dados</param>
        /// <returns>Retorna a response obtida pelo BLL para o utlizador. Idealmente, retornará uma response que diz que o DELETE foi bem sucedido.</returns>
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpDelete]
        public async Task <IActionResult> DeleteImage(int productID)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ImageLogic.DeleteImage(CS, productID);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);

            }
            return new JsonResult(response);
        }




    }
}
