using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Agrega una nueva entidad a la base de datos de manera asíncrona.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de la entidad.</typeparam>
        /// <param name="toCreate">La entidad que se va a agregar.</param>
        /// <returns>La entidad creada.</returns>
        Task<TEntity> CreateAsync<TEntity>(TEntity toCreate) where TEntity : class;

        /// <summary>
        /// Elimina una entidad de la base de datos de manera asíncrona.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de la entidad.</typeparam>
        /// <param name="toDelete">La entidad que se va a eliminar.</param>
        /// <returns>Un valor booleano indicando si la operación fue exitosa.</returns>
        Task<bool> DeleteAsync<TEntity>(TEntity toDelete) where TEntity : class;

        /// <summary>
        /// Actualiza una entidad existente en la base de datos de manera asíncrona.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de la entidad.</typeparam>
        /// <param name="toUpdate">La entidad que se va a actualizar.</param>
        /// <returns>Un valor booleano indicando si la operación fue exitosa.</returns>
        Task<bool> UpdateAsync<TEntity>(TEntity toUpdate) where TEntity : class;

        /// <summary>
        /// Recupera una entidad de la base de datos que coincida con el criterio especificado de manera asíncrona.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de la entidad.</typeparam>
        /// <param name="Criteria">Expresión lambda que define el criterio de búsqueda.</param>
        /// <returns>La entidad que coincide con el criterio, o null si no se encuentra.</returns>
        Task<TEntity> RetrieveAsync<TEntity>(Expression<Func<TEntity, bool>> Criteria) where TEntity : class;

        /// <summary>
        /// Recupera un conjunto de entidades de la base de datos que coincidan con el criterio de búsqueda especificado de manera asíncrona.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de la entidad.</typeparam>
        /// <param name="Criteria">Expresión lambda que define el criterio de búsqueda.</param>
        /// <returns>Una lista de entidades que coinciden con el criterio.</returns>
        Task<List<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> Criteria) where TEntity : class;
    }
}
