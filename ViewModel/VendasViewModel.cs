using Intelectah.Models;
using System.ComponentModel.DataAnnotations;

namespace Intelectah.ViewModel
{
    public class VendasViewModel
    {
        public int VendaId { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        [Display(Name = "Cliente")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "A data da venda é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data da Venda")]
        public DateTime DataVenda { get; set; }

        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [Display(Name = "Usuário")]
        public int UsuarioID { get; set; }

        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }

        [Display(Name = "Nome do Usuário")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "A concessionária é obrigatória.")]
        [Display(Name = "Concessionária")]
        public int ConcessionariaID { get; set; }

        [Display(Name = "Nome da Concessionária")]
        public string NomeConcessionaria { get; set; }
        
        public virtual ICollection<UsuariosModel> Usuarios { get; set; }
        public virtual ICollection<VeiculosModel> Veiculos { get; set; }
        public virtual ICollection<ConcessionariasModel> Concessionarias { get; set; }

        public VendasViewModel()
        {
        }

        public VendasViewModel(int vendaId, int clienteId, DateTime dataVenda, decimal valorTotal, int usuarioId, string nomeCliente, string nomeUsuario, string nomeConcessionaria)
        {
            VendaId = vendaId;
            ClienteID = clienteId;
            DataVenda = dataVenda;
            ValorTotal = valorTotal;
            UsuarioID = usuarioId;
            NomeCliente = nomeCliente;
            NomeUsuario = nomeUsuario;
            NomeConcessionaria = nomeConcessionaria;
        }
    }
}
