using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intelectah.Models
{
    public class ClientesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos.")]
        public string CPF { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string Telefone { get; set; }

        public ICollection<VendasModel> Vendas { get; set; }

        public bool FezCompras { get; set; } = false;

        public ClientesModel()
        {
            Vendas = new List<VendasModel>();
        }

        public ClientesModel(string nome, string cpf, string telefone, string email)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            FezCompras = false; 
            Email = email;
            Vendas = new List<VendasModel>(); 
        }

    }
}
