using PruebaTecnica.Domain.Abstractions;

using MediatR;

namespace PruebaTecnica.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> 
: IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}