using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Repositories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAll();
        public Task<Category> GetById(int id);
        public Task<Category> GetByName(string name);
        public Task<List<Category>> GetByType(TransactionType type);
        public Task Add(Category categoryToAdd);
        public Task Update(Category categoryToUpdate);
        public Task Delete(Category categoryToDelete);
    }
}
