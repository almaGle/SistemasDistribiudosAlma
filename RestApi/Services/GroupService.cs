using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    public GroupService(IGroupRepository groupRepository){
        _groupRepository = groupRepository;
    }
    public async Task<GroupUserModel> GetGroupByIdAsync(string Id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(Id, cancellationToken);
        if(group is null){
            return null;
        }
        return new GroupUserModel{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate
        };
    }
    public async Task<List<GroupModel>> GetGroupsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _groupRepository.FindGroupsByNameAsync(name, cancellationToken);
    }
}