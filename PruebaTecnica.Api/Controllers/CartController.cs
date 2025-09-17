using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Api.Utils;
using PruebaTecnica.Domain.Carts;
using PruebaTecnica.Application.Carts.UpdateCart;
using PruebaTecnica.Application.Carts.ChangeCartItemQuantity;
using PruebaTecnica.Application.Carts.CreateCartItem;
using PruebaTecnica.Application.Carts.GetAllCartItems;
using PruebaTecnica.Application.Carts.DeleteCartItem;

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




    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all")]
    public async Task<ActionResult<CartDto>> GetAll()
    {
        var request = new GetAllCartItemsQuery { };
        var results = await _sender.Send(request);

        return Ok(results);
    }



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


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpDelete("delete/{Id}")]
    public async Task<ActionResult<string>> Delete(
        string Id
    )
    {

        var command = new DeleteCartItemCommand(Guid.Parse(Id));

        var results = await _sender.Send(command);

        return Ok(results);
    }
}