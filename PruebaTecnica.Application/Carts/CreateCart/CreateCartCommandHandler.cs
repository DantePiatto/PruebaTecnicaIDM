
using PruebaTecnica.Application.Abstractions.Messaging;
using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Domain.Products;

namespace PruebaTecnica.Application.EntidadesGubernamentales.CreateEntidadGubernamental;

internal class CreateCartCommandHandler : ICommandHandler<CreateCartCommand, int>
{
    private readonly IProductRepository _productoRepository;

    public CreateCartCommandHandler(
        IProductRepository productoRepository
    )
    {
        _productoRepository = productoRepository;
    }

    public async Task<Result<int>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        // var newId = _entidadGubernamentalRepository.GetLastId() + 1;
        // var entidadGubernamental = new EntidadGubernamental(
        //     newId ?? 0,
        //     request.Nombre,
        //     request.Ubicacion
        // );

        // _entidadGubernamentalRepository.Add(entidadGubernamental);

        return Result.Success(1, Message.Create);

    }

}