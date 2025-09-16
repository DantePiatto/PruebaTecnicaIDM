
using SB.PruebaTecnica.Application.Abstractions.Messaging;
namespace SB.PruebaTecnica.Application.EntidadesGubernamentales.DeleteEntidadGubernamental;

public sealed record DeleteEntidadGubernamentalCommand(
    int Id
) : ICommand<int>;