using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task Add(User userToAdd)
        {
            await _context.Users.AddAsync(userToAdd);
            await _context.SaveChangesAsync();
        }

        // Обновление пользователя
        public async Task Update(User userToUpdate)
        {
            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();
        }

        // Удаление пользователя
        public async Task Delete(User userToDelete)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
