using System.Linq.Expressions;

namespace Outbox.Api.Domain.Repository;

public interface IMongoRepository<TDocument>
{
    Task<IEnumerable<TDocument>> FilterByAsync(
         Expression<Func<TDocument, bool>> filterExpression);

    Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

    Task<bool> InsertAsync(TDocument document);

    Task<bool> ReplaceOneAsync(TDocument document);
}