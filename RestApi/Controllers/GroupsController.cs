

using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using RestApi.Services;
using RestApi.Mappers;

using RestApi.Exceptions;
using System.Net;


namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]

[Authorize]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;
    
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet("{id}")]

    [Authorize(Policy="Read")]
    
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
   [Authorize(Policy="Read")]

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
[HttpDelete("id")]
[Authorize(Policy="Write")]


public async Task<IActionResult> DeleteGroup(string id, CancellationToken cancellationToken)
{
    try{
        await _groupService.DeleteGroupByIdAsync(id, cancellationToken);
        return NoContent();
    }
    catch(GroupNotFoundException){
        return NotFound();
    }
}
[HttpPost]

[Authorize(Policy="Write")]

public async Task<ActionResult<GroupResponse>> CreateGroup([FromBody]CreateGroupRequest groupRequest,
 CancellationToken cancellationToken){
    try{
        var group =await _groupService.CreateGroupAsync(groupRequest.Name, groupRequest.Users, cancellationToken);
        return CreatedAtAction(nameof(GetGroupById), new { id = group.Id }, group.ToDto);
    }catch(InvalidGroupRequestFormatException){
        
        return BadRequest(NewValidationProblemDetails("One or more validation errors occured.", 
        HttpStatusCode.BadRequest, new Dictionary<string, string[]>{
                {"Groups", ["Users array is empy"]}
            }));
    }
    catch(GroupAlreadyExistsException){
        return Conflict(NewValidationProblemDetails("One or more validation errors occured.", 
        HttpStatusCode.BadRequest, new Dictionary<string, string[]>{
                {"Groups", ["Group with same name already exist"]}
            }));
    }
 }
 private static ValidationProblemDetails NewValidationProblemDetails(string title, HttpStatusCode statusCode, Dictionary<string, string[]> errors){
    return new ValidationProblemDetails{
        Title = title,
        Status = (int)statusCode,
        Errors = errors
    };
 }


 [HttpPut("{id}")]
 [Authorize(Policy="Write")]

    public async Task<IActionResult> UpdateGroup(string id, [FromBody] UpdateGroupRequest groupRequest, CancellationToken cancellationToken){
        try{
            await _groupService.UpdateGroupAsync(id, groupRequest.Name, groupRequest.Users, cancellationToken);
            return NoContent(); 
        }catch(GroupNotFoundException){
            return NotFound();
        }catch(InvalidGroupRequestFormatException){
            return BadRequest(NewValidationProblemDetails("One or more validation errors occured.", HttpStatusCode.BadRequest, new Dictionary<string, string[]>{
                {"Groups", ["Users array is empy"]}
            }));
        }catch(GroupAlreadyExistsException){
            return Conflict(NewValidationProblemDetails("One or more validation errors occured.", HttpStatusCode.Conflict, new Dictionary<string, string[]>{
                {"Groups", ["Group with same name already exists"]}
            }));
        }
    }



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
