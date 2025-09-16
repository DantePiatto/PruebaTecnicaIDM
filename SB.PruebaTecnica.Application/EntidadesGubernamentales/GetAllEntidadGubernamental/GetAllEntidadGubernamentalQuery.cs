
using SB.NombreProyecto.Domain.DTOs;
using SB.PruebaTecnica.Application.Abstractions.Messaging;

namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.GetAllEntidadGubernamental;
public sealed record GetAllEntidadGubernamentalQuery : IQuery<List<EntidadGubernamentalDto>>
{
}