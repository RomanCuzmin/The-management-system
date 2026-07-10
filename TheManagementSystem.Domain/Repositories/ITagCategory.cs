using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;

namespace TheManagementSystem.Domain.Repositories
{
    public interface ITagCategory
    {
        public Task<List<Tag>> GetAll();
        public Task<Tag> GetById();
        public Task<Tag> GetByName(string name);
        public Task Add(Tag tagToAdd);
        public Task Update(Tag tagToUpdate);
        public Task Delete(Tag tagToDelete);
    }
}

