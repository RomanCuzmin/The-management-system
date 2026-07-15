using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories.Implementations
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор
        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получение всех бюджетов
        public async Task<List<Budget>> GetAll() => await _context.Budgets.ToListAsync();

        // Получение бюджета по id 
        public async Task<Budget> GetById(int id) => await _context.Budgets.SingleOrDefaultAsync(x => x.Id == id);

        // Получение бюджетов по id пользователя
        public async Task<List<Budget>> GetByUserId(int userId) => await _context.Budgets.Where(x => x.UserId == userId).ToListAsync();

        // Получение бюджетов по id категории 
        public async Task<List<Budget>> GetByCategoryId(int categoryId) => await _context.Budgets.Where(x => x.CategoryId == categoryId).ToListAsync();

        // Получение бюджетов по дате старта
        public async Task<List<Budget>> GetByStartDate(DateTime startDate) => await _context.Budgets.Where(x => x.StartDate == startDate).ToListAsync();

        // Получение бюджетов по дате окончания
        public async Task<List<Budget>> GetByEndDate(DateTime endDate) => await _context.Budgets.Where(x => x.EndDate == endDate).ToListAsync();

        // Получение бюджетов по дате
        public async Task<List<Budget>> GetByDate(DateTime startDate, DateTime endDate) => await _context.Budgets.Where(x => x.StartDate == startDate && x.EndDate == endDate).ToListAsync();

        // Получение бюджетов по запланированной сумме
        public async Task<List<Budget>> GetByPlannedAmount(int plannedAmount) => await _context.Budgets.Where(x => x.PlannedAmount == plannedAmount).ToListAsync();

        // Добавление бююджета
        public async Task Add(Budget budgetToAdd)
        {
            await _context.Budgets.AddAsync(budgetToAdd);
            await _context.SaveChangesAsync();
        }

        // Обновление бюджета
        public async Task Update(Budget budgetToUpdate)
        {
            _context.Budgets.Update(budgetToUpdate);
            await _context.SaveChangesAsync();
        }

        //Удаление бюджета
        public async Task Delete(Budget budgetToDelete)
        {
            _context.Budgets.Remove(budgetToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
