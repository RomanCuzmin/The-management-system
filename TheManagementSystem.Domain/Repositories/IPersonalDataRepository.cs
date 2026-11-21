using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IPersonalDataRepository
    {
        public Task<List<PersonalData>> GetAllPersonalDataAsync();
        public Task<PersonalData?> GetPersonalDataByIdAsync(int id);
        public Task<List<PersonalData>> GetPersonalDataByNameAsync(string name);
        public Task<List<PersonalData>> GetPersonalDataBySurnameAsync(string surName);
        public Task<List<PersonalData>> GetPersonalDataByPatronymicAsync(string patronymic);
        public Task<PersonalData?> GetPersonalDataByUserIdAsync(int userId);
        public Task<PersonalData?> GetPersonalDataByIdAndUserIdAsync(int id, int userId);
        public Task<PersonalData?> GetPersonalDataByNameAndUserIdAsync(string name, int userId);
        public Task<PersonalData?> GetPersonalDataBySurnameAndUserIdAsync(string surName, int userId);
        public Task<PersonalData?> GetPersonalDataByPatronymicAndUserIdAsync(string patronymic, int userId);
        public Task AddPersonalDataAsync(PersonalData personalDataToAdd);
        public void UpdatePersonalData(PersonalData personalDataToUpdate);
        public void DeletePersonalData(PersonalData personalDataToDelete);
        public Task AddPersonalDataAndSaveAsync(PersonalData personalDataToAdd);
        public Task UpdatePersonalDataAndSaveAsync(PersonalData personalDataToUpdate);
        public Task DeletePersonalDataAndSaveAsync(PersonalData personalDataToDelete);
    }
}
