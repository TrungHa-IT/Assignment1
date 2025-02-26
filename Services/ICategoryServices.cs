using ASS1.Models;

namespace ASS1.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(short categoryId);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(short categoryId);

    }
}
