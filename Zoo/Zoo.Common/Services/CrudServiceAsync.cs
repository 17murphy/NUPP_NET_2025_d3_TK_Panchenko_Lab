using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Zoo.Common.Services
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly ConcurrentDictionary<Guid, T> _storage = new ConcurrentDictionary<Guid, T>();
        private readonly string _filePath;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public CrudServiceAsync(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<bool> CreateAsync(T element)
        {
            var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
            return _storage.TryAdd(id, element);
        }

        public async Task<T> ReadAsync(Guid id)
        {
            _storage.TryGetValue(id, out var value);
            return value;
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return _storage.Values.ToList();
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return _storage.Values.Skip((page - 1) * amount).Take(amount).ToList();
        }

        public async Task<bool> UpdateAsync(T element)
        {
            var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
            _storage[id] = element;
            return true;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
            return _storage.TryRemove(id, out _);
        }

        public async Task<bool> SaveAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var json = JsonSerializer.Serialize(_storage.Values);
                using (var writer = new StreamWriter(_filePath, false, System.Text.Encoding.UTF8))
                {
                    await writer.WriteAsync(json);
                }
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
