using MongoDB.Driver;
using RestApi.Infrastructure.Mongo;
using RestApi.Mappers;
using RestApi.Models;
using MongoDB.Bson;

}
namespace RestApi.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMongoCollection<GroupEntity> _groups;

        public GroupRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            var database = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDb:Groups:DatabaseName"));
            _groups = database.GetCollection<GroupEntity>(configuration.GetValue<string>("MongoDb:Groups:CollectionName"));
        }

        public async Task<GroupModel> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<GroupEntity>.Filter.Eq(x => x.Id, Id);
                var group = await _groups.Find(filter).FirstOrDefaultAsync(cancellationToken);
                return group.ToModel();
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<GroupModel>> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var filter = Builders<GroupEntity>.Filter.Regex(x => x.Name, new BsonRegularExpression(name, "i")); // Búsqueda por coincidencia parcial
            var groups = await _groups.Find(filter).ToListAsync(cancellationToken);
            return groups.Select(group => group.ToModel());
        }

        public async Task<List<GroupModel>> GetGroupsByNameAsync(
    string name, 
    int pageIndex, 
    int pageSize, 
    string orderBy, 
    CancellationToken cancellationToken)
{
    var filter = Builders<GroupEntity>.Filter.Regex(g => g.Name, new BsonRegularExpression(name, "i")); // Usar GroupEntity aquí

    var sortDefinition = orderBy switch
    {
        "name" => Builders<GroupEntity>.Sort.Ascending(g => g.Name), // Cambiar a GroupEntity
        "creationDate" => Builders<GroupEntity>.Sort.Ascending(g => g.CreatedAt), // Cambiar a GroupEntity
        _ => Builders<GroupEntity>.Sort.Ascending(g => g.Name) // Ordenar por defecto por nombre
    };

    var groups = await _groups
        .Find(filter)
        .Sort(sortDefinition)
        .Skip((pageIndex - 1) * pageSize) 
        .Limit(pageSize)
        .ToListAsync(cancellationToken);

    return groups.Select(g => g.ToModel()).ToList(); 
}
    }
    }
