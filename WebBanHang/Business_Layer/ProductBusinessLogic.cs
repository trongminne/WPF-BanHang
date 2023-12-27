using System.Collections.Generic;
using System.Diagnostics;
using WebBanHang.Data_Access_Layer;
using static WebBanHang.Data_Access_Layer.ProductDataAccess;

namespace WebBanHang.Business_Layer
{
    public class ProductBusinessLogic
    {
        private readonly ProductDataAccess productDataAccess;

        public ProductBusinessLogic()
        {
            var databaseConnection = new DatabaseConnection();
            productDataAccess = new ProductDataAccess(databaseConnection);
        }

        public bool IsProductExists(string productName)
        {
            return productDataAccess.IsProductExists(productName);
        }

        public bool AddProduct(string productName, int categoryId, int brandId, double price, double priceSale, int quantity)
        {
            return productDataAccess.AddProduct(productName, categoryId, brandId, price, priceSale, quantity);
        }

        public bool EditProduct(string productName, int productId, int categoryId, int brandId, double price, double priceSale, int quantity)
        {
            return productDataAccess.editProduct(productName, productId, categoryId, brandId, price, priceSale, quantity);
        }


        public List<Product> GetProducts()
        {
            return productDataAccess.GetProducts();
        }
    }
}
