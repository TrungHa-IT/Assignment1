using ASS1.Models;

namespace ASS1.DAO
{
    public interface INewsArticleDAO
    {
        Task<IEnumerable<NewsArticle>> GetAllNews();
        Task<NewsArticle?> GetNewsById(string newsArticleId);
        Task AddNews(NewsArticle newsArticle);
        Task UpdateNews(NewsArticle newsArticle);
        Task DeleteNews(string newsArticleId);
    }
}
