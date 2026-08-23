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
        public Task<List<WorkDay>> GetByPlaseOfWorkId(int plaseOfWorkId);
        public Task<List<WorkDay>> GetByBillingStatementId(int billingStatementId);
        public Task<List<WorkDay>> GetByDate(DateOnly date);
        public Task<List<WorkDay>> GetByDateAndUserId(DateOnly date, int userId);
        public Task<List<WorkDay>> GetByTotalWorkHours(int totalWorkHours);
        public Task<List<WorkDay>> GetByUserId(int userId);
        public Task<WorkDay> GetByIdAndUserId(int id, int userId);
        public Task<List<WorkDay>> GetByPlaseOfWorkIdAndUserId(int plaseOfWorkId, int userId);
        public Task<List<WorkDay>> GetByBillingStatementIdAndUserId(int billingStatementId, int userId);
        public Task<List<WorkDay>> GetByTotalWorkHoursAndUserId(int totalWorkHours, int userId);
        public Task<List<WorkDay>> GetAllWithDetails();
        public Task<WorkDay> GetByIdWithDetails(int id);
        public Task<List<WorkDay>> GetByPlaseOfWorkIdWithDetails(int plaseOfWorkId);
        public Task<List<WorkDay>> GetByBillingStatementIdWithDetails(int billingStatementId);
        public Task<List<WorkDay>> GetByDateWithDetails(DateOnly date);
        public Task<List<WorkDay>> GetByDateAndUserIdWithDetails(DateOnly date, int userId);
        public Task<List<WorkDay>> GetByTotalWorkHoursWithDetails(int totalWorkHours);
        public Task<List<WorkDay>> GetByUserIdWithDetails(int userId);
        public Task<WorkDay> GetByIdAndUserIdWithDetails(int id, int userId);
        public Task<List<WorkDay>> GetByPlaseOfWorkIdAndUserIdWithDetails(int plaseOfWorkId, int userId);
        public Task<List<WorkDay>> GetByBillingStatementIdAndUserIdWithDetails(int billingStatementId, int userId);
        public Task<List<WorkDay>> GetByTotalWorkHoursAndUserIdWithDetails(int totalWorkHours, int userId);
        public Task Add(WorkDay workDayToAdd);
        public void Update(WorkDay workDayToUpdate);
        public void Delete(WorkDay workDayToRemove);
        public Task AddAndSave(WorkDay workDayToAdd);
        public Task UpdateAndSave(WorkDay workDayToUpdate);
        public Task DeleteAndSave(WorkDay workDayToDelete);
    }
}
