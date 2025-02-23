using ASS1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASS1.ViewModels
{
    public class NewsArticleViewModel
    {
        public string NewsArticleId { get; set; }
        public string? NewsTitle { get; set; }
        public string Headline { get; set; } = null!;
        public string? NewsContent { get; set; }
        public List<int> SelectedTagIds { get; set; } = new();
        public int? SelectedTagCategoryID { get; set; } = new();

        // Thêm danh sách Categories và Tags
        public List<Category>? Categories { get; set; } = new();
        public List<Tag>? Tags { get; set; } = new();
    }
}
