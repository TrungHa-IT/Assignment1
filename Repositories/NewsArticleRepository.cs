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
            var tags = new List<Tag>();
            foreach (var tagId in tagIds)
            {
                var tag = await _tagDAO.GetTagById(tagId);
                if (tag != null)
                {
                    tags.Add(tag);
                }
            }

            article.Tags = tags;

            await _newsArticleDAO.AddNews(article);
        }

        public async Task DeleteNews(string newsArticleId)
        {
            var article = await _newsArticleDAO.GetNewsById(newsArticleId);
            var listTags = new List<Tag>();
            listTags = _tagDAO.GetAllTags().Result.ToList();

            foreach (var tag in listTags)
            {
                if (article.Tags.Contains(tag))
                {
                    article.Tags.Remove(tag);
                }
            }
            await _newsArticleDAO.DeleteNews(newsArticleId);
        }

        public async Task<IQueryable<NewsArticle>> GetAllNews()
        {
            return await _newsArticleDAO.GetAllNews();
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNewsStatus()
        {
            return await _newsArticleDAO.GetAllNewsStatus();
        }

        public async Task<IEnumerable<NewsArticle>> GetNewsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _newsArticleDAO.GetNewsByDateRange(startDate, endDate);
        }

        public async Task<NewsArticle?> GetNewsById(string newsArticleId)
        {
            return await _newsArticleDAO.GetNewsById(newsArticleId);
        }

        public async Task UpdateNews(NewsArticle newsArticle)
        {
            await _newsArticleDAO.UpdateNews(newsArticle);
        }

        public async Task UpdateNewsArticleWithTagsAsync(NewsArticle article, List<int> tagIds)
        {
            var tags = new List<Tag>();
            foreach (var tagId in tagIds)
            {
                var tag = await _tagDAO.GetTagById(tagId);
                if (tag != null)
                {
                    tags.Add(tag);
                }
            }

            var listTags = new List<Tag>();
            listTags = _tagDAO.GetAllTags().Result.ToList();

            foreach (var tag in listTags)
            {
                if (article.Tags.Contains(tag))
                {
                    article.Tags.Remove(tag);
                }
            }

            // Thêm các tag mới vào bài viết nếu chưa có
            foreach (var tag in tags)
            {
                if (!article.Tags.Contains(tag))
                {
                    article.Tags.Add(tag);
                }
            }

            // Cập nhật bài viết
            await _newsArticleDAO.UpdateNews(article);
        }


    }
}
