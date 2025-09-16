using SB.PruebaTecnica.Domain.Abstractions;

using MediatR;

namespace SB.PruebaTecnica.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> 
: IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}