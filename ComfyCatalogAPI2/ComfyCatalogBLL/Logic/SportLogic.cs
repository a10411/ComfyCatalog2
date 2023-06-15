using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBLL.Logic
{
    public class SportLogic
    {
        public static async Task<Response> GetAllSports(string conString)
        {
            Response response = new Response();
            List<Sport> sportList = await SportService.GetAllSports(conString);
            if (sportList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = sportList;
            }
            return response;
        }

        public static async Task<Response> AddSport(string conString, Sport sportToAdd)
        {
            Response response = new Response();
            try
            {
                if (await SportService.AddSport(conString, sportToAdd))
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Sport was added to Catalog.";
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
