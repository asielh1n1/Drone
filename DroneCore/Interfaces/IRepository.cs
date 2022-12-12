using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Searches a list of elements given a search term
        /// </summary>
        /// <param name="filter">Search term</param>
        /// <returns>Returns a list elements</returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
        /// <summary>
        /// Search for an element given a search term
        /// </summary>
        /// <param name="filter">Search term</param>
        /// <returns>Return element</returns>
        public T FindOne(Expression<Func<T, bool>>? filter = null);
        /// <summary>
        /// Add element
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity);
        /// <summary>
        /// Update element
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity);
        /// <summary>
        /// Remove Element
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity);
    }
}
