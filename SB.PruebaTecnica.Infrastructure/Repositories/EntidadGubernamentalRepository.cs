using SB.PruebaTecnica.Domain.Entities;
using SB.PruebaTecnica.Domain.Interfaces;
using SB.PruebaTecnica.Infrastructure.FileDatabase;

namespace SB.PruebaTecnica.Infrastructure.Repositories
{
    public class EntidadGubernamentalRepository : IEntidadGubernamentalRepository
    {
        private readonly FileDbContext _fileDbContext;

        public EntidadGubernamentalRepository(FileDbContext fileDbContext)
        {
            _fileDbContext = fileDbContext;
        }

        public List<EntidadGubernamental> GetAll()
        {
            return _fileDbContext.ReadAllLines()
                .Select(line => ConvertToEntity(line))
                .ToList();
        }

        public EntidadGubernamental? GetById(int id)
        {
            var entidades = GetAll();
            
            return entidades.FirstOrDefault(e => e.Id == id);
        }

        public void Add(EntidadGubernamental entidad)
        {
            var lines = _fileDbContext.ReadAllLines();
            lines.Add($"{entidad.Id}|{entidad.Nombre}|{entidad.Ubicacion}");
            _fileDbContext.WriteAllLines(lines);
        }

        public void Update(EntidadGubernamental entidad)
        {
            var lines = GetAll().Where(e => e.Id != entidad.Id).ToList();
            lines.Add(entidad);
            _fileDbContext.WriteAllLines(lines.Select(e => $"{e.Id}|{e.Nombre}|{e.Ubicacion}").ToList());
        }

        public void Delete(int id)
        {
            var lines = GetAll().Where(e => e.Id != id).ToList();
            _fileDbContext.WriteAllLines(lines.Select(e => $"{e.Id}|{e.Nombre}|{e.Ubicacion}").ToList());
        }

        private EntidadGubernamental ConvertToEntity(string line)
        {
            var parts = line.Split('|');
            return new EntidadGubernamental
            {
                Id = int.Parse(parts[0]),
                Nombre = parts[1],
                Ubicacion = parts[2]
            };
        }

        public int? GetLastId()
        {
            var entidades = GetAll();

            if (entidades.Count == 0)
                return null; 

            return entidades.Max(e => e.Id);
        }
    }
}
