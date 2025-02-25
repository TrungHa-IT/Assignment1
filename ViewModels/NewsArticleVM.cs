using ASS1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ASS1.ViewModels
{
    public class NewsArticleViewModel
    {
        public string? NewsArticleId { get; set; }
        [Required(ErrorMessage = "Please enter the news title")]
        public string? NewsTitle { get; set; }
        [Required(ErrorMessage = "Please enter the news Headline")]
        public string Headline { get; set; } = null!;
        public string? NewsContent { get; set; }
        public string? NewsSource { get; set; }
        public bool? NewsStatus { get; set; }
        [Required(ErrorMessage = "Please select TagID")]
        public List<int> SelectedTagIds { get; set; } = new();
        [Required(ErrorMessage = "Please select CategoryID")]
        public int? SelectedTagCategoryID { get; set; } = new();
        public List<Category>? Categories { get; set; } = new();
        public List<Tag>? Tags { get; set; } = new();
    }
}
