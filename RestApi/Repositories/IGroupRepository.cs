using RestApi.Models;

namespace RestApi.Repositories;

public interface IGroupRepository
{
    Task<GroupModel> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<GroupModel>> GetByNameAsync(string name, CancellationToken cancellationToken); 
    Task<List<GroupModel>> GetGroupsByNameAsync(
        string name, 
        int pageIndex, 
        int pageSize, 
        string orderBy, 
        CancellationToken cancellationToken);
}
