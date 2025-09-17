using PruebaTecnica.Domain.Abstractions;

namespace PruebaTecnica.Domain.Errors;

public static class EntidadGubernamentalErrors
{
    public static Error NotFound = new(400, "Esta entidad goburnamental no existe");
    public static Error ItExists = new(400, "Esta entidad goburnamental ya existe, ingrese otro.");
}