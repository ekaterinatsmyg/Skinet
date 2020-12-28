using Core.Interfaces;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;
using System.Linq;
using Infrastructure.Data.SpecificationEvaluators;

namespace Infrastructure.Data.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _context;
		public GenericRepository(StoreContext context)
		{
			_context = context;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).ToListAsync();
		}

		public async Task<T> GetEntityWithSpec(ISpecification<T> specificaion)
		{
			return await ApplySpecification(specificaion).FirstOrDefaultAsync();
		}


		public async Task<int> CountAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).CountAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecification<T> specification)
		{
			return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
		}
	}
}
