using EcommerceAPI.Models;

namespace EcommerceAPI.Interfaces.Service
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        void AddCategory(Category category);
        void DeleteCategoryByName(string name);
    }
}
