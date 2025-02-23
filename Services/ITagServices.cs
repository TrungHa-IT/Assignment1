using ASS1.Models;

namespace ASS1.Services
{
    public interface ITagServices
    {
        Task<IEnumerable<Tag>> GetAllTags();
        Task<Tag?> GetTagById(int tagId);
        Task AddTag(Tag tag);
        Task UpdateTag(Tag tag);
        Task DeleteTag(int tagId);
    }
}
