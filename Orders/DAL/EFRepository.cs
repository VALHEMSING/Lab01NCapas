using DAL.Datos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{
    public class EFRepository : IRepository, IDisposable
    {
         ApplicationDbContext _context;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool disposedValue;
        public async Task<TEntity> CreateAsync<TEntity>(TEntity toCreate) where TEntity : class
        {
            TEntity Resul = default(TEntity);
            try
            {
                await _context.Set<TEntity>().AddAsync(toCreate);
                await _context.SaveChangesAsync();
                Resul = toCreate;
            }
            catch (DbUpdateException ex)
            {
                // Manejo de excepciones específicas
                throw new Exception("An error occurred while creating the entity.", ex);
            }
            return Resul;
        }

        public async Task<bool> DeleteAsync<TEntity>(TEntity toDelete) where TEntity : class
        {

            bool Result = false;
            try
            { 
                _context.Entry<TEntity>(toDelete).State = EntityState.Deleted;
                Result = await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the entity.", ex);
            }
            return Result;

        }

        public async Task<List<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            List<TEntity> Resul = default(List<TEntity>);

            try
            {
                Resul =  await _context.Set<TEntity>().Where(criteria).ToListAsync();
            }
            catch (DbException ex)
            {
                throw new Exception("An error occurred while filtering the entities.", ex);
            }
            return Resul;
        }

        public async Task<TEntity> RetrieveAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity Result = null;
            try
            {
                Result = await _context.Set<TEntity>().FirstOrDefaultAsync(criteria);
            }
            catch (DbException ex)
            {
                throw new Exception("An error occurred while retrieving the entity.", ex);
            }
            return Result;
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity toUpdate) where TEntity : class
        {
            bool Result = false;
            try
            {
                _context.Entry(toUpdate).State = EntityState.Modified;
                Result = await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
            return Result;
        }

        public void Dispose()
        {
            if( _context != null )
            {
                _context.Dispose();
            }
        }
    }
}
