
namespace SB.PruebaTecnica.Domain.Products.dto;

using System.Text.Json.Serialization;

public class GroupAttributeDto
{
    [JsonPropertyName("groupAttributeId")]
    public string? GroupAttributeId { get; set; }

    [JsonPropertyName("groupAttributeType")]
    public GroupAttributeTypeDto? GroupAttributeType { get; set; }

    // viene con "o" y "I" invertidas en el PDF (descripIon)
    [JsonPropertyName("description")]
    public string? DescripIon { get; set; }

    // idem: quanItyInformaIon
    [JsonPropertyName("quantityInformation")]
    public QuantityInformationDto? QuantityInformaIon { get; set; }

    // idem: aSributes
    [JsonPropertyName("attributes")]
    public List<AttributeOptionDto> Attributes { get; set; } = new();

    [JsonPropertyName("order")]
    public int Order { get; set; }
}

public class GroupAttributeTypeDto
{
    [JsonPropertyName("groupAttributeTypeId")]
    public string? GroupAttributeTypeId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}


public class QuantityInformationDto
    {
        // groupASributeQuanIty
        [JsonPropertyName("groupAttributeQuantity")]
        public int GroupAttributeQuantity { get; set; }

        [JsonPropertyName("showPricePerProduct")]
        public bool ShowPricePerProduct { get; set; }

        [JsonPropertyName("isShown")]
        public bool IsShown { get; set; }

        [JsonPropertyName("isEditable")]
        public bool IsEditable { get; set; }

        [JsonPropertyName("isVerified")]
        public bool IsVerified { get; set; }

        [JsonPropertyName("verifyValue")]
        public string? VerifyValue { get; set; }
    }

    public class AttributeOptionDto
    {
        [JsonPropertyName("productId")]
        public long ProductId { get; set; }

        [JsonPropertyName("attributeId")]
        public long AttributeId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        // defaultQuanIty
        [JsonPropertyName("defaultQuantity")]
        public int DefaultQuanIty { get; set; }

        // maxQuanIty
        [JsonPropertyName("maxQuantity")]
        public int MaxQuanIty { get; set; }

        [JsonPropertyName("priceImpactAmount")]
        public decimal PriceImpactAmount { get; set; }

        [JsonPropertyName("isRequired")]
        public bool IsRequired { get; set; }

        // negaIveASributeId (string o null en tu JSON)
        [JsonPropertyName("negativeAttributeId")]
        public string? NegativeAttributeId { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("statusId")]
        public string? StatusId { get; set; }

        [JsonPropertyName("urlImage")]
        public string? UrlImage { get; set; }
    }