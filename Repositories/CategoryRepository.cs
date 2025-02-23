using ASS1.DAO;
using ASS1.Models;
using ASS1.Services;

namespace ASS1.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryDAO _categoryDAO;
        public CategoryRepository(ICategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }
        public async Task AddCategory(Category category)
        {
             await _categoryDAO.AddCategory(category);
        }

        public async Task DeleteCategory(short categoryId)
        {
            await _categoryDAO.DeleteCategory(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryDAO.GetAllCategories();
        }

        public async Task<Category?> GetCategoryById(short categoryId)
        {
            return await _categoryDAO.GetCategoryById(categoryId);
        }

        public async Task UpdateCategory(Category category)
        {
            await _categoryDAO.UpdateCategory(category);
        }
    }
}
