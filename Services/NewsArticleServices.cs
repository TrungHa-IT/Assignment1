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

    public async Task<IEnumerable<NewsArticle>> GetAllNews()
    {
        return await _newsArticleRepository.GetAllNews();
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
        // Kiểm tra danh sách tagIds có null không
        tagIds ??= new List<int>();

        // Gọi repository để thêm bài viết với danh sách tagIds
        await _newsArticleRepository.AddNewsArticleWithTagsAsync(article, tagIds);
    }
}
