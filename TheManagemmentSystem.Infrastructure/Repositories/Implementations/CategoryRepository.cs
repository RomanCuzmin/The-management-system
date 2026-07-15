using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context; 

        // Конструктор
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получение всех категорий
        public Task<List<Category>> GetAll() => _context.Categories.ToListAsync();

        // Получение категории по id
        public Task<Category> GetById(int id) => _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        // Получение категории по имени 
        public Task<Category> GetByName(string name) => _context.Categories.FirstOrDefaultAsync(x => x.Name == name);

        // Получение категории по типу 
        public Task<List<Category>> GetByType(TransactionType type) => _context.Categories.Where(x => x.Type == type).ToListAsync();

        // Добавление категории
        public async Task Add(Category categoryToAdd)
        {
            await _context.Categories.AddAsync(categoryToAdd);
            await _context.SaveChangesAsync();
        }
        
        // Обновление категории
        public async Task Update(Category categoryToUpdate)
        {
            _context.Categories.Update(categoryToUpdate);
            await _context.SaveChangesAsync();
        }

        // Удаление категории
        public async Task Delete(Category categoryToDelete)
        {
            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
