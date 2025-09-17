using PruebaTecnica.Domain.Abstractions;

namespace PruebaTecnica.Domain.Products;

public static class ProductErrors
{
    public static Error NotFound = new(400, "Esta producto no existe");
    public static Error ItExists = new(400, "Esta producto ya existe, ingrese otro.");
}