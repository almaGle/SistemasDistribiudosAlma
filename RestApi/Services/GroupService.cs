using Microsoft.Extensions.Configuration.UserSecrets;
using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;
public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task<GroupUserModel> GetGroupByIdAsync(string Id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(Id, cancellationToken);
        if (group is null)
        {
            return null;
        }
        return new GroupUserModel
        {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(group.Users.Select(userId => _userRepository.GetByIdAsync(userId, cancellationToken))))
                        .Where(userModel => userModel != null)
                        .ToList()
        };
    }

    public async Task<IEnumerable<GroupUserModel>> GetGroupsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetByNameAsync(name, cancellationToken);
        return groups.Select(group => new GroupUserModel
        {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate
        });
    }

    public async Task<List<GroupModel>> GetByNameAsync(
        string name, 
        int pageIndex, 
        int pageSize, 
        string orderBy, 
        CancellationToken cancellationToken)
    {
        return await _groupRepository.GetGroupsByNameAsync(name, pageIndex, pageSize, orderBy, cancellationToken);
    }
}
