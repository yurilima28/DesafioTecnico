using System.ComponentModel.DataAnnotations;

namespace Intelectah.ViewModel
{
    public class ClientesViewModel
    {
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do cliente não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF do cliente é obrigatório.")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O telefone do cliente é obrigatório.")]
        [MaxLength(15, ErrorMessage = "O telefone não pode exceder 15 caracteres.")]
        public string Telefone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }
        public ClientesViewModel() { }

        public ClientesViewModel(int clienteID, string nome, string cpf, string telefone, string email)
        {
            ClienteID = clienteID;
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;  
        }
    }
}
