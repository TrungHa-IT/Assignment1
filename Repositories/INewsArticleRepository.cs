using ASS1.Models;

namespace ASS1.Repositories
{
    public interface INewsArticleRepository
    {
        Task<IEnumerable<NewsArticle>> GetAllNews();
        Task<NewsArticle?> GetNewsById(string newsArticleId);
        Task AddNews(NewsArticle newsArticle);
        Task UpdateNews(NewsArticle newsArticle);
        Task DeleteNews(string newsArticleId);
        Task AddNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds);
    }
}
