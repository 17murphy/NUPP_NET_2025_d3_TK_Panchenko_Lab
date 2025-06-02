using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zoo.Common.Services
{
	public interface ICrudService<T>
	{
		void Create(T element);
		T Read(Guid id);
		IEnumerable<T> ReadAll();
		void Update(T element);
		void Remove(T element);
	}

    public interface ICrudServiceAsync<T> : IEnumerable<T>
    {
        Task<bool> CreateAsync(T element);
        Task<T> ReadAsync(Guid id);
        Task<IEnumerable<T>> ReadAllAsync();
        Task<IEnumerable<T>> ReadAllAsync(int page, int amount);
        Task<bool> UpdateAsync(T element);
        Task<bool> RemoveAsync(T element);
        Task<bool> SaveAsync();
    }

}
