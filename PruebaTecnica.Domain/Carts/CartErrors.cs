using PruebaTecnica.Domain.Abstractions;

namespace PruebaTecnica.Domain.Products;

public static class CartErrors
{
    public static Error NotFound = new(400, "Esta  no existe");
    public static Error ItExists = new(400, "Esta  ya existe, ingrese otro.");
}