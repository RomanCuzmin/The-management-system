using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IWorkerRepository
    {
        public Task<List<Worker>> GetAll();
        public Task<Worker> GetById(int id);
        public Task<Worker> GetByUserId(int userId);
        public Task<List<Worker>> GetByName(string name);
        public Task<List<Worker>> GetBySurName(string surName);
        public Task<List<Worker>> GetByPatronymic(string patronymic);
        public Task<List<Worker>> GetByPlaseOfWork(string plaseOfWork);
        public Task<List<Worker>> GetByDivision(string division);
        public Task<List<Worker>> GetByPost(string post);
        public Task<Worker> GetByServiceNumber(string  serviceNumber);
        public Task Add(Worker workerToAdd);
        public Task Update(Worker workerToUpdate);
        public Task Delete(Worker workerToDelete);
    }
}
