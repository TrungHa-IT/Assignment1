using ASS1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASS1.DAO
{
    public class NewsArticleDAO : INewsArticleDAO // Implement the interface here
    {
        private readonly FunewsManagementContext _context;

        public NewsArticleDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNews()
        {
            return await _context.NewsArticles.Include(t => t.Tags).ToListAsync();
        }

        public async Task<NewsArticle?> GetNewsById(string newsArticleId)
        {
            return await _context.NewsArticles
            .Include(t => t.Tags)  // Load danh sách Tags khi lấy bài viết
            .FirstOrDefaultAsync(n => n.NewsArticleId == newsArticleId);
        }

        public async Task AddNews(NewsArticle newsArticle)
        {
            await _context.NewsArticles.AddAsync(newsArticle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNews(NewsArticle newsArticle)
        {
            _context.NewsArticles.Update(newsArticle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNews(string newsArticleId)
        {
            var newsArticle = await _context.NewsArticles.FindAsync(newsArticleId);
            if (newsArticle != null)
            {
                _context.NewsArticles.Remove(newsArticle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
