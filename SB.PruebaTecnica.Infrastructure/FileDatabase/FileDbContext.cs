

using System.Text;
using Microsoft.Extensions.Configuration;

namespace SB.PruebaTecnica.Infrastructure.FileDatabase;

    public class FileDbContext
    {
        private readonly string _filePath;

        public FileDbContext(IConfiguration configuration)
        {
            var print = Directory.GetCurrentDirectory();
            string rootPath = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;

            string basePath = configuration["FileSettings:BasePath"] ?? "FileDatabase";
            string fileName = configuration["FileSettings:FileName"] ?? "entities.txt";

            _filePath = Path.Combine(rootPath +  basePath, fileName);
            
            if (!File.Exists(_filePath))
                File.Create(_filePath).Close();
        }

        public List<string> ReadAllLines(){
           try
            {
                return File.Exists(_filePath) ? File.ReadAllLines(_filePath, Encoding.UTF8).ToList() : new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                return new List<string>();
            }
        } 

        public void WriteAllLines(List<string> lines) => File.WriteAllLines(_filePath, lines);
    }
