using System;
using System.Linq;
using System.Linq.Expressions;


namespace Dacha.Dal.Interfaces
{
    /// <summary>
    ///     interface for repository for particular entity
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        ///     Fetches all objects
        /// </summary>
        /// <returns>IQueryable interace for fetching objects</returns>
        IQueryable<T> GetAll();

        /// <summary>
        ///     Fetches all objects without tracking
        /// </summary>
        /// <returns>IQueryable interace for fetching objects</returns>
        IQueryable<T> GetAllWithoutTracking();

        /// <summary>
        ///     Fetches objects with condition
        /// </summary>
        /// /// <param name="where">The where.</param>
        /// <returns>IQueryable interace for fetching objects</returns>
        IQueryable<T> Get(Expression<Func<T, bool>> condition = null, string includeProperties = "");

        /// <summary>
        ///     Gets entity by id
        /// </summary>
        /// <param name="Id">Id of entity</param>
        /// <returns>entity, if found by Id</returns>
        T GetById(object Id);

        /// <summary>
        ///     Insert object in database
        /// </summary>
        /// <param name="obj">object to update</param>
        void Insert(T obj);

        /// <summary>
        ///     Updates object in database
        /// </summary>
        /// <param name="obj">object to update</param>
        void Update(T obj);

        /// <summary>
        ///     Deletes object from database
        /// </summary>
        /// <param name="Id">Id of an object to delete</param>
        void Delete(Object Id);

        /// <summary>
        ///     Deletes object from database
        /// </summary>
        /// <param name="entityToDelete">object to delete</param>
        void Delete(T entityToDelete);
    }
}
