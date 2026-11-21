using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IWorkDayRepository
    {
        public Task<List<WorkDay>> GetAllWorkDaysAsync();
        public Task<WorkDay?> GetWorkDayByIdAsync(int id);
        public Task<List<WorkDay>> GetWorkDaysByPlaceOfWorkIdAsync(int plaseOfWorkId);
        public Task<List<WorkDay>> GetWorkDaysByPayStubIdAsync(int billingStatementId);
        public Task<List<WorkDay>> GetWorkDaysByDateAsync(DateOnly date);
        public Task<List<WorkDay>> GetWorkDaysByDateAndUserIdAsync(DateOnly date, int userId);
        public Task<List<WorkDay>> GetWorkDaysByTotalWorkHoursAsync(int totalWorkHours);
        public Task<List<WorkDay>> GetWorkDaysByUserIdAsync(int userId);
        public Task<WorkDay?> GetWorkDayByIdAndUserIdAsync(int id, int userId);
        public Task<List<WorkDay>> GetWorkDaysByPlaceOfWorkIdAndUserIdAsync(int plaseOfWorkId, int userId);
        public Task<List<WorkDay>> GetWorkDaysByPayStubIdAndUserIdAsync(int billingStatementId, int userId);
        public Task<List<WorkDay>> GetWorkDaysByTotalWorkHoursAndUserIdAsync(int totalWorkHours, int userId);
        public Task<List<WorkDay>> GetAllWorkDaysWithDetailsAsync();
        public Task<WorkDay?> GetWorkDayByIdWithDetailsAsync(int id);
        public Task<List<WorkDay>> GetWorkDaysByPlaceOfWorkIdWithDetailsAsync(int plaseOfWorkId);
        public Task<List<WorkDay>> GetWorkDaysByPayStubIdWithDetailsAsync(int billingStatementId);
        public Task<List<WorkDay>> GetWorkDaysByDateWithDetailsAsync(DateOnly date);
        public Task<List<WorkDay>> GetWorkDaysByDateAndUserIdWithDetailsAsync(DateOnly date, int userId);
        public Task<List<WorkDay>> GetWorkDaysByTotalWorkHoursWithDetailsAsync(int totalWorkHours);
        public Task<List<WorkDay>> GetWorkDaysByUserIdWithDetailsAsync(int userId);
        public Task<WorkDay?> GetWorkDayByIdAndUserIdWithDetailsAsync(int id, int userId);
        public Task<List<WorkDay>> GetWorkDaysByPlaceOfWorkIdAndUserIdWithDetailsAsync(int plaseOfWorkId, int userId);
        public Task<List<WorkDay>> GetWorkDaysByBillingStatementIdAndUserIdWithDetailsAsync(int billingStatementId, int userId);
        public Task<List<WorkDay>> GetWorkDaysByTotalWorkHoursAndUserIdWithDetailsAsync(int totalWorkHours, int userId);
        public Task AddWorkDayAsync(WorkDay workDayToAdd);
        public void UpdateWorkDay(WorkDay workDayToUpdate);
        public void DeleteWorkDay(WorkDay workDayToRemove);
        public Task AddWorkDayAndSaveAsync(WorkDay workDayToAdd);
        public Task UpdateWorkDayAndSaveAsync(WorkDay workDayToUpdate);
        public Task DeleteWorkDayAndSaveAsync(WorkDay workDayToDelete);
    }
}
