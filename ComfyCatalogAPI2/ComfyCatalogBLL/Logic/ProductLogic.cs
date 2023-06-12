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
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo aos produtos
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    public class ProductLogic
    {
        /// <summary>
        /// Trata da parte lógica relativa à obtenção de todas os produtos dados estes que residem na base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (GET - Product (GetAllProducts))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <returns>Response com Status Code, mensagem e dados (Lista de produtos)</returns>
        
        public static async Task<Response> GetAllProducts(string conString)
        {
            Response response = new Response();
            List<Product> productsList = await ProductService.GetAllProducts(conString);
            if (productsList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = productsList;
            }
            return response;
        }

        public static async Task<Response> GetProduct(string conString, int productID)
        {
            Response response = new Response();
            Product product = await ProductService.GetProduct(conString, productID);
            if (product.ProductID > 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = product;
            }
            return response;
        }

        public static async Task<Response> GetProductByBrand(string conString, string brandName)
        {
            Response response = new Response();
            List<Product> productsList = await ProductService.GetProductByBrand(brandName, conString);
            if(productsList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = productsList;

            }
            return response;
        }

        public static async Task<Response> GetProductBySport(string conString, string sport)
        {
            Response response = new Response();
            List<Product> productList = await ProductService.GetProductBySport(sport, conString);
            if(productList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = productList;
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à criação de um produto na base de dados (sala que diz respeito a um Produto)
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (POST - Product (AddProduct))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="productToAdd">Product a adicionar </param>
        /// <returns>Response com Status Code e mensagem (Status Code 200 caso sucesso, ou 500 INTERNAL SERVER ERROR caso tenha havido algum erro</returns>

        public static async Task<Response> AddProduct(string conString, Product productToAdd)
        {
            Response response = new Response();
            try
            {
                if(await ProductService.AddProduct(conString, productToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Product was added to Catalog.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }

        public static async Task<Response> SetFavoriteProductToUser(string conString, int userID, int productID)
        {
            Response response = new Response();
            try
            {
                if(await ProductService.SetFavouriteProductToUser(conString, userID, productID))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Product was Added to Favorites";

                }
            }
            catch(Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à atualização de um produto que resida na base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (PATCH - Produto (UpdateProduct))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="productToUpdate">Produto inserido pelo utilizador para atualizar</param>
        /// <returns>Response com Status Code, mensagem e dados (Produto atualizado)</returns>

        public static async Task<Response> UpdateProduct(string conString, Product productToUpdate)
        {
            Response response = new Response();
            try
            {
                Product productReturned = await ProductService.UpdateProduct(conString,productToUpdate);
                if(productReturned.ProductID == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Product was not found";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Product was updated";
                    response.Data = productReturned;
                }
            }
            catch(Exception ex)
            {
                response.StatusCode= StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }


        /// <summary>
        /// Trata da parte lógica relativa à atualização do estado de um produto, dados estes que residem na base dados (tanto os estados, como os produtos)
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do utilizador (PATCH - Product (UpdateEstadoProduct))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="productID">ID do produto que o utilizador pretende atualizar o estado</param>
        /// <param name="estadoID">ID do novo estado do produto</param>
        /// <returns>Response com Status Code, mensagem e dados (idealmente, nos dados estará a sala atualizada)</returns>
        public static async Task<Response> UpdateEstadoProduct(string conString, int  productID, int estadoID)
        {
            Response response = new Response();
            try
            {
                Product productReturned = await ProductService.UpdateEstadoProduct(conString, productID, estadoID);
                if (productReturned.ProductID == 0)
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Product was not found";
                }
                else
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Product estado was updated";
                    response.Data = productReturned;
                }
            }
            catch( Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }

        /// <summary>
        /// Trata da parte lógica relativa à remoção de um produto da base de dados
        /// Gera uma resposta que será utilizada pela ComfyCatalogAPI para responder ao request do Admin (DELETE - Produto (DeleteProduct))
        /// </summary>
        /// <param name="conString">Connection String da base de dados, que reside no appsettings.json do projeto ComfyCatalogAPI</param>
        /// <param name="productID">ID do produto a remover</param>
        /// <returns>Response com Status Code e mensagem (indicando que o produto foi removido)</returns>
        public static async Task<Response> DeleteProduct(string conString, int productID)
        {
            Response response = new Response();
            try
            {
                if(await ProductService.DeleteProduct(conString, productID))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Product was deleted from DB";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }





    }
}
