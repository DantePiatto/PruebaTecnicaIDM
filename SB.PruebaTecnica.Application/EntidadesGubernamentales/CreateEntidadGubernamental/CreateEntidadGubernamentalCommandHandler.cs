
using SB.PruebaTecnica.Application.Abstractions.Messaging;
using SB.PruebaTecnica.Domain.Abstractions;
using SB.PruebaTecnica.Domain.Entities;
using SB.PruebaTecnica.Domain.Interfaces;

namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.CreateEntidadGubernamental;

internal class CreateEntidadGubernamentalCommandHandler : ICommandHandler<CreateEntidadGubernamentalCommand, int>
{
    private readonly IEntidadGubernamentalRepository _entidadGubernamentalRepository;

    public CreateEntidadGubernamentalCommandHandler(
        IEntidadGubernamentalRepository entidadGubernamentalRepository
    )
    {
        _entidadGubernamentalRepository = entidadGubernamentalRepository;
    }

    public async Task<Result<int>> Handle(CreateEntidadGubernamentalCommand request, CancellationToken cancellationToken)
    {
        var newId = _entidadGubernamentalRepository.GetLastId() + 1;
        var entidadGubernamental = new EntidadGubernamental(
            newId ?? 0,
            request.Nombre,
            request.Ubicacion
        );

        _entidadGubernamentalRepository.Add(entidadGubernamental);

        return Result.Success(entidadGubernamental.Id!, Message.Create);

    }

}