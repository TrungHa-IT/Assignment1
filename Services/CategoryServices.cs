using ASS1.Models;
using ASS1.Repositories;

namespace ASS1.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task AddCategory(Category category)
        {
            await _categoryRepository.AddCategory(category);
        }

        public async Task DeleteCategory(short categoryId)
        {
            await _categoryRepository.DeleteCategory(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<Category?> GetCategoryById(short categoryId)
        {
            return await _categoryRepository.GetCategoryById(categoryId);
        }

        public async Task UpdateCategory(Category category)
        {
            await _categoryRepository.UpdateCategory(category);
        }
    }
}
