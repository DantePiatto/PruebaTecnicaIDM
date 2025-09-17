
using MediatR;
using PruebaTecnica.Domain.Abstractions;

namespace PruebaTecnica.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}

public interface IBaseCommand
{}