namespace EcomPainel.Models
{
    public class Product
    {
        public int Id { get; set; }  // ID do produto
        public string Name { get; set; } = string.Empty;  // Nome do produto
        public string Description { get; set; } = string.Empty;  // Descrição do produto
        public decimal Price { get; set; }  // Preço do produto
        public string Sku { get; set; } = string.Empty;  // SKU do produto
        public string BarCode { get; set; } = string.Empty;  // Código de barras do produto
        public string ImageUrl { get; set; } = string.Empty;  // URL da imagem do produto
        public int CategoryId { get; set; }
        public Category? Category { get; set; } 
    }
}
