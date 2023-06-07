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

/// <summary>
/// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo às marcas
/// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
/// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
/// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
/// Gera uma response com um status code e dados
{
    public class BrandLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todas as marcas dados estes que residem na base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (GET - Brand (GetAllBrands))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <returns>Response com Status Code, mensagem e dados (Lista de produtos)</returns>
        public static async Task<Response> GetAllBrands(string conString)
        {
            Response response = new Response();
            List<Brand> brandList = await BrandService.GetAllBrands(conString);
            if (brandList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Success in obtaining the data";
                response.Data = brandList; 
            }
            return response;
        }

        public static async Task<Response> GetBrand(string conString, int brandID)
        {
            Response response = new Response();
            Brand brand = await BrandService.GetBrand(conString, brandID);
            if(brand.BrandID > 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Success in obtaining the data";
                response.Data = brand;
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à criação de um produto na base de dados (sala que diz respeito a uma Marca)
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (POST - Brand (AddBrand))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="brandToAdd">Product a adicionar </param>
        /// <returns>Response com Status Code e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro</returns>
        public static async Task<Response> AddBrand(string conString, Brand brandToAdd)
        {
            Response response = new Response();
            try
            {
                if(await BrandService.AddBrand(conString, brandToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Brand was added to Catalog";
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
        /// Trata da parte lógica relativa à atualização de uma Brand que resida na base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (PATCH - Brand (UpdateBrand))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="brandToUpdate">Marca inserido pelo utilizador para atualizar</param>
        /// <returns>Response com Status Code, mensagem e dados (Produto atualizado)</returns>
        
        public static async Task<Response> UpdateBrand(string conString, Brand brandToUpdate)
        {
            Response response = new Response();
            try
            {
                Brand brandReturned = await BrandService.UpdateBrand(conString, brandToUpdate);
                if(brandReturned.BrandID == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Brand was not found";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Brand was updated";
                    response.Data = brandReturned;
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
