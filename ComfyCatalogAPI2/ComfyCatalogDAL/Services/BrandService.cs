using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogDAL.Services
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes a Brand (marcas)
    /// Isto é, todos os acessos à base de dados relativos ao produto estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class BrandService
    {
        #region GET
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter as Marcas
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista das marcas</returns>


        public static async Task<List<Brand>> GetAllBrands(string conString)
        {
            var brandList = new List<Brand>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Brand", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Brand brand = new Brand(rdr);
                    brandList.Add(brand);
                }
                rdr.Close();
                con.Close();
            }
            return brandList;
        }

        public static async Task<Brand> GetBrand(string conString, int brandID)
        {
            Brand brand = new Brand();
            using(SqlConnection con = new SqlConnection( conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Brand where brandID = {brandID}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    brand = new Brand(rdr);
                }
                rdr.Close();
                con.Close();    
            }
            return brand;
            // retorna um brand com id = 0 caso não encontre nenhum com este ID
        }

        #endregion


        #region POST

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um registo de uma brand (adicionar um produto)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>
        public static async Task<Boolean> AddBrand(string conString, Brand brandToAdd)
        {
            try
            {
                using(SqlConnection con = new SqlConnection( conString ))
                {
                    string addBrand = "INSERT INTO Brand (brandName) VALUES (@brandName)";
                    using(SqlCommand queryAddBrand = new SqlCommand(addBrand))
                    {
                        queryAddBrand.Connection = con;
                        queryAddBrand.Parameters.Add("@brandName", SqlDbType.Char).Value = brandToAdd.BrandName;
                        con.Open();
                        queryAddBrand.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion


        #region PATCH/PUT

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar o registo de um producto
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="brandUpdated">Brand a atualizar</param>
        /// <returns>Brand atualizada ou erro</returns>
        public static async Task<Brand> UpdateBrand(string conString, Brand brandUpdated)
        {
            Brand brandCurrent = await GetBrand(conString, brandUpdated.BrandID);
            brandUpdated.BrandID = brandUpdated.BrandID != 0 ? brandUpdated.BrandID : brandCurrent.BrandID;
            brandUpdated.BrandName = brandUpdated.BrandName != String.Empty && brandUpdated.BrandName != null? brandUpdated.BrandName : brandCurrent.BrandName;

            try
            {
                using(SqlConnection con =new SqlConnection( conString))
                {
                    string updateBrand = "UPDATE Brand SET brandName = @brandName WHERE brandID = @brandID";
                    using(SqlCommand queryUpdateBrand = new SqlCommand(updateBrand))
                    {
                        queryUpdateBrand.Connection = con;
                        queryUpdateBrand.Parameters.Add("@brandID", SqlDbType.Int).Value = brandUpdated.BrandID;
                        queryUpdateBrand.Parameters.Add("@brandName", SqlDbType.Char).Value = brandUpdated.BrandName;
                        con.Open();
                        queryUpdateBrand.ExecuteNonQuery();
                        con.Close();
                        return await GetBrand(conString, brandUpdated.BrandID);
                    }
                }

            }
            catch (Exception ex ) 
            {
                throw;
            }

        }


        #endregion


        #region DELETE



        #endregion



    }
}
