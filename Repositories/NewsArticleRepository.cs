using ASS1.DAO;
using ASS1.Models;

namespace ASS1.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly INewsArticleDAO _newsArticleDAO;
        private readonly ITagDAO _tagDAO;
        public NewsArticleRepository(INewsArticleDAO newsArticleDAO, ITagDAO tagDAO)
        {
            _newsArticleDAO = newsArticleDAO;
            _tagDAO = tagDAO;
        }
        public async Task AddNews(NewsArticle newsArticle)
        {
            await _newsArticleDAO.AddNews(newsArticle);
        }

        public async Task AddNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds)
        {
            // Lấy danh sách tag từ database
            var tags = new List<Tag>();
            foreach (var tagId in tagIds)
            {
                var tag = await _tagDAO.GetTagById(tagId);
                if (tag != null)
                {
                    tags.Add(tag);
                }
            }

            // Gán danh sách Tags cho bài viết
            article.Tags = tags;

            // Thêm vào database (EF Core tự chèn vào bảng trung gian)
            await _newsArticleDAO.AddNews(article);
        }

        public async Task DeleteNews(string newsArticleId)
        {
            await _newsArticleDAO.DeleteNews(newsArticleId);
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNews()
        {
            return await _newsArticleDAO.GetAllNews();
        }

        public async Task<NewsArticle?> GetNewsById(string newsArticleId)
        {
            return await _newsArticleDAO.GetNewsById(newsArticleId);
        }

        public async Task UpdateNews(NewsArticle newsArticle)
        {
            await _newsArticleDAO.UpdateNews(newsArticle);
        }
    }
}
