
using AutoMapper;
using SB.NombreProyecto.Domain.DTOs;
using SB.PruebaTecnica.Application.Abstractions.Messaging;
using SB.PruebaTecnica.Domain.Abstractions;
using SB.PruebaTecnica.Domain.Interfaces;

namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.GetAllEntidadGubernamental;
internal sealed class GetAllEntidadGubernamentalQueryHandler : IQueryHandler<GetAllEntidadGubernamentalQuery, List<EntidadGubernamentalDto>>
{
    private readonly IEntidadGubernamentalRepository _entidadGubernamentalRepository;

    private readonly IMapper _mapper;

    public GetAllEntidadGubernamentalQueryHandler(
        IEntidadGubernamentalRepository entidadGubernamentalRepository,
        IMapper mapper
    )
    {
        _entidadGubernamentalRepository = entidadGubernamentalRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<EntidadGubernamentalDto>>> Handle(GetAllEntidadGubernamentalQuery request, CancellationToken cancellationToken)
    {
        
        var entidadesGubernamental = _entidadGubernamentalRepository.GetAll();

        var entidadesGubernamentalDto = _mapper.Map<List<EntidadGubernamentalDto>>(entidadesGubernamental);
        
        return entidadesGubernamentalDto!;
        
    }

}