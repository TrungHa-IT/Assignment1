using ASS1.Models;
using ASS1.Repositories;
using ASS1.Services;

public class NewsArticleServices : INewsArticleServices
{
    private readonly INewsArticleRepository _newsArticleRepository;

    public NewsArticleServices(INewsArticleRepository newsArticleRepository)
    {
        _newsArticleRepository = newsArticleRepository;
    }

    public  Task<IQueryable<NewsArticle>> GetAllNews()
    {
        return _newsArticleRepository.GetAllNews();
    }

    public async Task<NewsArticle?> GetNewsById(string newsArticleId)
    {
        return await _newsArticleRepository.GetNewsById(newsArticleId);
    }

    public async Task AddNews(NewsArticle newsArticle)
    {
        await _newsArticleRepository.AddNews(newsArticle);
    }

    public async Task UpdateNews(NewsArticle newsArticle)
    {
        await _newsArticleRepository.UpdateNews(newsArticle);
    }

    public async Task DeleteNews(string newsArticleId)
    {
        await _newsArticleRepository.DeleteNews(newsArticleId);
    }

    public async Task AddNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds)
    {
        tagIds ??= new List<int>();
        await _newsArticleRepository.AddNewsArticleWithTagsAsync(article, tagIds);
    }

    public async Task<IEnumerable<NewsArticle>> GetAllNewsStatus()
    {
        return await _newsArticleRepository.GetAllNewsStatus();
    }

    public async Task<IEnumerable<NewsArticle>> GetNewsByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _newsArticleRepository.GetNewsByDateRange(startDate, endDate); 
    }

    public async Task UpdateNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds)
    {
        await _newsArticleRepository.UpdateNewsArticleWithTagsAsync(article, tagIds);
    }
}
