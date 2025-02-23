using ASS1.DAO;
using ASS1.Models;

namespace ASS1.Services
{
    public interface INewsArticleServices
    {
        Task<IEnumerable<NewsArticle>> GetAllNews();
        Task<NewsArticle?> GetNewsById(string newsArticleId);
        Task AddNews(NewsArticle newsArticle);
        Task UpdateNews(NewsArticle newsArticle);
        Task DeleteNews(string newsArticleId);
        Task AddNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds);
    }
}
