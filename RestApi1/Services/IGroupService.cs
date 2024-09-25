using RestApi.Models;

namespace RestApi.Services;

public interface IGroupService{
    Task<GroupUserModel> GetGroupByIdAsync (string Id, CancellationToken cancellationToken);

    Task<List<GroupModel>> GetGroupsByNameAsync(string name, CancellationToken cancellationToken);
}