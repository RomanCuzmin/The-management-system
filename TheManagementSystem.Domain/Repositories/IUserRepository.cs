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
        public Task<List<User>> GetAllWithDetails();
        public Task<User> GetByIdWithDetails(int id);
        public Task<User> GetByEmaiWithDetailsl(string email);
        public Task<User> GetByEmailAndPasswordWithDetails(string email, string password);
        public Task<User> GetByPhoneNumberWithDetails(string phoneNumber);
        public Task<List<User>> GetByNotificationTypeWithDetails(NotificationType notificationType);
        public Task Add(User userToAdd);
        public void Update(User userToUpdate);
        public void Delete(User userToDelete);
        public Task AddAndSave(User userToAdd);
        public Task UpdateAndSave(User userToUpdate);
        public Task DeleteAndSave(User userToDelete);
    }
}
