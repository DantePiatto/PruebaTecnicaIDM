using SB.PruebaTecnica.Domain.Entities;

namespace SB.PruebaTecnica.Domain.Interfaces
{
    public interface IEntidadGubernamentalRepository
    {
        List<EntidadGubernamental> GetAll();
        EntidadGubernamental? GetById(int id);
        int? GetLastId();
        void Add(EntidadGubernamental entity);
        void Update(EntidadGubernamental entity);
        void Delete(int id);
    }
}
