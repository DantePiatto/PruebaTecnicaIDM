using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Domain.Common;

namespace PruebaTecnica.Domain.Entities
{
    public class EntidadGubernamental : BaseEntity
    {
        public EntidadGubernamental()
        {

        }
        public EntidadGubernamental(Guid id, string nombre, string ubicacion)
        {
            Id = id;
            Nombre = nombre;
            Ubicacion = ubicacion;
        }
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }


        public Result Update(
            string nombre,
            string ubicacion
        )
        {
            Nombre = nombre;
            Ubicacion = ubicacion;
            return Result.Success();
        }

    }
}
