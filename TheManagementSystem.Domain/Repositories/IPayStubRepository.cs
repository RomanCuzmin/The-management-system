using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IPayStubRepository
    {
        public Task<List<PayStub>> GetAllPayStubsAsync();
        public Task<PayStub> GetPayStubByIdAsync(int id);
        public Task<List<PayStub>> GetPayStubsByAccountIdAsync(int accountId);
        public Task<List<PayStub>> GetPayStubsByTransactionIdAsync(int transactionId);
        public Task<List<PayStub>> GetPayStubsByCategoryIdAsync(int categoryId);
        public Task<List<PayStub>> GetPayStubsByPlaceOfWorkIdAsync(int plaseOfWorkId);
        public Task<List<PayStub>> GetPayStubsByUserIdAsync(int userId);
        public Task<PayStub> GetPayStubByIdAndUserIdAsync(int id, int userId);
        public Task<List<PayStub>> GetPayStubsByAccountIdAndUserIdAsync(int accountId, int userId);
        public Task<List<PayStub>> GetPayStubsByTransactionIdAndUserIdAsync(int transactionId, int userId);
        public Task<List<PayStub>> GetPayStubsByCategoryIdAndUserIdAsync(int categoryId, int userId);
        public Task<List<PayStub>> GetPayStubsByPlaceOfWorkIdAndUserIdAsync(int plaseOfWorkId, int userId);
        public Task<List<PayStub>> GetAllPayStubsWithDetailsAsync();
        public Task<PayStub> GetPayStubByIdWithDetailsAsync(int id);
        public Task<List<PayStub>> GetPayStubsByAccountIdWithDetailsAsync(int accountId);
        public Task<PayStub> GetPayStubByTransactionIdWithDetailsAsync(int transactionId);
        public Task<List<PayStub>> GetPayStubsByCategoryIdWithDetailsAsync(int categoryId);
        public Task<List<PayStub>> GetPayStubsByPlaceOfWorkIdWithDetailsAsync(int plaseOfWorkId);
        public Task<List<PayStub>> GetPayStubsByUserIdWithDetailsAsync(int userId);
        public Task<PayStub> GetPayStubByIdAndUserIdWithDetailsAsync(int userId ,int id);
        public Task<List<PayStub>> GetPayStubsByAccountIdAndUserIdWithDetailsAsync(int userId, int accountId);
        public Task<PayStub> GetPayStubByTransactionIdAndUserIdWithDetailsAsync(int userId, int transactionId);
        public Task<List<PayStub>> GetPayStubsByCategoryIdAndUserIdWithDetailsAsync(int userId, int categoryId);
        public Task<List<PayStub>> GetPayStubsByPlaceOfWorkIdAndUserIdWithDetailsAsync(int userId, int plaseOfWorkId);
        public Task AddPayStubAsync(PayStub payStubToAdd);
        public void UpdatePayStub(PayStub payStubToUpdate);
        public void DeletePayStub(PayStub payStubToDelete);
        public Task AddPayStubAndSaveAsync(PayStub payStubToAdd);
        public Task UpdatePayStubAndSaveAsync(PayStub payStubToUpdate);
        public Task DeletePayStubAndSaveAsync(PayStub payStubToRemove);
    }
}
