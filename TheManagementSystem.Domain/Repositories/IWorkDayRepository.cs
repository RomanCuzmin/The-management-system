using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IWorkDayRepository
    {
        public Task<List<WorkDay>> GetAll();
        public Task<WorkDay> GetById(int id);
        public Task<List<WorkDay>> GetByWorkerId(int workerId);
        public Task<List<WorkDay>> GetByBillingStatementId(int billingStatementId);
        public Task<WorkDay> GetByDate(DateTime date);
        public Task<List<WorkDay>> GetByTotalWorkHours(int totalWorkHours);
        public Task Add(WorkDay workDayToAdd);
        public Task Update(WorkDay workDayToUpdate);
        public Task Delete(WorkDay workDayToDelete);
    }
}
