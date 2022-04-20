using BangGiaTrucTuyen.Models;
using System.Data;
using System.Data.SqlClient;

namespace BangGiaTrucTuyen.Repositories
{
    public class ProductRepository
    {
        string connectionString;

        public ProductRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<BANGGIATRUCTUYEN> GetProducts()
        {
            List<BANGGIATRUCTUYEN> products = new List<BANGGIATRUCTUYEN>();
            BANGGIATRUCTUYEN product;

            var data = GetProductDetailsFromDb();

            foreach (DataRow row in data.Rows)
            {
                product = new BANGGIATRUCTUYEN
                {
                    MACP = row["MACP"].ToString(),
                    MUAGIABA = Convert.ToInt32(row["MUAGIA3"]),
                    MUAKLBA = Convert.ToInt32(row["MUAKL3"]),
                    MUAGIAHAI = Convert.ToInt32(row["MUAGIA2"]),
                    MUAKLHAI = Convert.ToInt32(row["MUAKL2"]),
                    MUAGIAMOT = Convert.ToInt32(row["MUAGIA1"]),
                    MUAKLMOT = Convert.ToInt32(row["MUAKL1"]),
                    GIAKHOP = Convert.ToInt32(row["GIAKHOP"]),
                    KLKHOP = Convert.ToInt32(row["KLKHOP"]),
                    BANGIAMOT = Convert.ToInt32(row["BANGIA1"]),
                    BANKLMOT = Convert.ToInt32(row["BANKL1"]),
                    BANGIAHAI = Convert.ToInt32(row["BANGIA2"]),
                    BANKLHAI = Convert.ToInt32(row["BANKL2"]),
                    BANGIABA = Convert.ToInt32(row["BANGIA3"]),
                    BANKLBA = Convert.ToInt32(row["BANKL3"]),
                    TONGKL = Convert.ToInt32(row["TONGKL"])

                };
                products.Add(product);
            }

            return products;
        }
        private DataTable GetProductDetailsFromDb()
        {
            var query = "SELECT MACP, MUAGIA3, MUAKL3, MUAGIA2, MUAKL2, MUAGIA1, MUAKL1, GIAKHOP, KLKHOP, BANGIA1, BANKL1, BANGIA2, BANKL2, BANGIA3, BANKL3, TONGKL FROM dbo.BANGGIATRUCTUYEN";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<ProductForGraph> GetProductsForGraph()
        {
            List<ProductForGraph> productsForGraph = new List<ProductForGraph>();
            ProductForGraph productForGraph;

            var data = GetProductsForGraphFromDb();

            foreach (DataRow row in data.Rows)
            {
                productForGraph = new ProductForGraph
                {
                    MACP = row["MACP"].ToString(),
                    TONGKL = Convert.ToInt32(row["TONGKL"])
                };
                productsForGraph.Add(productForGraph);
            }

            return productsForGraph;
        }

        private DataTable GetProductsForGraphFromDb()
        {
            var query = "SELECT MACP, SUM(TONGKL) TONGKL FROM dbo.BANGGIATRUCTUYEN GROUP BY MACP";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
