using ASS1.Models;
using ASS1.Repositories;

namespace ASS1.Services
{
    public class TagServices : ITagServices
    {
        private readonly ITagRepository _tagRepository;
        public TagServices(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task AddTag(Tag tag)
        {
            await _tagRepository.AddTag(tag);
        }

        public async Task DeleteTag(int tagId)
        {
            await _tagRepository.DeleteTag(tagId);
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _tagRepository.GetAllTags();
        }

        public async Task<Tag?> GetTagById(int tagId)
        {
            return await _tagRepository.GetTagById(tagId);
        }

        public async Task UpdateTag(Tag tag)
        {
            await _tagRepository.UpdateTag(tag);
        }
    }
}
