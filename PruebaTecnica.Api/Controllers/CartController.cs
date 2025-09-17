using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SB.NombreProyecto.Domain.DTOs;
using PruebaTecnica.Api.Utils;
using PruebaTecnica.Application.EntidadesGubernamentales.CreateEntidadGubernamental;

namespace PruebaTecnica.Api.Controllers;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/cart")]
public class CartController : Controller
{

    private readonly ISender _sender;

    public CartController(ISender sender)
    {
        _sender = sender;
    }


    // [AllowAnonymous]
    // [ApiVersion(ApiVersions.V1)]
    // [HttpGet("get-by-id/{id}")]
    // public async Task<ActionResult<EntidadGubernamentalDto>> GetById(int id)
    // {
    //     var request = new GetByIdEntidadGubernamentalQuery { Id = id };
    //     var results = await _sender.Send(request);

    //     return Ok(results);
    // }

    // [AllowAnonymous]
    // [ApiVersion(ApiVersions.V1)]
    // [HttpGet("get-all")]
    // public async Task<ActionResult<EntidadGubernamentalDto>> GetAll()
    // {
    //     var request = new GetAllEntidadGubernamentalQuery { };
    //     var results = await _sender.Send(request);

    //     return Ok(results);
    // }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("create")]
    public async Task<ActionResult<EntidadGubernamentalDto>> Create(
        [FromBody] CreateCartCommand command
    )
    {
        var results = await _sender.Send(command);

        return Ok(results);
    }

    // [AllowAnonymous]
    // [ApiVersion(ApiVersions.V1)]
    // [HttpPut("update")]
    // public async Task<ActionResult<EntidadGubernamentalDto>> Update(
    //     [FromBody] UpdateEntidadGubernamentalCommand command
    // )
    // {
    //     var results = await _sender.Send(command);

    //     return Ok(results);
    // }

    // [AllowAnonymous]
    // [ApiVersion(ApiVersions.V1)]
    // [HttpDelete("Delete/{Id}")]
    // public async Task<ActionResult<EntidadGubernamentalDto>> Delete(
    //     int Id
    // )
    // {  
    //     var command = new DeleteEntidadGubernamentalCommand(Id);
    //     var results = await _sender.Send(command);

    //     return Ok(results);
    // }
}