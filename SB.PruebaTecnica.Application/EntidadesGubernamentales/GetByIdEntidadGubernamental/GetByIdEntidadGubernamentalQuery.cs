
using SB.NombreProyecto.Domain.DTOs;
using SB.PruebaTecnica.Application.Abstractions.Messaging;

namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.GetByIdEntidadGubernamental;
public sealed record GetByIdEntidadGubernamentalQuery : IQuery<EntidadGubernamentalDto>
{
    public int Id { get; set; }
}