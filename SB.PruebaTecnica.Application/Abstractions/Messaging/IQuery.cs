
using MediatR;
using SB.PruebaTecnica.Domain.Abstractions;

namespace SB.PruebaTecnica.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}