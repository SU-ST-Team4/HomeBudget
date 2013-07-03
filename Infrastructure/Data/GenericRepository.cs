using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Web;
using Core.Models;
using Core.Data;
using Core.Models.Authentication;
using Core.Models.Budget;

namespace Infrastructure.Data
{
    /// <summary>
    /// Generic Repository used to manipulate entities db operations.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        #region Fields

        protected static HomeBudgetContext _context;
        protected DbSet<TEntity> _dbSet;

        #endregion Fields

        #region Constructors

        public GenericRepository(HomeBudgetContext context)
        {
            GenericRepository<TEntity>._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get entities by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        /// <summary>
        /// Get an entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }
        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Insert(TEntity entity)
        {
            return _dbSet.Add(entity).Id;
        }
        /// <summary>
        /// Delete an entity by id
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = _context.Entry<TEntity>(entityToUpdate);

            if (entry.State == EntityState.Detached)
            {
                TEntity attachedEntity = _dbSet.Find(entityToUpdate.Id);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entityToUpdate);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
            /*_dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;*/
        }
        /// <summary>
        /// Saves the changes that were done
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion Methods
    }
}
