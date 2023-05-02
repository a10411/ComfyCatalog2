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
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo aos estados dos Produtos (ativa, inativa, etc.)
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class EstadoLogic
    {
        public static async Task <Response> GetAllEstados(string conString)
        {
            Response response = new Response();
            List<Estado> estadosList = await EstadoService.GetAllEstados(conString);
            if(estadosList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = estadosList;
            }
            return response;
        }

    }
}
