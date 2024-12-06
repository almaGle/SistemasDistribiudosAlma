using MongoDB.Driver;
using RestApi.Infrastructure.Mongo;
using RestApi.Mappers;
using RestApi.Models;
using MongoDB.Bson;


namespace RestApi.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly IMongoCollection<GroupEntity> _groups;
    public GroupRepository(IMongoClient mongoClient, IConfiguration configuration){
        var database = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDb:Groups:DatabaseName"));
        _groups = database.GetCollection<GroupEntity>(configuration.GetValue<string>("MongoDb:Groups:CollectionName"));
    }
    public async Task<GroupModel> GetByIdAsync(string Id, CancellationToken cancellationToken)
    {
        try{
            var filter = Builders<GroupEntity>.Filter.Eq(x => x.Id, Id);
            var group = await _groups.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return group.ToModel();
        }catch(FormatException){
            return null;
        }
    }
    public async Task<List<GroupModel>> FindGroupsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var filter = Builders<GroupEntity>.Filter.Regex("Name", new BsonRegularExpression(name, "i"));  // Búsqueda por coincidencia en nombre, sin distinción de mayúsculas/minúsculas
        var groups = await _groups.Find(filter).ToListAsync(cancellationToken);
        
        return groups.Select(group => group.ToModel()).ToList();
    }
}