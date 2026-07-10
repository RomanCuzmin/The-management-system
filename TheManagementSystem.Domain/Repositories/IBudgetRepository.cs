using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IBudgetRepository
    {
        public Task<List<Budget>> GetAll();
        public Task<Budget> GetById(int id);
        public Task<List<Budget>> GetByUserId(int userId);
        public Task<List<Budget>> GetByCategoryId(int categoryId);
        public Task<List<Budget>> GetByPlannedAmount(int plannedAmount);
        public Task<List<Budget>> GetByStartDate(DateTime startDate);
        public Task<List<Budget>> GetByEndDate(DateTime endDate);
        public Task<Budget> GetByDate(DateTime startDate, DateTime endDate);
        public Task Add(Budget budgetToAdd);
        public Task Update(Budget budgetToUpdate);
        public Task Delete(Budget budgetToDelete);
    }
}
