using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories.Implementations
{
    public class WorkDayRepository : IWorkDayRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор
        public WorkDayRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Получение всех рабочих дней
        public async Task<List<WorkDay>> GetAll() => await _context.WorkDays.ToListAsync();

        // Получение рабочего дня по id
        public async Task<WorkDay> GetById(int id) => await _context.WorkDays.SingleOrDefaultAsync(x => x.Id == id);

        // Получение рабочих дней по id рабочего места
        public async Task<List<WorkDay>> GetByPlaseOfWorkId(int plaseOfWorkId) => await _context.WorkDays.Where(x => x.PlaseOfWorkId == plaseOfWorkId).ToListAsync();

        // Получение рабочих дней по id расчетного листка
        public Task<List<WorkDay>> GetByBillingStatementId(int billingStatementId) => _context.WorkDays.Where(x => x.BillingStatementId == billingStatementId).ToListAsync();

        // Получение рабочего дня по дате
        public async Task<WorkDay> GetByDate(DateTime date) => await _context.WorkDays.SingleOrDefaultAsync(x => x.Date == date);

        // Получение рабочих дней по продолжительности сменны
        public async Task<List<WorkDay>> GetByTotalWorkHours(int totalWorkHours) => await _context.WorkDays.Where(x => x.TotalWorkHours == totalWorkHours).ToListAsync();
       
        // Добавление рабочего дня
        public async Task Add(WorkDay workDayToAdd)
        {
            await _context.AddAsync(workDayToAdd);
            await _context.SaveChangesAsync();
        }

        // Обновление рабочего дня
        public async Task Update(WorkDay workDayToUpdate)
        {
            _context.WorkDays.Update(workDayToUpdate);
            await _context.SaveChangesAsync();
        }

        // Удаление рабочего дня
        public async Task Delete(WorkDay workDayToDelete)
        {
            _context.Remove(workDayToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
