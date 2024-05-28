using System;

namespace Lab.ChaosEngineering.Domain.Interfaces.Repository
{
	public interface IRepositoryBase<TEntity> where TEntity : class
	{
		void Add(TEntity obj);
		Task<TEntity> AddAsync(TEntity obj);
		Task<List<TEntity>> AddRangeAsync(List<TEntity> obj);
		TEntity? GetById(Guid id);
		Task<TEntity?> GetByIdAsync(Guid id);
		Task<IEnumerable<TEntity>> GetAll();
		void Update(TEntity obj);
		Task UpdateAsync(TEntity obj);
		void Remove(TEntity obj);
		Task RemoveAsync(TEntity obj);
		void Dispose();
	}
}