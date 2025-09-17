
using MediatR;
using PruebaTecnica.Domain.Abstractions;

namespace PruebaTecnica.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}