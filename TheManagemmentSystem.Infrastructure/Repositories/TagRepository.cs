using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор
        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public Task<List<Tag>> GetAllTagsAsync() => _context.Tags.ToListAsync();

        public Task<Tag?> GetTagByIdAsync(int id) => _context.Tags.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<List<Tag>> GetTagsByCategoryIdAsync(int categoryId) => await _context.Tags.Where(x => x.CategoryId == categoryId).ToListAsync();
        public Task<List<Tag>> GetTagsByNameAsync(string name) => _context.Tags.Where(x => x.Name == name).ToListAsync();

        public async Task AddTagAndSaveAsync(Tag tagToAdd)
        {
            await _context.Tags.AddAsync(tagToAdd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTagAndSaveAsync(Tag tagToUpdate)
        {
            _context.Tags.Update(tagToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagAndSaveAsync(Tag tagToDelete)
        {
            _context.Tags.Remove(tagToDelete);
            await _context.SaveChangesAsync();
        }



        public async Task<List<Tag>> GetTagsByUserIdAsync(int userId) => await _context.Tags.Where(x => x.UserId == userId).ToListAsync();

        public async Task<Tag?> GetTagByIdAndUserIdAsync(int id, int userId) => await _context.Tags.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        public async Task<List<Tag>> GetTagsByCategoryIdAndUserIdAsync(int categoryId, int userId) => await _context.Tags.Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        public async Task<Tag?> GetTagByNameAndUserIdAsync(string name, int userId) => await _context.Tags.SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);

        public async Task<List<Tag>> GetAllTagsWithDetailsAsync() => await _context.Tags
                                                                          .Include(x => x.Category)
                                                                          .Include(x => x.Transactions)
                                                                          .ToListAsync();

        public async Task<Tag?> GetTagByIdWithDetailsAsync(int id) => await _context.Tags
                                                                           .Include(x => x.Category)
                                                                           .Include(x => x.Transactions)
                                                                           .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<List<Tag>> GetTagsByCategoryIdWithDetailsAsync(int categoryId) => await _context.Tags
                                                                                                 .Include(x => x.Category)
                                                                                                 .Include(x => x.Transactions)
                                                                                                 .Where(x => x.CategoryId == categoryId)
                                                                                                 .ToListAsync();

        public async Task<List<Tag>> GetTagsByNameWithDetailsAsync(string name) => await _context.Tags
                                                                                        .Include(x => x.Category)
                                                                                        .Include(x => x.Transactions)
                                                                                        .Where(x => x.Name == name)
                                                                                        .ToListAsync();

        public async Task<List<Tag>> GetTagsByUserIdWithDetailsAsync(int userId) => await _context.Tags
                                                                                         .Include(x => x.Category)
                                                                                         .Include(x => x.Transactions)
                                                                                         .Where(x => x.UserId == userId)
                                                                                         .ToListAsync();

        public async Task<Tag?> GetTagByIdAndUserIdWithDetailsAsync(int id, int userId) => await _context.Tags
                                                                                                 .Include(x => x.Category)
                                                                                                 .Include(x => x.Transactions)
                                                                                                 .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        public async Task<List<Tag>> GetByCategoryIdAndUserIdWithDetails(int categoryId, int userId) => await _context.Tags
                                                                                                                      .Include(x => x.Category)
                                                                                                                      .Include(x => x.Transactions)
                                                                                                                      .Where(x => x.CategoryId == categoryId && x.UserId == userId)
                                                                                                                      .ToListAsync();

        public async Task<Tag?> GetTagByNameAndUserIdWithDetailsAsync(string name, int userId) => await _context.Tags
                                                                                                       .Include(x => x.Category)
                                                                                                       .Include(x => x.Transactions)
                                                                                                       .SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        public async Task AddTagAsync(Tag tagToAdd) => await _context.Tags.AddAsync(tagToAdd);

        public void UpdateTag(Tag tagToUpdate) => _context.Tags.Update(tagToUpdate);

        public void DeleteTag(Tag tagToDelete) => _context.Tags.Remove(tagToDelete);
    }
}
