using ASS1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ASS1.DAO
{
    public class TagDAO : ITagDAO
    {
        private readonly FunewsManagementContext _context;

        public TagDAO(FunewsManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _context.Tags.Include(t => t.NewsArticles).ToListAsync();
        }

        public async Task<Tag?> GetTagById(int tagId)
        {
            return await _context.Tags.Include(t => t.NewsArticles)
                                      .FirstOrDefaultAsync(t => t.TagId == tagId);
        }

        public async Task AddTag(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTag(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTag(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
