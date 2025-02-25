using ASS1.Models;

namespace ASS1.ViewModels
{
    public class NewsReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<NewsArticle> NewsList { get; set; } = new List<NewsArticle>(); // Đảm bảo không bị null
    }
}
