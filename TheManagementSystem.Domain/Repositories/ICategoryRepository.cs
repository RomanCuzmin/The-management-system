using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Repositories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAll();
        public Task<Category?> GetById(int id);
        public Task<List<Category>> GetByName(string name);
        public Task<List<Category>> GetByType(TransactionType type);
        public Task<List<Category>> GetByUserId(int userId);
        public Task<Category?> GetByIdAndUserId(int id, int userId);
        public Task<Category?> GetByNameAndUserId(string name, int userId);
        public Task<List<Category>> GetByTypeAndUserId(TransactionType type, int userId);
        public Task<List<Category>> GetAllWithDetails();
        public Task<Category?> GetByIdWithDetails(int id);
        public Task<List<Category>> GetByNameWithDetails(string name);
        public Task<List<Category>> GetByTypeWithDetails(TransactionType type);
        public Task<List<Category>> GetByUserIdWithDetails(int userId);
        public Task<Category?> GetByIdAndUserIdWithDetails(int id, int userId);
        public Task<Category?> GetByNameAndUserIdWithDetails(string name, int userId);
        public Task<List<Category>> GetByTypeAndUserIdWithDetails(TransactionType type, int userId);
        public Task Add(Category categoryToAdd);
        public void Update(Category categoryToUpdate);
        public  void Delete(Category categoryToDelete);
        public Task AddAndSave(Category categoryToAdd);
        public Task UpdateAndSave(Category categoryToUpdate);
        public Task DeleteAndSave(Category categoryToDelete);
    }
}