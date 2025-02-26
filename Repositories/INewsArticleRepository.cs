using ASS1.Models;

namespace ASS1.Repositories
{
    public interface INewsArticleRepository
    {
        Task<IQueryable<NewsArticle>> GetAllNews();
        Task<IQueryable<NewsArticle>> GetAllNewsStatus();
        Task<NewsArticle?> GetNewsById(string newsArticleId);
        Task AddNews(NewsArticle newsArticle);
        Task UpdateNews(NewsArticle newsArticle);
        Task DeleteNews(string newsArticleId);
        Task<IEnumerable<NewsArticle?>> GetNewsByAccountID(short accountID);
        Task AddNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds);
        Task UpdateNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds);
        Task<IEnumerable<NewsArticle>> GetNewsByDateRange(DateTime startDate, DateTime endDate);
    }
}
