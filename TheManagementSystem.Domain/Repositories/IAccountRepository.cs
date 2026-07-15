using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IAccountRepository
    {
        public Task<List<Account>> GetAll();
        public Task<Account> GetById(int id);
        public Task<List<Account>> GetByUserId(int userId);
        public Task<Account> GetByName(string name);
        public Task<List<Account>> GetByCurrency(Currency currency);
        public Task Add(Account accountToAdd);
        public Task Update(Account accountToUpdate);
        public Task Delete(Account accountToDelete);
    }
}
