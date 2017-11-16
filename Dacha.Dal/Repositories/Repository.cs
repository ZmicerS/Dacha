using Dacha.Dal.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Dacha.Dal.Repositories
{
    /// <summary>
    ///     Repository realisation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _db = null;
        private DbSet<T> _dbSet = null;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            _db = context;
            _dbSet = _db.Set<T>();
        }

        /// <summary>
        ///     Gets entity by Id
        /// </summary>
        /// <param name="Id">Id of entity</param>
        /// <returns>entity, if found by Id</returns>
        public virtual T GetById(object Id)
        {
            return _dbSet.Find(Id);
        }

        /// <summary>
        ///     Fetches all objects
        /// </summary>
        /// <returns>IQueryable interace for fetching objects</returns>
        public virtual IQueryable<T> GetAll()
        {            
            return this._dbSet;
        }

        /// <summary>
        ///     Fetches all objects without tracking
        /// </summary>
        /// <returns>IQueryable interace for fetching objects</returns>
        public virtual IQueryable<T> GetAllWithoutTracking()
        {
            return this._dbSet.AsNoTracking<T>();
        }


        /// <summary>
        ///     Fetches objects with condition
        /// </summary>
        /// /// <param name="where">The where.</param>
        /// <returns>IQueryable interace for fetching objects</returns>
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> condition, string includeProperties = "")        
        {           
            IQueryable<T> query = _dbSet;
            if (condition != null)
            {
                query = query.Where(condition);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.AsQueryable();
        }

        /// <summary>
        ///     Insert object in database
        /// </summary>
        /// <param name="obj">object to update</param>
        public virtual void Insert(T obj)
        {
            _dbSet.Add(obj);
        }

        /// <summary>
        ///     Updates object in database
        /// </summary>
        /// <param name="obj">object to update</param>
        public virtual void Update(T obj)
        {
            _dbSet.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }

        /// <summary>
        ///     Deletes object from database
        /// </summary>
        /// <param name="Id">Id of an object to delete</param>
        public virtual void Delete(Object Id)
        {
            T getObjById = _dbSet.Find(Id);
            if (getObjById != null)
            {
              
                Delete(getObjById);
            }
        }

        /// <summary>
        ///     Deletes object from database
        /// </summary>
        /// <param name="entityToDelete">object to delete</param>
        public virtual void Delete(T entityToDelete)
        {
            if (_db.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

    }

}

