
using MediatR;
using SB.PruebaTecnica.Domain.Abstractions;

namespace SB.PruebaTecnica.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}

public interface IBaseCommand
{}