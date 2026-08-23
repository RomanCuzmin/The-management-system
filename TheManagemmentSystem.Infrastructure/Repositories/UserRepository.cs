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

        // Конструктор
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получение всех пользователей
        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();

        // Получение пользователя по id
        public async Task<User> GetById(int id) => await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

        // Получение пользователя по email
        public async Task<User> GetByEmail(string email) => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

        // Получение пользователя по  email и паролю
        public async Task<User> GetByEmailAndPassword(string email, string password) => await _context.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        
        // Получение пользователя по типу уведомлений
        public async Task<List<User>> GetByNotificationType(NotificationType notificationType) => await _context.Users.Where(x => x.NotificationType == notificationType).ToListAsync();

        // Получение пользователя по номеру телефона
        public async Task<User> GetByPhoneNumber(string phoneNumber) => await _context.Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

        // Добавление пользователя
        public async Task AddAndSave(User userToAdd)
        {
            await _context.Users.AddAsync(userToAdd);
            await _context.SaveChangesAsync();
        }

        // Обновление пользователя
        public async Task UpdateAndSave(User userToUpdate)
        {
            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();
        }

        // Удаление пользователя
        public async Task DeleteAndSave(User userToDelete)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllWithDetails() => await _context.Users
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

        public async Task<User?> GetByIdWithDetails(int id) => await _context.Users
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

        public async Task<User?> GetByEmaiWithDetailsl(string email) => await _context.Users
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

        public async Task<User?> GetByEmailAndPasswordWithDetails(string email, string password) => await _context.Users
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

        public async Task<User?> GetByPhoneNumberWithDetails(string phoneNumber) => await _context.Users
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
        public async Task<List<User>> GetByNotificationTypeWithDetails(NotificationType notificationType) => await _context.Users
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

        public async Task Add(User userToAdd) => await _context.Users.AddAsync(userToAdd);

        public void Update(User userToUpdate) => _context.Users.Update(userToUpdate);

        public void Delete(User userToDelete) => _context.Users.Remove(userToDelete);
    }
}
