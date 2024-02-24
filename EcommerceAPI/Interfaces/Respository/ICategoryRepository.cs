using EcommerceAPI.Models;

namespace EcommerceAPI.Interfaces.Respository
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        void AddCategory(Category category);
        void DeleteCategoryByName(string name);
    }
}
