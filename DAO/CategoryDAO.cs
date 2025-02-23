using ASS1.Models;
using Microsoft.EntityFrameworkCore;

namespace ASS1.DAO
{
    public class CategoryDAO : ICategoryDAO
    {
        private readonly FunewsManagementContext _context;
        public CategoryDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.InverseParentCategory)
                .Include(c => c.NewsArticles)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryById(short categoryId)
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.InverseParentCategory)
                .Include(c => c.NewsArticles)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(short categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
