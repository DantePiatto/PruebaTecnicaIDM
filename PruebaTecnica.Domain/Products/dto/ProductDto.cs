
namespace PruebaTecnica.Domain.Products.dto;
using System.Text.Json.Serialization;

public class ProductDto
    {
        [JsonPropertyName("productId")]
        public long ProductId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        // Ojo al nombre con S
        [JsonPropertyName("groupAttributes")]
        public List<GroupAttributeDto> GroupAttributes { get; set; } = new();
    }