using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface ITagRepository
    {
        public Task<List<Tag>> GetAllTagsAsync();
        public Task<Tag?> GetTagByIdAsync(int id);
        public Task<List<Tag>> GetTagsByCategoryIdAsync(int categoryId);
        public Task<List<Tag>> GetTagsByNameAsync(string name);
        public Task<List<Tag>> GetTagsByUserIdAsync(int userId);
        public Task<Tag?> GetTagByIdAndUserIdAsync(int id, int userId);
        public Task<List<Tag>> GetTagsByCategoryIdAndUserIdAsync(int categoryId, int userId);
        public Task<Tag?> GetTagByNameAndUserIdAsync(string name, int userId);
        public Task<List<Tag>> GetAllTagsWithDetailsAsync();
        public Task<Tag?> GetTagByIdWithDetailsAsync(int id);
        public Task<List<Tag>> GetTagsByCategoryIdWithDetailsAsync(int categoryId);
        public Task<List<Tag>> GetTagsByNameWithDetailsAsync(string name);
        public Task<List<Tag>> GetTagsByUserIdWithDetailsAsync(int userId);
        public Task<Tag?> GetTagByIdAndUserIdWithDetailsAsync(int id, int userId);
        public Task<List<Tag>> GetTagByCategoryIdAndndUserIdWithDetailsAsync(int categoryId, int userId);
        public Task<Tag?> GetTagByNameAndUserIdWithDetailsAsync(string name, int userId);
        public Task AddTagAsync(Tag tagToAdd);
        public void UpdateTag(Tag tagToUpdate);
        public void DeleteTag(Tag tagToDelete);
        public Task AddTagAndSaveAsync(Tag tagToAdd);
        public Task UpdateTagAndSaveAsync(Tag tagToUpdate);
        public Task DeleteTagAndSaveAsync(Tag tagToDelete);
    }
}

