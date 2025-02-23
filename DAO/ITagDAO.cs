using ASS1.Models;

namespace ASS1.DAO
{
    public interface ITagDAO
    {
        Task<IEnumerable<Tag>> GetAllTags();
        Task<Tag?> GetTagById(int tagId);
        Task AddTag(Tag tag);
        Task UpdateTag(Tag tag);
        Task DeleteTag(int tagId);
    }
}
