using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IBillingStatementRepository
    {
        public Task<List<BillingStatement>> CetAll();
        public Task<BillingStatement> GetById(int id);
        public Task<List<BillingStatement>> GetByWorkerId(int workerId);
        public Task<List<BillingStatement>> GetByUserId(int userId);
        public Task<List<BillingStatement>> GetByAccountId(int accountId);
        public Task<List<BillingStatement>> GetByCategoryId(int categoryId);
        public Task<BillingStatement> GetByTransactionId(int transactionId);
        public Task Add(BillingStatement billingStatementToAdd);
        public Task Update(BillingStatement billingStatementToUpdate);
        public Task Delete(BillingStatement billingStatementToRemove);
    }
}
