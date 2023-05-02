using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBLL.Utils
{
    /// <summary>
    /// Response será o objeto a retornar, em JSON, para o utilizador, quando o mesmo faz um request
    /// O Model é convertido num Response com um status code, mensagem e dados
    /// Para que a API se abstraia de detalhes relacionados com os models (objetos) e não lide diretamente com a base de dados
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Status Code do pedido (200 sucesso, 401 unauthorized, etc.)
        /// </summary>
        public StatusCodes StatusCode { get; set; }

        /// <summary>
        /// Mensagem do pedido, ex: "Sucesso na obtenção dos dados"
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Dados retornados para o utilizador aquando a realização de um request
        /// Por exemplo, num get aos Announcements, os Announcements/observações presentes na base de dados estarão em "Data"
        /// </summary>
        public object Data { get; set; }


        /// <summary>
        /// Construtor da Response com dados
        /// </summary>
        /// <param name="statusCode">Status Code da response</param>
        /// <param name="message">Mensagem da response</param>
        /// <param name="data">Dados da response</param>
        public Response(StatusCodes statusCode, string message, object data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public Response() 
        {
            StatusCode = StatusCodes.NOCONTENT;
            Message = "No content found.";
            Data = null;
        }
    }
}
