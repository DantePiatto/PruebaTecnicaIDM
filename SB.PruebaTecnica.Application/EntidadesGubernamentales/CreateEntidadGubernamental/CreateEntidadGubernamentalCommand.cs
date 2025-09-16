
using SB.PruebaTecnica.Application.Abstractions.Messaging;

namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.CreateEntidadGubernamental;

public sealed record CreateEntidadGubernamentalCommand(
    string Nombre,
    string Ubicacion
) : ICommand<int>;