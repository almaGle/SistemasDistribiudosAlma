using RestApi.Models;

namespace RestApi.Services;


public interface IGroupService
{
    Task<GroupUserModel> GetGroupByIdAsync(string Id, CancellationToken cancellationToken);
    Task<IEnumerable<GroupUserModel>> GetGroupsByNameAsync(string name, CancellationToken cancellationToken); // Este método debe estar definido
    Task<List<GroupModel>> GetByNameAsync(string name, int pageIndex, int pageSize, string orderBy, CancellationToken cancellationToken); // Este método también debe estar definido
}

public interface IGroupService{
    Task<GroupUserModel> GetGroupByIdAsync (string Id, CancellationToken cancellationToken);

    Task<List<GroupModel>> GetGroupsByNameAsync(string name, CancellationToken cancellationToken);
}
}
