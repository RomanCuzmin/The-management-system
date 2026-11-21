using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories;

public class WorkDayRepository : IWorkDayRepository
{
    private readonly ApplicationDbContext _context;

    // Конструктор репозитория WorkDayRepository
    public WorkDayRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
    #region Методы для получения рабочих дней    
    // Асинхронный метод для получения всех рабочих дней
    public async Task<List<WorkDay>> GetAllWorkDaysAsync() => await _context.WorkDays.ToListAsync();
    // Асинхронный метод для получения рабочего дня по id
    public async Task<WorkDay?> GetWorkDayByIdAsync(int id) => await _context.WorkDays.SingleOrDefaultAsync(x => x.Id == id);

    // Асинхронный метод для получения рабочих дней по id места работы
    public async Task<List<WorkDay>> GetWorkDaysByPlaceOfWorkIdAsync(int plaseOfWorkId) => await _context.WorkDays.Where(x => x.PlaseOfWorkId == plaseOfWorkId).ToListAsync();

    // Асинхронный метод для получения рабочего дня по id места работы
    public Task<List<WorkDay>> GetWorkDaysByPayStubIdAsync(int billingStatementId) => _context.WorkDays.Where(x => x.BillingStatementId == billingStatementId).ToListAsync();
    // Асинхронный метод для получения рабочего дня по дате
    public async Task<List<WorkDay>> GetWorkDaysByDateAsync(DateOnly date) => await _context.WorkDays.Where(x => x.Date == date).ToListAsync();
    // Асинхронный метод для получения рабочего дня по продолжительности сменны
    public async Task<List<WorkDay>> GetWorkDaysByTotalWorkHoursAsync(int totalWorkHours) => await _context.WorkDays.Where(x => x.TotalWorkHours == totalWorkHours).ToListAsync();
    #endregion
    #region Методы для получения рабочих дней пользователя
    // Асинхронный метод для получения всех рабочих дней пользователя 
    public async Task<List<WorkDay>> GetWorkDaysByUserIdAsync(int userId) => await _context.WorkDays.Where(x =>x.UserId == userId).ToListAsync();
    // Асинхронный метод для получения рабочего дня пользователя по id
    public async Task<WorkDay?> GetWorkDayByIdAndUserIdAsync(int id, int userId) => await _context.WorkDays.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    // Асинхронный метод для получения рабочего дня пользователя по id места работы
    public async Task<List<WorkDay>> GetWorkDaysByPlaceOfWorkIdAndUserIdAsync(int plaseOfWorkId, int userId) => await _context.WorkDays.Where(x => x.PlaseOfWorkId == plaseOfWorkId && x.UserId == userId).ToListAsync();
    // Асинхронный метод для получения рабочих дней пользователя по id расчетного листа 
    public async Task<List<WorkDay>> GetWorkDaysByPayStubIdAndUserIdAsync(int billingStatementId, int userId) => await _context.WorkDays.Where(x => x.BillingStatementId == billingStatementId && x.UserId == userId).ToListAsync();
    // Асинхронный метод для получения рабочих дней пользователя по дате
    public async Task<List<WorkDay>> GetWorkDaysByDateAndUserIdAsync(DateOnly date, int userId) => await _context.WorkDays.Where(x => x.Date == date && x.UserId == userId).ToListAsync();
    // Асинхронный метод для получения рабочих дней пользователя по продолжительности сменны
    public async Task<List<WorkDay>> GetWorkDaysByTotalWorkHoursAndUserIdAsync(int totalWorkHours, int userId) => await _context.WorkDays.Where(x => x.TotalWorkHours == totalWorkHours && x.UserId == userId).ToListAsync();
    #endregion
    #region Методы для получения рабочих дней с загруженными связанными данными
    // Асинхронный метод для получения всех рабочих дней с загруженными связанными данными
    public async Task<List<WorkDay>> GetAllWorkDaysWithDetailsAsync() => await _context.WorkDays
                                                                          .Include(x => x.PlaseOfWork)
                                                                          .Include(x => x.PayStubs)
                                                                          .ToListAsync();
    // Асинхронный метод для получения рабочего дня по id с загруженными связанными данными
    public async Task<WorkDay?> GetWorkDayByIdWithDetailsAsync(int id) => await _context.WorkDays
                                                                            .Include(x => x.PlaseOfWork)
                                                                            .Include(x => x.PayStubs)
                                                                            .SingleOrDefaultAsync(x => x.Id == id);
    // Асинхронный метод для получения рабочих дней по id места работы с загруженными связанными данными
    public async Task<List<WorkDay>> GetWorkDaysByPlaceOfWorkIdWithDetailsAsync(int plaseOfWorkId) => await _context.WorkDays
                                                                                                       .Include(x => x.PlaseOfWork)
                                                                                                       .Include(x => x.PayStubs)
                                                                                                       .Where(x => x.PlaseOfWorkId  == plaseOfWorkId)
                                                                                                       .ToListAsync();
    // Асинхронный метод для получения рабочих дней по id расчетного листа с загруженными связанными данными
    public async Task<List<WorkDay>> GetWorkDaysByPayStubIdWithDetailsAsync(int billingStatementId) => await _context.WorkDays
                                                                                                                 .Include(x => x.PlaseOfWork)
                                                                                                                 .Include(x => x.PayStubs)
                                                                                                                 .Where(x => x.BillingStatementId == billingStatementId)
                                                                                                                 .ToListAsync();
    // Асинхронный метод для получения рабочих дней по дате с загруженными связанными данными
    public async Task<List<WorkDay>> GetWorkDaysByDateWithDetailsAsync(DateOnly date) => await _context.WorkDays
                                                                                          .Include(x => x.PlaseOfWork)
                                                                                          .Include(x => x.PayStubs)
                                                                                          .Where(x => x.Date == date)
                                                                                          .ToListAsync();
    // Асинхронный метод для получения рабочих дней по дате с загруженными связанными данными
    public async Task<List<WorkDay>> GetWorkDaysByDateAndUserIdWithDetailsAsync(DateOnly date, int userId) => await _context.WorkDays
                                                                                                               .Include(x => x.PlaseOfWork)
                                                                                                               .Include(x => x.PayStubs)
                                                                                                               .Where(x => x.Date == date && x.UserId == userId)
                                                                                                               .ToListAsync();
    // Асинхронный метод для получения рабочих дней с загруженными связанными данными 
    public async Task<List<WorkDay>> GetWorkDaysByTotalWorkHoursWithDetailsAsync(int totalWorkHours) => await _context.WorkDays
                                                                                                         .Include(x => x.PlaseOfWork)
                                                                                                         .Include(x => x.PayStubs)
                                                                                                         .Where(x => x.TotalWorkHours == totalWorkHours)
                                                                                                         .ToListAsync();
    #endregion
    #region Методы для получения рабочих дней пользователя с загруженными связанными данными
    // Асинхронный метод для получения всех рабочих дней пользователя с загруженными связанными данными
    public async Task<List<WorkDay>> GetWorkDaysByUserIdWithDetailsAsync(int userId) => await _context.WorkDays
                                                                                         .Include(x => x.PlaseOfWork)
                                                                                         .Include(x => x.PayStubs)
                                                                                         .Where(x => x.UserId == userId)
                                                                                         .ToListAsync();
    // Асинхронный метод для получения рабочего дня пользователя по id с загруженными связанными данными 
    public async Task<WorkDay?> GetWorkDayByIdAndUserIdWithDetailsAsync(int id, int userId) => await _context.WorkDays
                                                                                                .Include(x => x.PlaseOfWork)
                                                                                                .Include(x => x.PayStubs)
                                                                                                .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    // Асинхронный метод для получения рабочих дней пользователя по id места работы с загруженными связанными данными                                                                                           
    public async Task<List<WorkDay>> GetWorkDaysByPlaceOfWorkIdAndUserIdWithDetailsAsync(int plaseOfWorkId, int userId) => await _context.WorkDays
                                                                                                                            .Include(x => x.PlaseOfWork)
                                                                                                                            .Include (x => x.PayStubs)
                                                                                                                            .Where(x => x.PlaseOfWorkId == plaseOfWorkId && x.UserId == userId)
                                                                                                                            .ToListAsync();
    // Асинхронный метод для получения рабочих дней пользователя по id расчетного листа с загруженными связанными данными
    public async Task<List<WorkDay>> GetWorkDaysByBillingStatementIdAndUserIdWithDetailsAsync(int billingStatementId, int userId) => await _context.WorkDays
                                                                                                                                     .Include(x => x.PlaseOfWork)
                                                                                                                                     .Include(x => x.PayStubs)
                                                                                                                                     .Where(x => x.BillingStatementId == billingStatementId && x.UserId == userId)
                                                                                                                                     .ToListAsync();
    // Асинхронный метод для получения рабочих дней пользователя по продолжительности смены с загруженными связанными данными
    public async Task<List<WorkDay>> GetWorkDaysByTotalWorkHoursAndUserIdWithDetailsAsync(int totalWorkHours, int userId) => await _context.WorkDays
                                                                                                                              .Include(x => x.PlaseOfWork)
                                                                                                                              .Include(x => x.PayStubs)
                                                                                                                              .Where(x => x.TotalWorkHours == totalWorkHours && x.UserId == userId)
                                                                                                                              .ToListAsync();
    #endregion
    #region Методы для добавления, обновления или удаления рабочего дня с сохранением или без
    // Асинхронный метод для добавления рабочего дня без сохранения
    public async Task AddWorkDayAsync(WorkDay workDayToAdd) => await _context.WorkDays.AddAsync(workDayToAdd);
    // Метод для обновления рабочего дня без сохранения
    public void UpdateWorkDay(WorkDay workDayToUpdate) => _context.WorkDays.Update(workDayToUpdate);
    // Метод для удаления рабочего дня без сохранения
    public void DeleteWorkDay(WorkDay workDayToRemove) => _context.WorkDays.Remove(workDayToRemove);

    // Асинхронный метод для добавления рабочего дня с сохранением
    public async Task AddWorkDayAndSaveAsync(WorkDay workDayToAdd)
    {
        await _context.AddAsync(workDayToAdd);
        await _context.SaveChangesAsync();
    }

    // Асинхронный метод для обновления рабочего дня пользователя с сохранением
    public async Task UpdateWorkDayAndSaveAsync(WorkDay workDayToUpdate)
    {
        _context.WorkDays.Update(workDayToUpdate);
        await _context.SaveChangesAsync();
    }

    // Асинхронный метод для удаления рабочего дня с сохранением
    public async Task DeleteWorkDayAndSaveAsync(WorkDay workDayToDelete)
    {
        _context.Remove(workDayToDelete);
        await _context.SaveChangesAsync();
    }
    #endregion
}
