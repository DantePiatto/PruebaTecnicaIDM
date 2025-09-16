
using SB.PruebaTecnica.Application.Abstractions.Messaging;
namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.UpdateEntidadGubernamental;

public sealed record UpdateEntidadGubernamentalCommand(
    int Id,
    string Nombre,
    string Ubicacion
) : ICommand<int>;