using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace ComfyCatalogDAL.Services
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes ao Produto
    /// Isto é, todos os acessos à base de dados relativos ao produto estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class ProductService
    {
        #region GET

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter os Produtos
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista dos produtos</returns>

        public static async Task<List<Product>> GetAllProducts(string conString)
        {   
            var productList = new List<Product>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product(rdr);
                    productList.Add(product);
                }
                rdr.Close();
                con.Close();
            }
            return productList;           
        }

        public static async Task<Product> GetProduct(string conString, int productID)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Product where productID = {productID}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    product = new Product(rdr);
                }
                rdr.Close();
                con.Close();
            }
            return product;
            // retorna um produto com id = 0 caso não encontre nenhum com este ID
        }

        public static async Task<Boolean> CheckIfProductIsFavourite(string conString,int userID, int productID)
        {
            Boolean isFavourite = false;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Favourite WHERE userID = {userID} AND productID = {productID}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    isFavourite = true;
                }
                rdr.Close();
                con.Close();
            }
            return isFavourite;
        }

        

        public static async Task<List<Product>> GetProductBySport(string conString, string sportName)
        {
            var productList = new List<Product>();
            using (SqlConnection con = new SqlConnection( conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT p.* FROM Product p INNER JOIN Sport s ON p.sportID = s.sportID WHERE s.SportName = '{sportName}'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    productList.Add(new Product(rdr));
                }
                rdr.Close();
                con.Close();
            }
            return productList;
        } 

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter os produtos de cada Marca
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista dos produtos</returns>
        public static async Task<List<Product>> GetProductByBrand(string conString, string brandName)
        {
            var productList = new List<Product>();
            using (SqlConnection con = new SqlConnection( conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT p.* FROM Product p INNER JOIN Brand b ON p.brandID = b.brandID WHERE b.BrandName = '{brandName}'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product(rdr);
                    productList.Add(product);
                }
                rdr.Close();
                con.Close();
            }
            return productList;

        }


        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter um user por username
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>User com determinado Username</returns>
        public static async Task<User> GetUserByUsername(string conString, string username)
        {
            User user = new User();
            using(SqlConnection con = new SqlConnection( conString ))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM User WHERE username = '{username}'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user = new User();
                }
                rdr.Close();
                con.Close();
            }

            return user;
        }



        #endregion

        #region POST
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um registo de um produto(adicionar um produto)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>

        public static async Task<Boolean> AddProduct(string conString, Product productToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection( conString ))
                {
                    string addProduct = "INSERT INTO Product (brandID, estadoID,  sportID, nomeCliente, composition, color, size, certification, knittingType) VALUES (@brandID, @estadoID,  @sportID, @nomeCliente, @composition, @color, @size, @certification, @knittingType)";
                    using (SqlCommand queryAddProduct = new SqlCommand(addProduct))
                    {
                        queryAddProduct.Connection = con;
                        queryAddProduct.Parameters.Add("@brandID", SqlDbType.Int).Value = productToAdd.BrandID;
                        queryAddProduct.Parameters.Add("@estadoID", SqlDbType.Int).Value = productToAdd.EstadoID;
                        queryAddProduct.Parameters.Add("@sportID", SqlDbType.Int).Value = productToAdd.SportID;
                        queryAddProduct.Parameters.Add("@nomeCliente", SqlDbType.Char).Value = productToAdd.NomeCliente;
                        queryAddProduct.Parameters.Add("@composition", SqlDbType.Char).Value = productToAdd.Composition;
                        queryAddProduct.Parameters.Add("@color", SqlDbType.Char).Value = productToAdd.Color;
                        queryAddProduct.Parameters.Add("@size", SqlDbType.Char).Value =productToAdd.Size;
                        queryAddProduct.Parameters.Add("@certification", SqlDbType.Char).Value = productToAdd.Certification;
                        queryAddProduct.Parameters.Add("@knittingType", SqlDbType.Char).Value = productToAdd.KnittingType;

                        con.Open();
                        queryAddProduct.ExecuteNonQuery();
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

        /// <summary>
        /// Método que visa aceder à base de dados via SQL Query e adicionar um novo registo na tabela Favourites consoante o username de um user 
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="username">username do cliente a quem se vai adicionar um produto favorito</param>
        /// <returns>True se adicionar, False se não adicionar</returns>
        public static async Task<Boolean> SetFavouriteProductToUser(string conString, int userID, int productID)
        {
            try
            {
                User user = await UserService.GetUser(conString, userID);
                Product product = await GetProduct(conString, productID);

                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addFavourite = "INSERT INTO Favourite (userID, productID, username) VALUES (@userID, @productID, @username)";
                    using (SqlCommand queryAddFavourite = new SqlCommand(addFavourite))
                    {
                        queryAddFavourite.Connection = con;
                        queryAddFavourite.Parameters.Add("@userID", SqlDbType.Int).Value = user.UserID;
                        queryAddFavourite.Parameters.Add("@productID", SqlDbType.Int).Value = product.ProductID;
                        queryAddFavourite.Parameters.Add("@username", SqlDbType.VarChar).Value = user.Username;

                        con.Open();
                        queryAddFavourite.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }


            }
            catch 
            {
                throw;
            }
        }

        public static async Task<Boolean> SetProductToImage(string conString, int productID, int imageID)
        {
            try
            {
                Product product = await GetProduct(conString, productID);
                Image img = await ImageService.GetImageByProductID(conString, imageID);

                using(SqlConnection con = new SqlConnection(conString))
                {
                    string addProductImage = "INSERT INTO Product_Image (imageID, productID) VALUES (@imageID, @productID)";
                    using (SqlCommand queryAddProductImage = new SqlCommand(addProductImage))
                    {
                        queryAddProductImage.Connection = con;
                        queryAddProductImage.Parameters.Add("@imageID", SqlDbType.Int).Value = imageID;
                        queryAddProductImage.Parameters.Add("@productID", SqlDbType.Int).Value = productID;

                        con.Open();
                        queryAddProductImage.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch 
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
        /// <param name="productUpdated">Produto a atualizar</param>
        /// <returns>Produto atualizado ou erro</returns>

        public static async Task<Product> UpdateProduct(string conString, Product productUpdated)
        {
            Product productCurrent = await GetProduct(conString, productUpdated.ProductID);
            productUpdated.ProductID = productUpdated.ProductID != 0 ? productUpdated.ProductID : productCurrent.ProductID;
            productUpdated.BrandID = productUpdated.BrandID != 0 ? productUpdated.BrandID : productCurrent.BrandID;
            productUpdated.EstadoID = productUpdated.EstadoID != 0 ? productUpdated.EstadoID : productCurrent.EstadoID;
            productUpdated.NomeCliente = productUpdated.NomeCliente != String.Empty && productUpdated.NomeCliente != null ? productUpdated.NomeCliente : productCurrent.NomeCliente;
            productUpdated.Composition = productUpdated.Composition != String.Empty && productUpdated.Composition != null ? productUpdated.Composition : productCurrent.Composition;
            productUpdated.Color = productUpdated.Color != String.Empty && productUpdated.Color != null ? productUpdated.Color : productCurrent.Color;
            productUpdated.Size = productUpdated.Size != String.Empty && productUpdated.Size != null ? productUpdated.Size : productCurrent.Size;
            productUpdated.Certification = productUpdated.Certification != String.Empty && productUpdated.Certification != null ? productUpdated.Certification : productCurrent.Certification;
            productUpdated.KnittingType = productUpdated.KnittingType != String.Empty && productUpdated.KnittingType != null ? productUpdated.KnittingType : productCurrent.KnittingType;
            try
            {
                using 
                    (SqlConnection con = new SqlConnection(conString))
                {
                    string updateProduct = "UPDATE Product Set brandID = @brandID, estadoID = @estadoID,  sportID = @sportID, nomeCliente = @nomeCliente, composition = @composition, color = @color, certification = @certification, knittingType = @knittingType  WHERE productID = @productID";
                    using (SqlCommand queryUpdateProduct = new SqlCommand(updateProduct))
                    {
                        queryUpdateProduct.Connection = con;
                        queryUpdateProduct.Parameters.Add("@productID", SqlDbType.Int).Value = productUpdated.ProductID;
                        queryUpdateProduct.Parameters.Add("@brandID", SqlDbType.Int).Value = productUpdated.BrandID;
                        queryUpdateProduct.Parameters.Add("@estadoID", SqlDbType.Int).Value = productUpdated.EstadoID;
                        queryUpdateProduct.Parameters.Add("@sportID", SqlDbType.Int).Value = productUpdated.SportID;
                        queryUpdateProduct.Parameters.Add("@nomeCliente", SqlDbType.Char).Value = productUpdated.NomeCliente;
                        queryUpdateProduct.Parameters.Add("@composition", SqlDbType.Char).Value = productUpdated.Composition;
                        queryUpdateProduct.Parameters.Add("@color", SqlDbType.Char).Value = productUpdated.Color;
                        queryUpdateProduct.Parameters.Add("@certification", SqlDbType.Char).Value = productUpdated.Certification;
                        queryUpdateProduct.Parameters.Add("@knittingType", SqlDbType.Char).Value = productUpdated.KnittingType;
                        con.Open();
                        queryUpdateProduct.ExecuteNonQuery();
                        con.Close();
                        return await GetProduct(conString, productUpdated.ProductID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar o estado de um product
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="productID">ID do produto que pretendemos atualizar</param>
        /// <param name="estadoID">ID do estado para o qual queremos atualizar o produto</param>
        /// <returns>Product com estado atualizado</returns>
        public static async Task<Product> UpdateEstadoProduct(string conString, int productID, int estadoID)
        {
            try
            {
                using (SqlConnection  con = new SqlConnection(conString))
                {
                    string updateEstado = "UPDATE Product SET estadoID = @estadoID WHERE productID = @productID";
                    using (SqlCommand queryUpdateEstado = new SqlCommand(updateEstado))
                    {
                        queryUpdateEstado.Connection = con;
                        queryUpdateEstado.Parameters.Add("@estadoID", SqlDbType.Int).Value = estadoID;
                        queryUpdateEstado.Parameters.Add("@productID", SqlDbType.Int).Value = productID;
                        con.Open();
                        queryUpdateEstado.ExecuteNonQuery();
                        con.Close();
                        return await GetProduct(conString, productID);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e apagar o registo de um produto
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns> sucesso ou erro</returns>
        
        public static async Task<Boolean> DeleteProduct(string conString, int productID)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(conString))
                {
                    string deleteProduct = $"DELETE FROM Product WHERE productID = {productID}";
                    using (SqlCommand queryDeleteProduct = new SqlCommand(deleteProduct))
                    {
                        queryDeleteProduct.Connection = con;
                        con.Open();
                        queryDeleteProduct.ExecuteNonQuery();
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

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e apagar o registo de um produto Favorito
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns> sucesso ou erro</returns>
        public static async Task<Boolean> DeleteFavorite(string conString, int favoriteID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string deleteFavorite = $"DELETE FROM Product WHERE productID = {favoriteID}";
                    using (SqlCommand queryDeleteFavorite = new SqlCommand(deleteFavorite))
                    {
                        queryDeleteFavorite.Connection = con;
                        con.Open();
                        queryDeleteFavorite.ExecuteNonQuery();
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

    }
}
