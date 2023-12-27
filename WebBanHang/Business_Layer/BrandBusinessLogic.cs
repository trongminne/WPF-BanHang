using System.Collections.Generic;
using WebBanHang.Data_Access_Layer;
using System.Data.SqlClient;
using Brand = WebBanHang.Data_Access_Layer.BrandDataAccess.Brand;
namespace WebBanHang.Business_Layer
{
    public class BrandBusinessLogic
    {
        private readonly BrandDataAccess brandDataAccess;

        public BrandBusinessLogic()
        {
            var databaseConnection = new DatabaseConnection();
            brandDataAccess = new BrandDataAccess(databaseConnection);
        }

        public bool IsBrandExists(string brandName)
        {
            return brandDataAccess.IsBrandExists(brandName);
        }

        public bool AddBrand(string brandName)
        {
            return brandDataAccess.AddBrand(brandName);
        }

        public bool EditBrand(string brandName, int brandId)
        {
            return brandDataAccess.editBrand(brandName, brandId);
        }

      

        public List<Brand> GetBrands()
        {
            return brandDataAccess.GetBrands();
        }
    }
}
