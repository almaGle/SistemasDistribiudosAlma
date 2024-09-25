using Microsoft.AspNetCore.Http.HttpResults;
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
        if(group is null)
        {
            return NotFound();
        }
        return Ok(group.ToDto());
    }
    [HttpGet]
public async Task<ActionResult<List<GroupResponse>>> GetGroupsByName([FromQuery] string name, CancellationToken cancellationToken)
{
    var groups = await _groupService.GetGroupsByNameAsync(name, cancellationToken);
    
    if(groups == null || groups.Count == 0)
    {
        return Ok(new List<GroupResponse>());  // Si no se encontraron coincidencias, retorna una lista vacía
    }
    
    return Ok(groups.Select(group => group.ToDto()).ToList());
}
}