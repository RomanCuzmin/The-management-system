using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure;

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
    public async Task<List<WorkDay>> GetByDate(DateOnly date) => await _context.WorkDays.Where(x => x.Date == date).ToListAsync();

    // Получение рабочих дней по продолжительности сменны
    public async Task<List<WorkDay>> GetByTotalWorkHours(int totalWorkHours) => await _context.WorkDays.Where(x => x.TotalWorkHours == totalWorkHours).ToListAsync();
   
    // Добавление рабочего дня
    public async Task AddAndSave(WorkDay workDayToAdd)
    {
        await _context.AddAsync(workDayToAdd);
        await _context.SaveChangesAsync();
    }

    // Обновление рабочего дня
    public async Task UpdateAndSave(WorkDay workDayToUpdate)
    {
        _context.WorkDays.Update(workDayToUpdate);
        await _context.SaveChangesAsync();
    }

    // Удаление рабочего дня
    public async Task DeleteAndSave(WorkDay workDayToDelete)
    {
        _context.Remove(workDayToDelete);
        await _context.SaveChangesAsync();
    }

    public Task<List<WorkDay>> GetByDateAndUserId(DateOnly date, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<WorkDay>> GetByUserId(int userId) => await _context.WorkDays.Where(x =>x.UserId == userId).ToListAsync();
    public async Task<WorkDay?> GetByIdAndUserId(int id, int userId) => await _context.WorkDays.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);

    public async Task<List<WorkDay>> GetByPlaseOfWorkIdAndUserId(int plaseOfWorkId, int userId) => await _context.WorkDays.Where(x => x.PlaseOfWorkId == plaseOfWorkId && x.UserId == userId).ToListAsync();

    public async Task<List<WorkDay>> GetByBillingStatementIdAndUserId(int billingStatementId, int userId) => await _context.WorkDays.Where(x => x.BillingStatementId == billingStatementId && x.UserId == userId).ToListAsync();

    public async Task<List<WorkDay>> GetByTotalWorkHoursAndUserId(int totalWorkHours, int userId) => await _context.WorkDays.Where(x => x.TotalWorkHours == totalWorkHours && x.UserId == userId).ToListAsync();
    public async Task<List<WorkDay>> GetAllWithDetails() => await _context.WorkDays
                                                                          .Include(x => x.PlaseOfWork)
                                                                          .Include(x => x.PayStubs)
                                                                          .ToListAsync();

    public async Task<WorkDay?> GetByIdWithDetails(int id) => await _context.WorkDays
                                                                            .Include(x => x.PlaseOfWork)
                                                                            .Include(x => x.PayStubs)
                                                                            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<List<WorkDay>> GetByPlaseOfWorkIdWithDetails(int plaseOfWorkId) => await _context.WorkDays
                                                                                                       .Include(x => x.PlaseOfWork)
                                                                                                       .Include(x => x.PayStubs)
                                                                                                       .Where(x => x.PlaseOfWorkId  == plaseOfWorkId)
                                                                                                       .ToListAsync();
    public async Task<List<WorkDay>> GetByBillingStatementIdWithDetails(int billingStatementId) => await _context.WorkDays
                                                                                                                 .Include(x => x.PlaseOfWork)
                                                                                                                 .Include(x => x.PayStubs)
                                                                                                                 .Where(x => x.BillingStatementId == billingStatementId)
                                                                                                                 .ToListAsync();

    public async Task<List<WorkDay>> GetByDateWithDetails(DateOnly date) => await _context.WorkDays
                                                                                          .Include(x => x.PlaseOfWork)
                                                                                          .Include(x => x.PayStubs)
                                                                                          .Where(x => x.Date == date)
                                                                                          .ToListAsync();
    public async Task<List<WorkDay>> GetByDateAndUserIdWithDetails(DateOnly date, int userId) => await _context.WorkDays
                                                                                                               .Include(x => x.PlaseOfWork)
                                                                                                               .Include(x => x.PayStubs)
                                                                                                               .Where(x => x.Date == date && x.UserId == userId)
                                                                                                               .ToListAsync();
    public async Task<List<WorkDay>> GetByTotalWorkHoursWithDetails(int totalWorkHours) => await _context.WorkDays
                                                                                                         .Include(x => x.PlaseOfWork)
                                                                                                         .Include(x => x.PayStubs)
                                                                                                         .Where(x => x.TotalWorkHours == totalWorkHours)
                                                                                                         .ToListAsync();

    public async Task<List<WorkDay>> GetByUserIdWithDetails(int userId) => await _context.WorkDays
                                                                                         .Include(x => x.PlaseOfWork)
                                                                                         .Include(x => x.PayStubs)
                                                                                         .Where(x => x.UserId == userId)
                                                                                         .ToListAsync();

    public async Task<WorkDay?> GetByIdAndUserIdWithDetails(int id, int userId) => await _context.WorkDays
                                                                                                .Include(x => x.PlaseOfWork)
                                                                                                .Include(x => x.PayStubs)
                                                                                                .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
                                                                                                
    public async Task<List<WorkDay>> GetByPlaseOfWorkIdAndUserIdWithDetails(int plaseOfWorkId, int userId) => await _context.WorkDays
                                                                                                                            .Include(x => x.PlaseOfWork)
                                                                                                                            .Include (x => x.PayStubs)
                                                                                                                            .Where(x => x.PlaseOfWorkId == plaseOfWorkId && x.UserId == userId)
                                                                                                                            .ToListAsync();

    public async Task<List<WorkDay>> GetByBillingStatementIdAndUserIdWithDetails(int billingStatementId, int userId) => await _context.WorkDays
                                                                                                                                     .Include(x => x.PlaseOfWork)
                                                                                                                                     .Include(x => x.PayStubs)
                                                                                                                                     .Where(x => x.BillingStatementId == billingStatementId && x.UserId == userId)
                                                                                                                                     .ToListAsync();
    public async Task<List<WorkDay>> GetByTotalWorkHoursAndUserIdWithDetails(int totalWorkHours, int userId) => await _context.WorkDays
                                                                                                                              .Include(x => x.PlaseOfWork)
                                                                                                                              .Include(x => x.PayStubs)
                                                                                                                              .Where(x => x.TotalWorkHours == totalWorkHours && x.UserId == userId)
                                                                                                                              .ToListAsync();

    public async Task Add(WorkDay workDayToAdd) => await _context.WorkDays.AddAsync(workDayToAdd);

    public void Update(WorkDay workDayToUpdate) => _context.WorkDays.Update(workDayToUpdate);

    public void Delete(WorkDay workDayToRemove) => _context.WorkDays.Remove(workDayToRemove);
}
