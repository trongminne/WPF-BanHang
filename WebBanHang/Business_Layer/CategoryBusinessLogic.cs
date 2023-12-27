using System.Collections.Generic;
using WebBanHang.Data_Access_Layer;
using static WebBanHang.Data_Access_Layer.CategoryDataAccess;

namespace WebBanHang.Business_Layer
{
    public class CategoryBusinessLogic
    {
        private readonly CategoryDataAccess categoryDataAccess;

        public CategoryBusinessLogic()
        {
            var databaseConnection = new DatabaseConnection();
            categoryDataAccess = new CategoryDataAccess(databaseConnection);
        }

        public bool IsCategoryExists(string categoryName)
        {
            return categoryDataAccess.IsCategoryExists(categoryName);
        }

        public bool AddCategory(string categoryName)
        {
            return categoryDataAccess.AddCategory(categoryName);
        }

        public bool EditCategory(string categoryName, int categoryId)
        {
            return categoryDataAccess.editCategory(categoryName, categoryId);
        }


        public List<Category> GetCategorys()
        {
            return categoryDataAccess.GetCategorys();
        }
    }
}
