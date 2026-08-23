using Microsoft.EntityFrameworkCore;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор репозитория BudgetRepository
        public BudgetRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
        

        #region Методы для получения бюджетов
        // Асинхронный метод для получения всех бюджетов из базы данных
        public async Task<List<Budget>> GetAllBudgetsAsync() => await _context.Budgets.ToListAsync();
        // Асинхронный метод для получения бюджета по id из базы данных
        public async Task<Budget?> GetBudgetByIdAsync(int id) => await _context.Budgets.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения бюджета по id категории из базы данных
        public async Task<List<Budget>> GetBudgetsByCategoryIdAsync(int categoryId) => await _context.Budgets.Where(x => x.CategoryId == categoryId).ToListAsync();
        // Асинхронный метод для получения бюджетов по запланированной сумме из базы данных
        public async Task<List<Budget>> GetBudgetsByPlannedAmountAsync(decimal plannedAmount) => await _context.Budgets.Where(x => x.PlannedAmount == plannedAmount).ToListAsync();
        // Асинхронный метод для получения бюджетов по дате начала из базы данных
        public async Task<List<Budget>> GetBudgetsByStartDateAsync(DateOnly startDate) => await _context.Budgets.Where(x =>x.StartDate == startDate).ToListAsync();
        // Асинхронный метод для получения бюджетов по дате окончания из базы данных 
        public async Task<List<Budget>> GetBudgetsByEndDateAsync(DateOnly endDate) => await _context.Budgets.Where(x => x.EndDate == endDate).ToListAsync();
        // Асинхронный метод для получения бюджетов по дате  начала и окончания из базы данных
        public async Task<List<Budget>> GetBudgetsByStartAndEndDateAsync(DateOnly startDate, DateOnly endDate) => await _context.Budgets.Where(x =>x.StartDate == startDate && x.EndDate == endDate).ToListAsync();
        #endregion
        #region Методы для получения бюджетов пользователя 
        // Асинхронный метод для получения всех бюджетов пользователя из базы данных 
        public Task<List<Budget>> GetBudgetsByUserIdAsync(int userId) => _context.Budgets.Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по id из базы данных
        public async Task<Budget?> GetBudgetByIdAndUserIdAsync(int id, int userId) => await _context.Budgets.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения бюджетов пользователя по id категории из базы данных
        public async Task<List<Budget>> GetBudgetsByCategoryIdAndUserIdAsync(int categoryId, int userId) => await _context.Budgets.Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по запланированной сумме из базы данных
        public async Task<List<Budget>> GetBudgetsByPlannedAmountAndUserIdAsync(decimal plannedAmount, int userId) => await _context.Budgets.Where(x => x.PlannedAmount == plannedAmount && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по дате начала из базы данных
        public async Task<List<Budget>> GetBudgetsByStartDateAndUserIdAsync(DateOnly startDate, int useId) => await _context.Budgets.Where(x => x.StartDate == startDate && x.UserId == useId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по дате окончания из базы данных
        public async Task<List<Budget>> GetBudgetsByEndDateAndUserIdAsync(DateOnly endDate, int userId) => await _context.Budgets.Where(x => x.EndDate == endDate && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по дате начала и окончания из базы данных
        public async Task<List<Budget>> GetBudgetsByStartAndEndDateAndUserIdAsync(DateOnly startDate, DateOnly endDate, int userId) => await _context.Budgets.Where(x => x.StartDate == startDate && x.EndDate == endDate && x.UserId == userId).ToListAsync();
        #endregion
        #region Методы для получения бюджетов с связанными данными 
        // Асинхронный метод для получения всех бюджетов с связанными данными из базы данных 
        public async Task<List<Budget>> GetAllBudgetsWithDetailsAsync() => await _context.Budgets.Include(x => x.Category).ToListAsync();
        // Асинхронный метод для получения бюджета по id бюджета с связанными данными из базы данных
        public async Task<Budget?> GetBudgetByIdWithDetailsAsync(int id) => await _context.Budgets.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения бюджетов по id категории с связанными данными из базы данных
        public async Task<List<Budget>> GetBudgetsByCategoryIdWithDetailsAsync(int categoryId) => await _context.Budgets.Include(x => x.Category).Where(x => x.CategoryId == categoryId).ToListAsync();
        // Асинхронный метод для получения бюджетов по дате начала с связанными данными из базы данных  
        public async Task<List<Budget>> GetBudgetsByStartDateWithDetailsAsync(DateOnly startDate) => await _context.Budgets.Include(x => x.Category).Where(x => x.StartDate == startDate).ToListAsync();
        // Асинхронный метод для получения бюджетов по дате окончания с связанными данными из базы данных
        public async Task<List<Budget>> GetBudgetsByEndDateWithDetailsAsync(DateOnly endDate) => await _context.Budgets.Include(x => x.Category).Where(x => x.EndDate == endDate).ToListAsync();
        // Асинхронный метод для получения бюджетов по дате начала и окончания с связанными данными из базы данных
        public async Task<List<Budget>> GetBudgetsByStartAndEndDateWithDetailsAsync(DateOnly startDate, DateOnly endDate) => await _context.Budgets.Include(x => x.Category).Where(x => x.StartDate == startDate && x.EndDate == endDate).ToListAsync();
        // Асинхронный метод для получения бюджетов по запланированной сумме с связанными данными из базы данных
        public async Task<List<Budget>> GetBudgetsByPlannedAmountWithDetailsAsync(decimal plannedAmount) => await _context.Budgets.Include(x => x.Category).Where(x => x.PlannedAmount == plannedAmount).ToListAsync();
        #endregion
        #region Методы для получения бюджетов пользователя с связанными данными
        // Асинхронный метод для получения всех бюджетов пользователя с связанными данными из базы данных 
        public async Task<List<Budget>> GetBudgetsByUserIdWithDetailsAsync(int userId) => await _context.Budgets.Include(x => x.Category).Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджета пользователя с связанными данными из базы данных
        public async Task<Budget?> GetBudgetByIdAndUserIdWithDetailsAsync(int id, int userId) => await _context.Budgets.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения бюджетов пользователя по id категории с связанными данными из базы данных 
        public async Task<List<Budget>> GetBudgetsByCategoryIdAndUserIdWithDetailsAsync(int categoryId, int userId) => await _context.Budgets.Include(x => x.Category).Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по дате начала с связанными данными из базы данных
        public async Task<List<Budget>> GetBudgetsByStartDateAndUserIdWithDetailsAsync(DateOnly startDate, int userId) => await  _context.Budgets.Include(x => x.Category).Where(x => x.StartDate == startDate && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по дате окончания с связанными данными из базы данных
        public async Task<List<Budget>> GetBudgetsByEndDateAndUserIdWithDetailsAsync(DateOnly endDate, int userId) => await _context.Budgets.Include(x => x.Category).Where(x => x.EndDate == endDate && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по дате начала и окончания с связанными данными из базы данных
        public async Task<List<Budget>> GetBudgetsByStartAndEndDateAndUserIdWithDetailsAsync(DateOnly startDate, DateOnly endDate, int userId) => await _context.Budgets.Include(x => x.Category).Where(x => x.StartDate == startDate && x.EndDate == endDate && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения бюджетов пользователя по дате запланированной сумме из базы данных
        public async Task<List<Budget>> GetBudgetsByPlannedAmountAndUserIdWithDetailsAsync(decimal plannedAmount, int userId) => await _context.Budgets.Include(x => x.Category).Where(x => x.PlannedAmount == plannedAmount && x.UserId == userId).ToListAsync();
        #endregion
        #region Методы для добавления, обновления или удаления бюджета с сохранением или без
        // Асинхронный метод для добавления бюджета в базу данных без сохранения
        public async Task AddBudgetAsync(Budget budgetToAdd) => await _context.Budgets.AddAsync(budgetToAdd);
        // Метод для обновления бюджета в базе данных без сохранения
        public void UpdateBudget(Budget budgetToUpdate) => _context.Budgets.Update(budgetToUpdate);
        // Метод для удаления бюджета из базы данных без сохранения 
        public void DeleteBudget(Budget budgetToDelete) => _context.Budgets.Remove(budgetToDelete);

        // Асинхронный метод для добавления бюджета в базу данных с сохранением
        public async Task AddBudgetAndSaveAsync(Budget budgetToAdd)
        {
            await _context.Budgets.AddAsync(budgetToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления бюджета в базе данных с сохранением
        public async Task UpdateBudgetAndSaveAsync(Budget budgetToUpdate)
        {
            _context.Budgets.Update(budgetToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления бюджета в базе данных с сохранением
        public async Task DeleteBudgetAndSaveAsync(Budget budgetToDelete)
        {
            _context.Budgets.Remove(budgetToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
