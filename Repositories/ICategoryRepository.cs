using ASS1.Models;

namespace ASS1.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(short categoryId);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(short categoryId);
    }
}
