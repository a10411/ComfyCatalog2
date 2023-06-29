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
    public class FavouriteLogic
    {

        public static async Task<Response> GetFavouritesByUser(string conString, int userID)
        {
            Response response = new Response();
            List<Product> favList = await FavouriteService.GetFavouritesByUser(conString, userID);
            if (favList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = favList;
            }
            return response;
        }

        public static async Task<Response> GetAllFavourite(string conString)
        {
            Response response = new Response();
            List<Product> favList = await FavouriteService.GetAllFavourites(conString);
            if (favList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = favList;
            }
            return response;
        }

        public static async Task<Response> GetAllFavRelations(string conString)
        {
            Response response = new Response();
            List<Favourite> favList = await FavouriteService.GetAllFavRelations(conString);
            if (favList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = favList;
            }
            return response;
        }

        public static async Task<Response> DeleteFavourite(string conString, int userID, int productID)
        {
            Response response = new Response();
            try
            {
                if (await FavouriteService.DeleteFavourite(conString, userID, productID))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Favourite was deleted from DB";
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
