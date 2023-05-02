﻿using System;
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
    public class Product_ImageLogic
    {
        public static async Task<Response> GetAllGetAllImageRelations(string conString)
        {
            Response response = new Response();
            List<Product_Image> list = await Product_ImageService.GetAllImageRelations(conString);
            if (list.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = list;
            }
            return response;
        }
    }
}