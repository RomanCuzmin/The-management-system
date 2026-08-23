using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context; 

        // Конструктор репозитория CategoryRepository
        public CategoryRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        #region Методы для получения категории 
        // Асинхронный метод для получения всех категорий из базы данных
        public async Task<List<Category>> GetAll() => await _context.Categories.ToListAsync();
        // Асинхронный метод для получения категории по id из базы данных
        public async Task<Category?> GetById(int id) => await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения категорий по именни из базы данных
        public Task<List<Category>> GetByName(string name) => _context.Categories.Where(x => x.Name == name).ToListAsync();
        // Асинхронный метод для получения категории по типу из базы данных 
        public Task<List<Category>> GetByType(TransactionType type) => _context.Categories.Where(x => x.Type == type).ToListAsync();
        #endregion
        #region Методы для получения категорий пользователя
        // Асинхронный метод для получения всех категорий пользователя из базы данных
        public async Task<List<Category>> GetByUserId(int userId) => await _context.Categories.Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения категории пользователя по id из базы данных
        public async Task<Category?> GetByIdAndUserId(int id, int userId) => await _context.Categories.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения категории пользователя по именни из базы данных
        public async Task<Category?> GetByNameAndUserId(string name, int userId) => await _context.Categories.SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        // Асинхронный метод для получения категории пользователя по типу из базы данных
        public async Task<List<Category>> GetByTypeAndUserId(TransactionType type, int userId) => await _context.Categories.Where(x => x.Type == type && x.UserId == userId).ToListAsync();
        #endregion
        #region Методы для получения категорий с загруженными связанными данными
        // Асинхронный метод для получения категорий с загружженными связанными данными
        public async Task<List<Category>> GetAllWithDetails() => await _context.Categories
                                                                               .Include(x => x.Tags)
                                                                               .Include(x => x.Transactions)
                                                                               .Include(x => x.Budgets)
                                                                               .Include(x => x.BillingStatements)
                                                                               .ToListAsync();
        // Асинхронный метод для получения категории по id с загруженными связанными данными
        public async Task<Category?> GetByIdWithDetails(int id) => await _context.Categories
                                                                       .Include(x => x.Tags)
                                                                       .Include(x => x.Transactions)
                                                                       .Include(x => x.Budgets)
                                                                       .Include(x => x.BillingStatements)
                                                                       .SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения категорий по имени с загруженными связанными данными 
        public async Task<List<Category>> GetByNameWithDetails(string name) =>await _context.Categories
                                                                                            .Include(x => x.Tags)
                                                                                            .Include(x => x.Transactions)
                                                                                            .Include(x => x.Budgets)
                                                                                            .Include(x => x.BillingStatements)
                                                                                            .Where(x => x.Name == name)
                                                                                            .ToListAsync();
        // Асинхронный метод для получения категорий по типу с загруженными связанными данными 
        public async Task<List<Category>> GetByTypeWithDetails(TransactionType type) => await _context.Categories
                                                                                                      .Include(x => x.Tags)
                                                                                                      .Include(x => x.Transactions)
                                                                                                      .Include(x => x.Budgets)
                                                                                                      .Include(x => x.BillingStatements)
                                                                                                      .Where(x => x.Type == type)
                                                                                                      .ToListAsync();
        #endregion
        #region Методы для получения категорий пользователя с загруженными связанными данными
        // Асинхронный метод для получения категорий пользователя с загруженными связанными данными
        public async Task<List<Category>> GetByUserIdWithDetails(int userId) => await _context.Categories
                                                                                              .Include(x => x.Tags)
                                                                                              .Include(x => x.Transactions)
                                                                                              .Include(x => x.Budgets)
                                                                                              .Include(x => x.BillingStatements)
                                                                                              .Where(x => x.UserId == userId)
                                                                                              .ToListAsync();
        // Асинхронный метод для получения категории пользователя по id с загруженными связанными данными
        public async Task<Category?> GetByIdAndUserIdWithDetails(int id, int userId) => await _context.Categories
                                                                                          .Include(x => x.Tags)
                                                                                          .Include(x => x.Transactions)
                                                                                          .Include(x => x.Budgets)
                                                                                          .Include(x => x.BillingStatements)
                                                                                          .SingleOrDefaultAsync(x => x.Id == id  && x.UserId == userId);
        // Асинхронный метод для получения категории пользователя по именни с загруженными связанными данными
        public async Task<Category?> GetByNameAndUserIdWithDetails(string name, int userId) => await _context.Categories
                                                                                                .Include(x => x.Tags)
                                                                                                .Include(x => x.Transactions)
                                                                                                .Include(x => x.Budgets)
                                                                                                .Include(x => x.BillingStatements)
                                                                                                .SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        // Асинхронный метод для получения категории пользователя по типу с загруженными связанными данными
        public async Task<List<Category>> GetByTypeAndUserIdWithDetails(TransactionType type, int userId) => await _context.Categories 
                                                                                                               .Include(x => x.Tags)
                                                                                                               .Include(x => x.Transactions)
                                                                                                               .Include(x => x.Budgets)
                                                                                                               .Include(x => x.BillingStatements)
                                                                                                               .Where(x => x.Type == type && x.UserId == userId)
                                                                                                               .ToListAsync();
        #endregion
        #region Методы для добавления, обновления или удаления бюджета с сохранением или без
        // Асинхронный метод для добавления категории без сохрания 
        public async Task Add(Category categoryToAdd) => await _context.Categories.AddAsync(categoryToAdd);
        // Метод для обновления категории без сохранения
        public void Update(Category categoryToUpdate) => _context.Categories.Update(categoryToUpdate);
        // Метод для обновления категории без сохранения
        public void Delete(Category categoryToDelete) => _context.Categories.Remove(categoryToDelete);
        // Асинхронный метод для добавления категории с сохранением
        public async Task AddAndSave(Category categoryToAdd)
        {
            await _context.Categories.AddAsync(categoryToAdd);
            await _context.SaveChangesAsync();
        }
        
        // Асинхронный метод для обноления категории с сохранением
        public async Task UpdateAndSave(Category categoryToUpdate)
        {
            _context.Categories.Update(categoryToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления категории с сохранением
        public async Task DeleteAndSave(Category categoryToDelete)
        {
            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
