using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class PlaseOfWorkRepository : IPlaseOfWorkRepository
    {
        // Конструктор репозитория PlaseOfWorkRepository
        private readonly ApplicationDbContext _context;
        public PlaseOfWorkRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
        #region Методы для получения рабочих мест
        // Асинхронный метод для получения всех рабочих мест
        public async Task<List<PlaceOfWork>> GetAllPlaceOfWorksAsync() => await _context.PlaceOfWorks.ToListAsync();
        // Асинхронный метод для получения рабочего места по id
        public async Task<PlaceOfWork?> GetPlaceOfWorkByIdAsync(int id) => await _context.PlaceOfWorks.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения рабочего места по компании
        public async Task<List<PlaceOfWork>> GetPlaceOfWorksByCompanyAsync(string company) => await _context.PlaceOfWorks.Where(x => x.Company == company).ToListAsync();
        // Асинхронный метод для получения рабочих мест по отделу
        public async Task<List<PlaceOfWork>> GetPlaceOfWorksByDivisionAsync(string division) => await _context.PlaceOfWorks.Where(x => x.Division == division).ToListAsync();
        // Асинхронный метод для получения рабочих мест по должности 
        public async Task<List<PlaceOfWork>> GetPlaceOfWorksByPostAsync(string post) => await _context.PlaceOfWorks.Where(x => x.Post == post).ToListAsync();
        #endregion
        #region Методы для получения рабочих мест пользователя 
        // Асинхронный метод для получения всех рабочих мест пользователя 
        public async Task<List<PlaceOfWork>> GetPlaceOfWorksByUserIdAsync(int userId) => await _context.PlaceOfWorks.Where( x=> x.UserId == userId).ToListAsync();
        // Асинхронный метод для получения рабочего места пользователя по id
        public async Task<PlaceOfWork?> GetPlaceOfWorkByIdAndUserIdAsync(int id, int userId) => await _context.PlaceOfWorks.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения рабочего места пользователя по компании
        public async Task<PlaceOfWork?> GetPlaceOfWorkByCompanyAndUserIdAsync(string company, int userId) => await _context.PlaceOfWorks.SingleOrDefaultAsync(x => x.Company == company && x.UserId == userId);
        // Асинхронный метод для получения рабочего места пользователя по отделу
        public async Task<PlaceOfWork?> GetPlaceOfWorkByDivisionAndUserIdAsync(string division, int userId) => await _context.PlaceOfWorks.SingleOrDefaultAsync(x => x.Division == division && x.UserId == userId);
        // Асинхронный метод для получения рабочего места пользователя по должности
        public async Task<PlaceOfWork?> GetPlaceOfWorkByPostAndUserIdAsync(string post, int userId) => await _context.PlaceOfWorks.SingleOrDefaultAsync(x => x.Post == post && x.UserId == userId);
        #endregion
        #region Методы для получения рабочих мест с загруженными связанными данными
        // Асинхронный метод для получения всех рабочих мест с загруженными связанными данными
        public async Task<List<PlaceOfWork>> GetAllPlaceOfWorksWithDetailsAsync() => await _context.PlaceOfWorks
                                                                                                   .Include(x => x.WorkDays)
                                                                                                   .Include(x => x.PayStubs)
                                                                                                   .ToListAsync();
        // Асинхронный метод для получения рабочего места по id с загруженными связанными данными
        public async Task<PlaceOfWork?> GetPlaceOfWorkByIdWithDetailsAsync(int id) => await _context.PlaceOfWorks
                                                                                                    .Include(x => x.WorkDays)
                                                                                                    .Include(x => x.PayStubs)
                                                                                                    .SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения рабочих мест по компании с загруженными связанными данными
        public async Task<List<PlaceOfWork>> GetPlaceOfWorksByCompanyWithDetailsAsync(string company) => await _context.PlaceOfWorks
                                                                                                                       .Include(x => x.WorkDays)
                                                                                                                       .Include(x => x.PayStubs)
                                                                                                                       .Where(x => x.Company == company)
                                                                                                                       .ToListAsync();   
        // Асихронный метод для получения рабочих мест по отделу с загруженными связанными данными
        public async Task<List<PlaceOfWork>> GetPlaceOfWorksByDivisionWithDetailsAsync(string division) => await _context.PlaceOfWorks
                                                                                                                         .Include(x => x.WorkDays)
                                                                                                                         .Include(x => x.PayStubs)
                                                                                                                         .Where(x => x.Division == division)
                                                                                                                         .ToListAsync();
        // Асинхронный метод для получения рабочих мест по должности с загруженными связанными данными
        public async Task<List<PlaceOfWork>> GetPlaceOfWorksByPostWithDetailsAsync(string post) => await _context.PlaceOfWorks
                                                                                                                 .Include(x => x.WorkDays)
                                                                                                                 .Include(x => x.PayStubs)
                                                                                                                 .Where(x => x.Post == post)
                                                                                                                 .ToListAsync();
        #endregion
        #region Методы для получения рабочих мест пользователя с загруженными связанными данными
        // Асинхронный метод для получения всех рабочих мест пользователя с загруженными связанными данными
        public async Task<List<PlaceOfWork>> GetPlaceOfWorksByUserIdWithDetailsAsync(int userId) => await _context.PlaceOfWorks
                                                                                                                  .Include(x => x.WorkDays)
                                                                                                                  .Include(x => x.PayStubs)
                                                                                                                  .Where(x => x.UserId == userId)
                                                                                                                  .ToListAsync();
        // Асинхронный метод для получения рабочего места пользователя по id с загруженными связанными данными
        public async Task<PlaceOfWork?> GetPlaceOfWorkByIdAndUserIdWithDetailsAsync(int id, int userId) => await _context.PlaceOfWorks
                                                                                                                         .Include(x => x.WorkDays)
                                                                                                                         .Include(x => x.PayStubs)
                                                                                                                         .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения рабочего места пользователя по компании с загруженными связанными данными 
        public async Task<PlaceOfWork?> GetPlaceOfWorkByCompanyAndUserIdWithDetailsAsync(string company, int userId) => await _context.PlaceOfWorks
                                                                                                                                      .Include(x => x.WorkDays)
                                                                                                                                      .Include(x => x.PayStubs)
                                                                                                                                      .SingleOrDefaultAsync(x => x.Company == company && x.UserId == userId);
        // Асинхронный метод для получения рабочего места пользователя по отделу с загруженными связанными данными
        public async Task<PlaceOfWork?> GetPlaceOfWorkByDivisionAndUserIdWithDetailsAsync(string division, int userId) => await _context.PlaceOfWorks
                                                                                                                                        .Include(x => x.WorkDays)
                                                                                                                                        .Include(x => x.PayStubs)
                                                                                                                                        .SingleOrDefaultAsync(x => x.Division == division && x.UserId == userId);
        // Асинхронный метод для получения рабочего места пользователя по должности с загруженными связанными данными
        public async Task<PlaceOfWork?> GetPlaceOfWorkByPostAndUserIdWithDetailsAsync(string post, int userId) => await _context.PlaceOfWorks
                                                                                                                                .Include(x => x.WorkDays)
                                                                                                                                .Include(x => x.PayStubs)
                                                                                                                                .SingleOrDefaultAsync(x => x.Post == post && x.UserId == userId);
        #endregion
        #region Методы для добавления, обновления или удаления рабочего места с сохранением или без 
        // Асинхронный метод для добавления рабочего места без сохранения
        public async Task AddPlaceOfWorkAsync(PlaceOfWork plaсeOfWorkToAdd) => await _context.PlaceOfWorks.AddAsync(plaсeOfWorkToAdd);
        // Метод для обновления рабочего места без сохранения
        public void UpdatePlaceOfWork(PlaceOfWork plaсeOfWorkToUpdate) => _context.Update(plaсeOfWorkToUpdate);
        // Метод для удаления рабочего места без сохранения
        public void DeletePlaceOfWork(PlaceOfWork plaсeOfWorkToDelete) => _context.PlaceOfWorks.Remove(plaсeOfWorkToDelete);
        
        // Асинхронный метод для добавления рабочего места с сохранением
        public async Task AddPlaceOfWorkAndSaveAsync(PlaceOfWork plaseOfWorkToAdd)
        {
            await _context.PlaceOfWorks.AddAsync(plaseOfWorkToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления рабочего места с сохранением
        public async Task UpdatePlaceOfWorkAndSaveAsync(PlaceOfWork plaseOfWorkToUpdate)
        {
            _context.PlaceOfWorks.Update(plaseOfWorkToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления рабочего места с сохранением
        public async Task DeletePlaceOfWorkAndSaveAsync(PlaceOfWork plaseOfWorkToDelete)
        {
            _context.PlaceOfWorks.Remove(plaseOfWorkToDelete);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
