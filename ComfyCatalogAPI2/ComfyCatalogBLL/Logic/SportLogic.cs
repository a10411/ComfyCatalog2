using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL.Services;
using Microsoft.AspNetCore.Mvc;
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

        public static async Task<Response> GetSport(string conString, int sportID)
        {
            Response response = new Response();
            Sport sport = await SportService.GetSport(conString, sportID);
            if (sport.SportID > 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = sport;
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
