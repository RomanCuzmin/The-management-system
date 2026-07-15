using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheManagementSystem.Domain.Entities;
using TheManagementSystem.Domain.Repositories;

namespace TheManagemmentSystem.Infrastructure.Repositories.Implementations
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        // Конструктор
        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получение всех тэгов
        public Task<List<Tag>> GetAll() => _context.Tags.ToListAsync();

        // Получение тэга по id
        public Task<Tag> GetById(int id) => _context.Tags.SingleOrDefaultAsync(x => x.Id == id);

        // Получение тэга по именни 
        public Task<Tag> GetByName(string name) => _context.Tags.SingleOrDefaultAsync(x => x.Name == name);

        // Добавление тэга
        public async Task Add(Tag tagToAdd)
        {
            await _context.Tags.AddAsync(tagToAdd);
            await _context.SaveChangesAsync();
        }

        // Обновление тэга
        public Task Update(Tag tagToUpdate)
        {
            throw new NotImplementedException();
        }

        // Удаление тэга
        public Task Delete(Tag tagToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
