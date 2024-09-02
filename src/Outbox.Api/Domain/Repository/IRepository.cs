using System.Linq.Expressions;

namespace Outbox.Api.Domain.Repository;

public interface IRepository
{
    #region Queries

    IQueryable<T> QueryAsNoTracking<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
    IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
    IQueryable<T> QueryIncludeStringProperties<T>(params string[] includeProperties) where T : class;

    #endregion

    #region Commands

    Task AddAsync<T>(T entity) where T : class;
    Task AddRange<T>(IEnumerable<T> items) where T : class;
    void Update<T>(T entity) where T : class;
    void Remove<T>(T item) where T : class;
    void RemoveRange<T>(IEnumerable<T> items) where T : class;
    Task<bool> CommitAsync();

    #endregion

    #region Transactions

    Task BeginTransactionAsync();
    void RollbackTransaction();
    void CommitTransaction();

    #endregion

}