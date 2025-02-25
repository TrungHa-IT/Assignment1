using ASS1.Models;

namespace ASS1.DAO
{
    public interface INewsArticleDAO
    {
        Task<IQueryable<NewsArticle>> GetAllNews();
        Task<IEnumerable<NewsArticle>> GetAllNewsStatus();
        Task<NewsArticle?> GetNewsById(string newsArticleId);
        Task AddNews(NewsArticle newsArticle);
        Task UpdateNews(NewsArticle newsArticle);
        Task DeleteNews(string newsArticleId);
        Task<IEnumerable<NewsArticle>> GetNewsByDateRange(DateTime startDate, DateTime endDate);
    }
}
