using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL;
using ComfyCatalogDAL.Services;
using Microsoft.AspNetCore.Http;
using StatusCodes = ComfyCatalogBLL.Utils.StatusCodes;

namespace ComfyCatalogBLL.Logic
{
    /// <summary>
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo às Imagens
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class ImageLogic
    {

        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todas as imagens que residem na base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (GET - Image (GetAllImages))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <returns>Response com Status Code, mensagem e dados (Lista de Imagens)</returns>
        public static async Task<Response> GetAllImages(string conString)
        {
            Response response= new Response();
            List<Image> imgList = await ImageService.GetAllImages(conString);
            if(imgList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = imgList;
            }
            return response;
        }

        public static async Task<Response> GetImageByID(string conString, int imageID)
        {
            Response response= new Response();
            Image img = await ImageService.GetImageByID(conString, imageID);
            if(img != null)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção da imagem";
                response.Data = img;
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à criação de uma imagem na base de dados. 
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (POST - Product (AddProduct))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="imageToAdd">Product a adicionar </param>
        /// <returns>Response com Status Code e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro</returns>
        /*
        public static async Task<Response> AddImage(string conString, Image imageToAdd)
        {
            Response response= new Response();
            try
            {
                if(await ImageService.AddImage(conString, imageToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Image was added to Catalog";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }
        */

        public static async Task<Response> AddImage(string conString, IFormFile file, int productID)
        {
            Response response = new Response();
            try
            {
                Image image = await ImageService.AddImage(conString, file, productID);

                response.Data = image;
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Image was added";
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }

    


        /// <summary>
        /// Trata da parte lógica relativa à remoção de uma imagem de um produto da base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (DELETE - Image (DeleteImage))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="obsID">ID da imagem a remover</param>
        /// <returns>Response com Status Code e mensagem (indicando que a imagem foi apagada)</returns>
        public static async Task<Response> DeleteImage(string conString, int imageID)
        {
            Response response= new Response();
            try
            {
                if(await ImageService.DeleteImage(conString, imageID))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Image was deleted";
                }
                else
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Image not found or has a relation with a product and therefore it can't be deleted";
                }
            }
            catch(Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();   
            }
            return response;
        }
    }
}
