
using SB.PruebaTecnica.Application.Abstractions.Messaging;
using SB.PruebaTecnica.Domain.Abstractions;
using SB.PruebaTecnica.Domain.Entities;
using SB.PruebaTecnica.Domain.Interfaces;
using SB.PruebaTecnica.Domain.Products;

namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.CreateEntidadGubernamental;

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