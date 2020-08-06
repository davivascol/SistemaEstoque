using System.ComponentModel.DataAnnotations;

namespace EstoqueApi.Domain.Entities
{
    public class ProdutoEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
