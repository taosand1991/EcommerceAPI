using EcommerceAPI.Interfaces.Respository;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Models;

namespace EcommerceAPI.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }

        public void DeleteCategoryByName(string name)
        {
            _categoryRepository.DeleteCategoryByName(name);
        }
    }
}
