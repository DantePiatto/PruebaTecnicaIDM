
using SB.PruebaTecnica.Application.Abstractions.Messaging;
using SB.PruebaTecnica.Domain.Abstractions;
using SB.PruebaTecnica.Domain.Entities;
using SB.PruebaTecnica.Domain.Errors;
using SB.PruebaTecnica.Domain.Interfaces;
namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.UpdateEntidadGubernamental;

internal class UpdateEntidadGubernamentalCommandHandler : ICommandHandler<UpdateEntidadGubernamentalCommand, int>
{
    private readonly IEntidadGubernamentalRepository _entidadGubernamentalRepository;

    public UpdateEntidadGubernamentalCommandHandler(
        IEntidadGubernamentalRepository entidadGubernamentalRepository
    )
    {
        _entidadGubernamentalRepository = entidadGubernamentalRepository;
    }

    public async Task<Result<int>> Handle(UpdateEntidadGubernamentalCommand request, CancellationToken cancellationToken)
    {
        var entidadGubernamental = _entidadGubernamentalRepository.GetById(request.Id);
        
        if(entidadGubernamental == null)
        {
            return Result.Failure<int>(EntidadGubernamentalErrors.NotFound);
        }
        entidadGubernamental.Update(request.Nombre, request.Ubicacion);

        _entidadGubernamentalRepository.Update(entidadGubernamental);

        return Result.Success(entidadGubernamental.Id!, Message.Update);

    }

}