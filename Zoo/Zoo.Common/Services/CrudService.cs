using System;
using System.Collections.Generic;
using System.Linq;

namespace Zoo.Common.Services
{
	public class CrudService<T> : ICrudService<T> where T : class
	{
		private readonly Dictionary<Guid, T> _storage = new Dictionary<Guid, T>();

		public void Create(T element)
		{
			var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
			_storage[id] = element;
		}

		public T Read(Guid id)
		{
			return _storage.TryGetValue(id, out var value) ? value : null;
		}

		public IEnumerable<T> ReadAll()
		{
			return _storage.Values;
		}

		public void Update(T element)
		{
			var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
			if (_storage.ContainsKey(id))
				_storage[id] = element;
		}

		public void Remove(T element)
		{
			var id = (Guid)typeof(T).GetProperty("Id").GetValue(element);
			_storage.Remove(id);
		}
	}
}
