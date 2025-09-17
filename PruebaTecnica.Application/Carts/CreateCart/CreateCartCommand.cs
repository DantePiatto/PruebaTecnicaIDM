
using PruebaTecnica.Application.Abstractions.Messaging;

namespace PruebaTecnica.Application.EntidadesGubernamentales.CreateEntidadGubernamental;

public sealed record CreateCartCommand(
    string Nombre,
    string Ubicacion
) : ICommand<int>;