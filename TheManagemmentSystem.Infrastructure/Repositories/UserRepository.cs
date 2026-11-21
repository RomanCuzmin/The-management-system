using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор репозитория UserRepository
        public UserRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
        #region Методы для получения пользователей
        // Асинхронный метод для получения всех пользователей
        public async Task<List<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();
        // Асинхронный метод для получения пользователя по id
        public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения пользователя по email
        public async Task<User?> GetUserByEmailAsync(string email) => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        // Асинхронный метод для получения пользователя по email и паролю
        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password) => await _context.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        // Асинхронный метод для получения пользователей по типу уведомлений
        public async Task<List<User>> GetUsersByNotificationTypeAsync(NotificationType notificationType) => await _context.Users.Where(x => x.NotificationType == notificationType).ToListAsync();
        // Асинхронный метод для получения пользователя по номеру телефона
        public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber) => await _context.Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        #endregion
        #region Методы для получения пользователя с загруженными связанными данными
        // Асинхронный метод для получения пользователей с загруженными связанными данными
        public async Task<List<User>> GetAllUsersWithDetailsAsync() => await _context.Users
                                                                           .Include(x => x.Accounts)
                                                                           .Include(x => x.Budgets)
                                                                           .Include(x => x.Categories)
                                                                           .Include(x => x.PayStubs)
                                                                           .Include(x => x.PersonalData)
                                                                           .Include(x => x.PlaseOfWorks)
                                                                           .Include(x => x.ReceiptItems)
                                                                           .Include(x => x.Receipts)
                                                                           .Include(x => x.Tags)
                                                                           .Include(x => x.Transactions)
                                                                           .Include(x => x.WorkDays)
                                                                           .ToListAsync();
        // Асинхронный метод для получения пользователя по id с загруженными связанными данными
        public async Task<User?> GetUserByIdWithDetailsAsync(int id) => await _context.Users
                                                                      .Include(x => x.Accounts)
                                                                      .Include(x => x.Budgets)
                                                                      .Include(x => x.Categories)
                                                                      .Include(x => x.PayStubs)
                                                                      .Include(x => x.PersonalData)
                                                                      .Include(x => x.PlaseOfWorks)
                                                                      .Include(x => x.ReceiptItems)
                                                                      .Include(x => x.Receipts)
                                                                      .Include(x => x.Tags)
                                                                      .Include(x => x.Transactions)
                                                                      .Include(x => x.WorkDays)
                                                                      .SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения пользователя по email с загруженными связанными данными
        public async Task<User?> GetUserByEmailWithDetailsAsync(string email) => await _context.Users
                                                                                      .Include(x => x.Accounts)
                                                                                      .Include(x => x.Budgets)
                                                                                      .Include(x => x.Categories)
                                                                                      .Include(x => x.PayStubs)
                                                                                      .Include(x => x.PersonalData)
                                                                                      .Include(x => x.PlaseOfWorks)
                                                                                      .Include(x => x.ReceiptItems)
                                                                                      .Include(x => x.Receipts)
                                                                                      .Include(x => x.Tags)
                                                                                      .Include(x => x.Transactions)
                                                                                      .Include(x => x.WorkDays)
                                                                                      .SingleOrDefaultAsync(x => x.Email == email);
        // Асинхронный метод для получения пользователя по email и паролю с загруженными связанными данными
        public async Task<User?> GetUserByEmailAndPasswordWithDetailsAsync(string email, string password) => await _context.Users
                                                                                                                  .Include(x => x.Accounts)
                                                                                                                  .Include(x => x.Budgets)
                                                                                                                  .Include(x => x.Categories)
                                                                                                                  .Include(x => x.PayStubs)
                                                                                                                  .Include(x => x.PersonalData)
                                                                                                                  .Include(x => x.PlaseOfWorks)
                                                                                                                  .Include(x => x.ReceiptItems)
                                                                                                                  .Include(x => x.Receipts)
                                                                                                                  .Include(x => x.Tags)
                                                                                                                  .Include(x => x.Transactions)
                                                                                                                  .Include(x => x.WorkDays)
                                                                                                                  .SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        // Асинхронный метод для получения пользователя по номеру телефона с загруженными связанными данными
        public async Task<User?> GetUserByPhoneNumberWithDetailsAsync(string phoneNumber) => await _context.Users
                                                                                                  .Include(x => x.Accounts)
                                                                                                  .Include(x => x.Budgets)
                                                                                                  .Include(x => x.Categories)
                                                                                                  .Include(x => x.PayStubs)
                                                                                                  .Include(x => x.PersonalData)
                                                                                                  .Include(x => x.PlaseOfWorks)
                                                                                                  .Include(x => x.ReceiptItems)
                                                                                                  .Include(x => x.Receipts)
                                                                                                  .Include(x => x.Tags)
                                                                                                  .Include(x => x.Transactions)
                                                                                                  .Include(x => x.WorkDays)
                                                                                                  .SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        // Асинхронный метод для получения пользователя по типу уведомлений с загруженными связанными данными 
        public async Task<List<User>> GetUsersByNotificationTypeWithDetailsAsync(NotificationType notificationType) => await _context.Users
                                                                                                                           .Include(x => x.Accounts)
                                                                                                                           .Include(x => x.Budgets)
                                                                                                                           .Include(x => x.Categories)
                                                                                                                           .Include(x => x.PayStubs)
                                                                                                                           .Include(x => x.PersonalData)
                                                                                                                           .Include(x => x.PlaseOfWorks)
                                                                                                                           .Include(x => x.ReceiptItems)
                                                                                                                           .Include(x => x.Receipts)
                                                                                                                           .Include(x => x.Tags)
                                                                                                                           .Include(x => x.Transactions)
                                                                                                                           .Include(x => x.WorkDays)
                                                                                                                           .Where(x => x.NotificationType == notificationType)
                                                                                                                           .ToListAsync();
        #endregion
        #region Методы для добавления, обновления или удаления  с сохранением или без
        // Асинхронный метод для добавления пользователя без сохранения
        public async Task AddUserAsync(User userToAdd) => await _context.Users.AddAsync(userToAdd);
        // Метод для обновления пользователя без сохраннения
        public void UpdateUser(User userToUpdate) => _context.Users.Update(userToUpdate);
        // Метод для удаления пользователя без сохранения
        public void DeleteUser(User userToDelete) => _context.Users.Remove(userToDelete);

        // Асинхронный метод для добавления пользователя с сохранением
        public async Task AddUserAndSaveAsync(User userToAdd)
        {
            await _context.Users.AddAsync(userToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления пользователя с сохранением
        public async Task UpdateUserAndSaveAsync(User userToUpdate)
        {
            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления пользователя с сохранением
        public async Task DeleteUserAndSaveAsync(User userToDelete)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
