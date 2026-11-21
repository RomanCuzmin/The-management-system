using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Enums;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
        public Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
        public Task<List<User>> GetUsersByNotificationTypeAsync(NotificationType notificationType);
        public Task<List<User>> GetAllUsersWithDetailsAsync();
        public Task<User?> GetUserByIdWithDetailsAsync(int id);
        public Task<User?> GetUserByEmailWithDetailsAsync(string email);
        public Task<User?> GetUserByEmailAndPasswordWithDetailsAsync(string email, string password);
        public Task<User?> GetUserByPhoneNumberWithDetailsAsync(string phoneNumber);
        public Task<List<User>> GetUsersByNotificationTypeWithDetailsAsync(NotificationType notificationType);
        public Task AddUserAsync(User userToAdd);
        public void UpdateUser(User userToUpdate);
        public void DeleteUser(User userToDelete);
        public Task AddUserAndSaveAsync(User userToAdd);
        public Task UpdateUserAndSaveAsync(User userToUpdate);
        public Task DeleteUserAndSaveAsync(User userToDelete);
    }
}
