using System.ComponentModel.DataAnnotations;

namespace Intelectah.Models
{
    public class FabricantesModel
    {
        [Key]
        public int FabricanteID { get; set; }

        [Required]
        [MaxLength(100)]
        [StringLength(100, ErrorMessage = "O nome do fabricante não pode exceder 100 caracteres.")]
        public string NomeFabricante { get; set; }

        [Required]
        [MaxLength(50)]
        [StringLength(50, ErrorMessage = "O país de origem não pode exceder 50 caracteres.")]
        public string PaisOrigem { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O ano de fundação deve ser um ano válido.")]
        public int AnoFundacao { get; set; }

    }
}
