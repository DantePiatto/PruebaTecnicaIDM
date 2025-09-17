using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Api.Utils;
using PruebaTecnica.Domain.Carts;
using PruebaTecnica.Application.Carts.CreateCart;
using PruebaTecnica.Application.Carts.UpdateCart;
using PruebaTecnica.Application.Carts.ChangeCartItemQuantity;

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

    // [AllowAnonymous]
    // [ApiVersion(ApiVersions.V1)]
    // [HttpPost("create")]
    // public async Task<ActionResult<EntidadGubernamentalDto>> Create(
    //     [FromBody] Cre command
    // )
    // {
    //     var results = await _sender.Send(command);

    //     return Ok(results);
    // }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("items")]
    public async Task<ActionResult<CartDto>> AddItem([FromBody] CreateCartItemCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPut("update")]
    public async Task<ActionResult<CartDto>> UpdateItem([FromBody] UpdateCartItemCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPatch("items/{cartItemId:guid}/quantity")]
    public async Task<ActionResult<CartDto>> ChangeQuantity(
    Guid cartItemId,
    [FromBody] ChangeQuantityDto body,
    IMediator sender)
    {
        if ((body.Quantity is null && body.Delta is null) ||
            (body.Quantity is not null && body.Delta is not null))
            return BadRequest(new { error = "Proporciona 'quantity' o 'delta', no ambos." });

        if (body.Quantity is not null && body.Quantity < 1)
            return BadRequest(new { error = "Quantity debe ser >= 1." });

        var result = await sender.Send(new ChangeCartItemQuantityCommand(
            cartItemId,
            body.Quantity,   // absolute
            body.Delta       // relative
        ));

        return Ok(result);
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