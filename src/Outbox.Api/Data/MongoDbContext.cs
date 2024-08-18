using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Outbox.Api.Configs.Databases;
using Outbox.Api.Data.Helpers;
using Outbox.Api.Domain.Repository;

namespace Outbox.Api.Data;

public class MongoDbContext <TDocument> : IMongoRepository<TDocument>
    where TDocument : Document
{
    private readonly IMongoCollection<TDocument> _collection;
    
    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var database = new MongoClient(settings.Value.GetConnectionString())
            .GetDatabase(settings.Value.DatabaseName);
        
        var collectionName = typeof(TDocument).Name;
        _collection = database.GetCollection<TDocument>(collectionName);
    }

    public async Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression) 
        => (await _collection.FindAsync(filterExpression)).ToEnumerable();

    public async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        var documentsPromise = await _collection.FindAsync(filterExpression);
        return await documentsPromise.FirstOrDefaultAsync();
    }

    public async Task<bool> InsertAsync(TDocument document)
    {
        try
        {
            await _collection.InsertOneAsync(document);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> ReplaceOneAsync(TDocument document)
    {
        try
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
