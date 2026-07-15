using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор
        public AccountRepository(ApplicationDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Получение всех счётов 
        public async Task<List<Account>> GetAll() => await _context.Accounts.ToListAsync();
         
        // Получение счета по id
        public async Task<Account> GetById(int id) => await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id);

        // Получение счетов по id пользователя
        public async Task<List<Account>> GetByUserId(int userId) => await _context.Accounts.Where(x => x.UserId == userId).ToListAsync();

        // Получение счетов по имени
        public async Task<Account> GetByName(string name) => await _context.Accounts.SingleOrDefaultAsync(x => x.Name == name);

        // Получение счетов по валюте
        public async Task<List<Account>> GetByCurrency(Currency currency) => await _context.Accounts.Where(x =>x.Currency == currency).ToListAsync();

        // Добавление счета
        public async Task Add(Account accountToAdd)
        {
            await _context.AddAsync(accountToAdd);
            await _context.SaveChangesAsync();
        }

        // Обновление счета
        public async Task Update(Account accountToUpdate)
        {
            _context.Accounts.Update(accountToUpdate);
            await _context.SaveChangesAsync();
        }

        // Удаление счёта
        public async Task Delete(Account accountToDelete)
        {
            _context.Remove(accountToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
