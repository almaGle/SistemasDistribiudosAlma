using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using RestApi.Services;
using RestApi.Mappers;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;
    
    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupResponse>> GetGroupById(string id, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByIdAsync(id, cancellationToken);
        if (group is null)
        {
            return NotFound();
        }
        return Ok(group.ToDto());
    }

   [HttpGet]
public async Task<ActionResult<IEnumerable<GroupResponse>>> GetGroupsByName(
    [FromQuery] string name, 
    [FromQuery] int pageIndex = 1, 
    [FromQuery] int pageSize = 10, 
    [FromQuery] string orderBy = "name", 
    CancellationToken cancellationToken = default) // Coloca cancellationToken al final
{
    var groups = await _groupService.GetByNameAsync(name, pageIndex, pageSize, orderBy, cancellationToken);
    
    if (groups == null || !groups.Any())
    {
        return Ok(new List<GroupResponse>());  // Retorna lista vacía si no hay resultados
    }
    
    var groupResponses = groups.Select(group => group.ToDto()).ToList();
    
    return Ok(groupResponses);
}

}
