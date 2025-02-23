using ASS1.DAO;
using ASS1.Models;

namespace ASS1.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ITagDAO _tagDAO;
        public TagRepository(ITagDAO tagDAO)
        {
            _tagDAO = tagDAO;
        }
        public async Task AddTag(Tag tag)
        {
            await _tagDAO.AddTag(tag);
        }

        public async Task DeleteTag(int tagId)
        {
            await _tagDAO.DeleteTag(tagId);
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _tagDAO.GetAllTags();
        }

        public async Task<Tag?> GetTagById(int tagId)
        {
            return await _tagDAO.GetTagById(tagId);
        }

        public async Task UpdateTag(Tag tag)
        {
            await _tagDAO.UpdateTag(tag);
        }
    }
}
