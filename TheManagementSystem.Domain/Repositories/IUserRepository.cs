using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAll();
        public Task<User> GetById(int id);
        public Task<User> GetByEmail(string email);
        public Task<User> GetByEmailAndPassword(string email, string password);
        public Task<User> GetByPhoneNumber(string phoneNumber);
        public Task<List<User>> GetByNotificationType(NotificationType notificationType);
        public Task Add(User userToAdd);
        public Task Update(User userToUpdate);
        public Task Delete(User userToDelete);
    }
}
