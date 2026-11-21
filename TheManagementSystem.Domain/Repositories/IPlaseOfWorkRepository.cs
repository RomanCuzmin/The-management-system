using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface IPlaseOfWorkRepository
    {
        public Task<List<PlaceOfWork>> GetAllPlaceOfWorksAsync();
        public Task<PlaceOfWork?> GetPlaceOfWorkByIdAsync(int id);
        public Task<List<PlaceOfWork>> GetPlaceOfWorksByCompanyAsync(string company);
        public Task<List<PlaceOfWork>> GetPlaceOfWorksByDivisionAsync(string division);
        public Task<List<PlaceOfWork>> GetPlaceOfWorksByPostAsync(string post);
        public Task<List<PlaceOfWork>> GetPlaceOfWorksByUserIdAsync(int userId);
        public Task<PlaceOfWork?> GetPlaceOfWorkByIdAndUserIdAsync(int id, int userId);
        public Task<PlaceOfWork?> GetPlaceOfWorkByCompanyAndUserIdAsync(string company, int userId);
        public Task<PlaceOfWork?> GetPlaceOfWorkByDivisionAndUserIdAsync(string division, int userId);
        public Task<PlaceOfWork?> GetPlaceOfWorkByPostAndUserIdAsync(string post, int userId);
        public Task<List<PlaceOfWork>> GetAllPlaceOfWorksWithDetailsAsync();
        public Task<PlaceOfWork?> GetPlaceOfWorkByIdWithDetailsAsync(int id);
        public Task<List<PlaceOfWork>> GetPlaceOfWorksByCompanyWithDetailsAsync(string company);
        public Task<List<PlaceOfWork>> GetPlaceOfWorksByDivisionWithDetailsAsync(string division);
        public Task<List<PlaceOfWork>> GetPlaceOfWorksByPostWithDetailsAsync(string post);
        public Task<List<PlaceOfWork>> GetPlaceOfWorksByUserIdWithDetailsAsync(int userId);
        public Task<PlaceOfWork?> GetPlaceOfWorkByIdAndUserIdWithDetailsAsync(int id, int userId);
        public Task<PlaceOfWork?> GetPlaceOfWorkByCompanyAndUserIdWithDetailsAsync(string company, int userId);
        public Task<PlaceOfWork?> GetPlaceOfWorkByDivisionAndUserIdWithDetailsAsync(string division, int userId);
        public Task<PlaceOfWork?> GetPlaceOfWorkByPostAndUserIdWithDetailsAsync(string post, int userId);
        public Task AddPlaceOfWorkAsync(PlaceOfWork plaсeOfWorkToAdd);
        public void UpdatePlaceOfWork(PlaceOfWork plaсeOfWorkToUpdate);
        public void DeletePlaceOfWork(PlaceOfWork plaсeOfWorkToDelete);
        public Task AddPlaceOfWorkAndSaveAsync(PlaceOfWork plaseOfWorkToAdd);
        public Task UpdatePlaceOfWorkAndSaveAsync(PlaceOfWork plaseOfWorkToUpdate);
        public Task DeletePlaceOfWorkAndSaveAsync(PlaceOfWork plaseOfWorkToDelete);
    }
}
//public int Id { get; set; }
//public int UserId { get; set; }
//public string Company { get; set; }
//public string Division { get; set; }
//public string Post { get; set; }
//public string ServiceNumber { get; set; }