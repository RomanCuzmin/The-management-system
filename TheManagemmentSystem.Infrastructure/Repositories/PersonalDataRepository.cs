using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories
{
    public class PersonalDataRepository : IPersonalDataRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonalDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Методы для получения персональных данных
        // Асинхронный метод для получения всех персональных данных
        public async Task<List<PersonalData>> GetAllPersonalDataAsync() => await _context.PersonalData.ToListAsync();
        // Асинхронный метод для получения персональных данных по id
        public async Task<PersonalData?> GetPersonalDataByIdAsync(int id) => await _context.PersonalData.SingleOrDefaultAsync(x => x.Id == id);
        // Асинхронный метод для получения персональных данных по имени
        public async Task<List<PersonalData>> GetPersonalDataByNameAsync(string name) => await _context.PersonalData.Where(x => x.Name == name).ToListAsync();
        // Асинхронный метод для получения персональных данных по фамилии
        public async Task<List<PersonalData>> GetPersonalDataBySurnameAsync(string surName) => await _context.PersonalData.Where(x => x.SurName == surName).ToListAsync();
        // Асинхонный метод для получения персональных данных по отчеству
        public async Task<List<PersonalData>> GetPersonalDataByPatronymicAsync(string patronymic) => await _context.PersonalData.Where(x => x.Patronymic == patronymic).ToListAsync();
        #endregion
        #region Методы для получения персональных данных пользователя
        // Асинхронный метод для получения персональных данных пользователя
        public async Task<PersonalData?> GetPersonalDataByUserIdAsync(int userId) => await _context.PersonalData.SingleOrDefaultAsync(x => x.UserId == userId);
        // Асинхронный метод для получения персональных данных пользователя по id
        public async Task<PersonalData?> GetPersonalDataByIdAndUserIdAsync(int id, int userId) => await _context.PersonalData.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        // Асинхронный метод для получения персональных данных пользователя по имени
        public async Task<PersonalData?> GetPersonalDataByNameAndUserIdAsync(string name, int userId) => await _context.PersonalData.SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        // Асинхронный метод для получения персональных данных пользователя по фамилии
        public async Task<PersonalData?> GetPersonalDataBySurnameAndUserIdAsync(string surName, int userId) => await _context.PersonalData.SingleOrDefaultAsync(x => x.SurName == surName && x.UserId == userId);
        // Асинхронный метод для получения персональных данных пользователя по отчеству
        public async Task<PersonalData?>GetPersonalDataByPatronymicAndUserIdAsync(string patronymic, int userId) => await _context.PersonalData.SingleOrDefaultAsync(x => x.Patronymic == patronymic && x.UserId == userId);
        #endregion
        #region Методы для сохранения, обновления или удаление персональных данных с сохранением или без 
        // Асинхронный метод для добавления персональных данных пользователя без сохранения
        public async Task AddPersonalDataAsync(PersonalData personalDataToAdd) => await _context.PersonalData.AddAsync(personalDataToAdd);
        // Асинхронный метод для возвращения персональных данных пользователя без сохранения
        public void UpdatePersonalData(PersonalData personalDataToUpdate) => _context.PersonalData.Update(personalDataToUpdate);
        // Асинхронный метод для удаления персональных данных пользователя без сохранения
        public void DeletePersonalData(PersonalData personalDataToDelete) => _context.Remove(personalDataToDelete);

        // Асинхронный метод для добавления персональных данных с сохранением
        public async Task AddPersonalDataAndSaveAsync(PersonalData personalDataToAdd)
        {
            await _context.PersonalData.AddAsync(personalDataToAdd);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для обновления персональных данных с сохранением
        public async Task UpdatePersonalDataAndSaveAsync(PersonalData personalDataToUpdate)
        {
            _context.PersonalData.Update(personalDataToUpdate);
            await _context.SaveChangesAsync();
        }

        // Асинхронный метод для удаления персональных данных с сохранением
        public async Task DeletePersonalDataAndSaveAsync(PersonalData personalDataToDelete)
        {
            _context.PersonalData.Remove(personalDataToDelete);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
