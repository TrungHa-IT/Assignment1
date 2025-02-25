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

        public async Task<IQueryable<NewsArticle>> GetAllNews()
        {
            return _context.NewsArticles.Include(t => t.Tags);
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

        public async Task<IEnumerable<NewsArticle>> GetAllNewsStatus()
        {
            return await _context.NewsArticles
                     .Where(n => n.NewsStatus == true)
                     .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetNewsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.NewsArticles
            .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
            .OrderByDescending(n => n.CreatedDate)
            .ToListAsync();
        }
    }
}

