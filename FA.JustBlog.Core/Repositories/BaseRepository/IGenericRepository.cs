using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories.BaseRepository
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Find <typeparamref name="T"/> by ID
        /// </summary>
        /// <param name="id">ID of <typeparamref name="T"/></param>
        /// <returns><typeparamref name="T"/></returns>
        T GetById(Guid id);

        /// <summary>
        /// Find <typeparamref name="T"/> by ID
        /// </summary>
        /// <param name="id">ID of <typeparamref name="T"/></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Add a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        int Add(T entity);

        /// <summary>
        /// Add a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        Task<int> AddAsync(T entity);

        /// <summary>
        /// Update a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        bool Update(T entity);

        /// <summary>
        /// Update a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Delete a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        bool Delete(Guid id);

        /// <summary>
        /// Delete a <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Get all <typeparamref name="T"/>
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        List<T> GetAll();

        /// <summary>
        /// Get all <typeparamref name="T"/>
        /// </summary>
        /// <returns>List of <typeparamref name="T"/></returns>
        Task<List<T>> GetAllAsync();
    }
}
