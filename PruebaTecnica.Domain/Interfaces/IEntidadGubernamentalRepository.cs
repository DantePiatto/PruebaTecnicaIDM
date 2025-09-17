using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Domain.Interfaces
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
