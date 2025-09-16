
using AutoMapper;
using SB.NombreProyecto.Domain.DTOs;
using SB.PruebaTecnica.Application.Abstractions.Messaging;
using SB.PruebaTecnica.Domain.Abstractions;
using SB.PruebaTecnica.Domain.Errors;
using SB.PruebaTecnica.Domain.Interfaces;

namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.GetByIdEntidadGubernamental;
internal sealed class GetByIdEntidadGubernamentalQueryHandler : IQueryHandler<GetByIdEntidadGubernamentalQuery, EntidadGubernamentalDto>
{
    private readonly IEntidadGubernamentalRepository _entidadGubernamentalRepository;

    private readonly IMapper _mapper;

    public GetByIdEntidadGubernamentalQueryHandler(
        IEntidadGubernamentalRepository entidadGubernamentalRepository,
        IMapper mapper
    )
    {
        _entidadGubernamentalRepository = entidadGubernamentalRepository;
        _mapper = mapper;
    }

    public async Task<Result<EntidadGubernamentalDto>> Handle(GetByIdEntidadGubernamentalQuery request, CancellationToken cancellationToken)
    {
        
        var entidadGubernamental = _entidadGubernamentalRepository.GetById(request.Id);

        if(entidadGubernamental is null){
            return Result.Failure<EntidadGubernamentalDto>(EntidadGubernamentalErrors.NotFound)!;
        }

        var entidadGubernamentalDto = _mapper.Map<EntidadGubernamentalDto>(entidadGubernamental);

        return entidadGubernamentalDto!;
        
    }

}