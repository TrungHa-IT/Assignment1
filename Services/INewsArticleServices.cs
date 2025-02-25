using ASS1.DAO;
using ASS1.Models;

namespace ASS1.Services
{
    public interface INewsArticleServices
    {
        Task<IQueryable<NewsArticle>> GetAllNews();
        Task<IEnumerable<NewsArticle>> GetAllNewsStatus();
        Task<NewsArticle?> GetNewsById(string newsArticleId);
        Task AddNews(NewsArticle newsArticle);
        Task UpdateNews(NewsArticle newsArticle);
        Task DeleteNews(string newsArticleId);
        Task AddNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds);
        Task<IEnumerable<NewsArticle>> GetNewsByDateRange(DateTime startDate, DateTime endDate);
        Task UpdateNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds);
    }
}
