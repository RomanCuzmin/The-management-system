using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор репозитория TagRepository
        public TagRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
        #region Методы для получения тэгов
        // Асинхронный метод для получения всех тэгов
        public Task<List<Tag>> GetAllTagsAsync() => _context.Tags.ToListAsync();
        // Асинхронный метод для получения тэга по id
        public Task<Tag?> GetTagByIdAsync(int id) => _context.Tags.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения тэгов по id категории
        public async Task<List<Tag>> GetTagsByCategoryIdAsync(int categoryId) => await _context.Tags.Where(x => x.CategoryId == categoryId).ToListAsync();
        // Асинхронный метод для получения тэгов по имени 
        public Task<List<Tag>> GetTagsByNameAsync(string name) => _context.Tags.Where(x => x.Name == name).ToListAsync();
        #endregion
        #region Методы для получения тэгов пользователя
        // Асинхронный метод для получения всех тэгов пользователя
        public async Task<List<Tag>> GetTagsByUserIdAsync(int userId) => await _context.Tags.Where(x => x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения тэга пользователя по id
        public async Task<Tag?> GetTagByIdAndUserIdAsync(int id, int userId) => await _context.Tags.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения тэгов пользователя по id категории
        public async Task<List<Tag>> GetTagsByCategoryIdAndUserIdAsync(int categoryId, int userId) => await _context.Tags.Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения тэгов пользователя по именни
        public async Task<Tag?> GetTagByNameAndUserIdAsync(string name, int userId) => await _context.Tags.SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        #endregion
        #region Методы для получения тэгов с загруженными связанными данными
        // Асинхронный метод для получения всех тэгов с загруженными связанными данными
        public async Task<List<Tag>> GetAllTagsWithDetailsAsync() => await _context.Tags
                                                                          .Include(x => x.Category)
                                                                          .Include(x => x.Transactions)
                                                                          .ToListAsync();
        // Асинхронный метод для получения тэга по id с загруженными связанными данными
        public async Task<Tag?> GetTagByIdWithDetailsAsync(int id) => await _context.Tags
                                                                           .Include(x => x.Category)
                                                                           .Include(x => x.Transactions)
                                                                           .SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения тэгов по id категории с загруженными связанными данными
        public async Task<List<Tag>> GetTagsByCategoryIdWithDetailsAsync(int categoryId) => await _context.Tags
                                                                                                 .Include(x => x.Category)
                                                                                                 .Include(x => x.Transactions)
                                                                                                 .Where(x => x.CategoryId == categoryId)
                                                                                                 .ToListAsync();
        // Асинхронный метод для получения тэгов по именни с загруженными связанными данными
        public async Task<List<Tag>> GetTagsByNameWithDetailsAsync(string name) => await _context.Tags
                                                                                        .Include(x => x.Category)
                                                                                        .Include(x => x.Transactions)
                                                                                        .Where(x => x.Name == name)
                                                                                        .ToListAsync();
        #endregion
        #region Методы для получения тэгов пользователя с загруженными связанными данными
        // Асинхронный метод для получения тэгов пользователя с загруженными связанными данными
        public async Task<List<Tag>> GetTagsByUserIdWithDetailsAsync(int userId) => await _context.Tags
                                                                                         .Include(x => x.Category)
                                                                                         .Include(x => x.Transactions)
                                                                                         .Where(x => x.UserId == userId)
                                                                                         .ToListAsync();
        // Асинхронный метод для получения тэгов пользователя по id с загруженными связанными данными
        public async Task<Tag?> GetTagByIdAndUserIdWithDetailsAsync(int id, int userId) => await _context.Tags
                                                                                                 .Include(x => x.Category)
                                                                                                 .Include(x => x.Transactions)
                                                                                                 .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения  тэгов пользователя по id категории с загруженными связанными данными
        public async Task<List<Tag>> GetTagByCategoryIdAndndUserIdWithDetailsAsync(int categoryId, int userId) => await _context.Tags
                                                                                                                      .Include(x => x.Category)
                                                                                                                      .Include(x => x.Transactions)
                                                                                                                      .Where(x => x.CategoryId == categoryId && x.UserId == userId)
                                                                                                                      .ToListAsync();
        // Асинхронный метод для получения тэгов пользователя по именни с загруженными связанными данными
        public async Task<Tag?> GetTagByNameAndUserIdWithDetailsAsync(string name, int userId) => await _context.Tags
                                                                                                       .Include(x => x.Category)
                                                                                                       .Include(x => x.Transactions)
                                                                                                       .SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        #endregion
        #region Методы для добавления, обновления или удаления тэга с сохранением или без
        // Асинхронный метод для добавления тэга без сохранения
        public async Task AddTagAsync(Tag tagToAdd) => await _context.Tags.AddAsync(tagToAdd);
        // Метод для обновления тэга без сохранения
        public void UpdateTag(Tag tagToUpdate) => _context.Tags.Update(tagToUpdate);
        // Метод для удаления тэга без сохранения
        public void DeleteTag(Tag tagToDelete) => _context.Tags.Remove(tagToDelete);
        
        // Асинхронный метод для добавления тэга с сохранением
        public async Task AddTagAndSaveAsync(Tag tagToAdd)
        {
            await _context.Tags.AddAsync(tagToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления тэга с сохранением
        public async Task UpdateTagAndSaveAsync(Tag tagToUpdate)
        {
            _context.Tags.Update(tagToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления тэга с сохранением
        public async Task DeleteTagAndSaveAsync(Tag tagToDelete)
        {
            _context.Tags.Remove(tagToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
