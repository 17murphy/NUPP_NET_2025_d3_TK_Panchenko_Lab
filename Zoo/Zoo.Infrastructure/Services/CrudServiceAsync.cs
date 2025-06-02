using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Infrastructure.Services;
using Zoo.Infrastructure.Repositories;

namespace Zoo.Infrastructure.Services
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly ZooContext _context;

        public CrudServiceAsync(IRepository<T> repository, ZooContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _repository.AddAsync(element);
            return await SaveAsync();
        }

        public async Task<T> ReadAsync(Guid id)
        {
            var entity = await _repository.GetAllAsync();
            return entity.FirstOrDefault(e => (Guid)e.GetType().GetProperty("Id")?.GetValue(e)! == id)!;
        }

        public async Task<IEnumerable<T>> ReadAllAsync() => await _repository.GetAllAsync();

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return (await _repository.GetAllAsync())
                   .Skip((page - 1) * amount)
                   .Take(amount);
        }

        public async Task<bool> UpdateAsync(T element)
        {
            await _repository.Update(element);
            return await SaveAsync();
        }

        public async Task<bool> RemoveAsync(T element)
        {
            await _repository.Delete(element);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;
    }
}
