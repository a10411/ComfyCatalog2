using ComfyCatalogBLL.Utils;
using ComfyCatalogDAL.Services;
using ComfyCatalogBOL.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBLL.Logic
{
    public class AdminLogic
    {
        public static async Task<Response> LoginAdmin(string conString, string username, string password)
        {
            Response response = new Response();
            try
            {
                Boolean result = await AdminService.LoginAdmin(conString, username, password);
                if (result)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "User autenticado";
                }
                else
                {
                    response.StatusCode = StatusCodes.NOTFOUND;
                    response.Message = "Credenciais inválidas";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }



        public static async Task<Response> RegisterAdmin(string conString, string username, string password)
        {
            Response response = new Response();
            try
            {
                Boolean result = await AdminService.RegisterAdmin(conString, username, password);
                if (result)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Administrador registado";
                }
                else
                {
                    response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                    response.Message = "Não foi possível registar o administrador.";
                }
                return response;
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
