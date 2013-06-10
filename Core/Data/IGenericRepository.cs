using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using Core.Models;
using Core.Models.Authentication;
using Core.Models.Budget;

namespace Core.Data
{
    /// <summary>
    /// Generic Repository used to manipulate entities db operations.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> 
        where TEntity : IEntity
    {
        /// <summary>
        /// Get entities by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        /// <summary>
        /// Get an entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetByID(object id);
        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        int Insert(TEntity entity);
        /// <summary>
        /// Delete an entity by id
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Delete(TEntity entityToDelete);
        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity entityToUpdate);
        /// <summary>
        /// Saves the changes that were done
        /// </summary>
        void SaveChanges();

        // Badddddddddd
        UserProfile GetUserProfile(string Username);
        BudgetCategory GetBudgetCategory(int BudgetCategoryId);
        RecurrentBudget GetRecurrentBudget(int RecurrentBudgetId);
    }
}
