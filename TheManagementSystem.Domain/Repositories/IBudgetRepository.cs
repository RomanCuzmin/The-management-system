using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IBudgetRepository
    {
        public Task<List<Budget>> GetAllBudgetsAsync();
        public Task<Budget> GetBudgetByIdAsync(int id);
        public Task<List<Budget>> GetBudgetsByCategoryIdAsync(int categoryId);
        public Task<List<Budget>> GetBudgetsByPlannedAmountAsync(decimal plannedAmount);
        public Task<List<Budget>> GetBudgetsByEndDateAsync(DateOnly endDate);
        public Task<List<Budget>> GetBudgetsByStartDateAsync(DateOnly startDate);
        public Task<List<Budget>> GetBudgetsByStartAndEndDateAsync(DateOnly startDate ,DateOnly endDate);
        public Task<List<Budget>> GetBudgetsByUserIdAsync(int userId);
        public Task<Budget> GetBudgetByIdAndUserIdAsync(int id, int userId);
        public Task<List<Budget>> GetBudgetsByCategoryIdAndUserIdAsync(int categoryId, int userId);
        public Task<List<Budget>> GetBudgetsByPlannedAmountAndUserIdAsync(decimal plannedAmount, int userId);
        public Task<List<Budget>> GetBudgetsByStartDateAndUserIdAsync(DateOnly startDate, int useId);
        public Task<List<Budget>> GetBudgetsByEndDateAndUserIdAsync(DateOnly endDate, int userId);
        public Task<List<Budget>> GetBudgetsByStartAndEndDateAndUserIdAsync(DateOnly startDate, DateOnly endDate ,int userId);
        public Task<List<Budget>> GetAllBudgetsWithDetailsAsync();
        public Task<Budget> GetBudgetByIdWithDetailsAsync(int id);
        public Task<List<Budget>> GetBudgetsByCategoryIdWithDetailsAsync(int categoryId);
        public Task<List<Budget>> GetBudgetsByPlannedAmountWithDetailsAsync(decimal plannedAmount);
        public Task<List<Budget>> GetBudgetsByStartDateWithDetailsAsync(DateOnly startDate);
        public Task<List<Budget>> GetBudgetsByEndDateWithDetailsAsync(DateOnly endDate);
        public Task<List<Budget>> GetBudgetsByStartAndEndDateWithDetailsAsync(DateOnly startDate, DateOnly endDate);
        public Task<Budget> GetBudgetByIdAndUserIdWithDetailsAsync(int id, int userId);
        public Task<List<Budget>> GetBudgetsByUserIdWithDetailsAsync(int userId);
        public Task<List<Budget>> GetBudgetsByCategoryIdAndUserIdWithDetailsAsync(int categoryId, int userId);
        public Task<List<Budget>> GetBudgetsByPlannedAmountAndUserIdWithDetailsAsync(decimal plannedAmount, int userId);
        public Task<List<Budget>> GetBudgetsByStartDateAndUserIdWithDetailsAsync(DateOnly startDate, int userId);
        public Task<List<Budget>> GetBudgetsByEndDateAndUserIdWithDetailsAsync(DateOnly endDate, int userId);
        public Task<List<Budget>> GetBudgetsByStartAndEndDateAndUserIdWithDetailsAsync(DateOnly startDate, DateOnly endDate, int userId);
        public Task AddBudgetAsync(Budget budgetToAdd);
        public void UpdateBudget(Budget budgetToUpdate);
        public void DeleteBudget(Budget budgetToDelete);
        public Task AddBudgetAndSaveAsync(Budget budgetToAdd);
        public Task UpdateBudgetAndSaveAsync(Budget budgetToUpdate);
        public Task DeleteBudgetAndSaveAsync(Budget budgetToDelete);
    }
}
