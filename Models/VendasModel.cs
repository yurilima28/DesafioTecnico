using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Intelectah.ViewModel;

namespace Intelectah.Models
{
    public class VendasModel
    {
        public int VendaId { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        [ForeignKey("Cliente")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "A data da venda é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataVenda { get; set; }

        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }

        public int UsuarioID { get; set; }
        public int ConcessionariaID { get; set; }

        public virtual UsuariosModel Usuario { get; set; }
        public virtual ClientesModel Cliente { get; set; }
        public virtual ConcessionariasModel Concessionaria { get; set; }
        public VendasModel() { }

        public VendasModel(int clienteID, decimal valorTotal, DateTime dataVenda, int usuarioID, int concessionarID)
        {
            ClienteID = clienteID;
            ValorTotal = valorTotal;
            DataVenda = dataVenda;
            UsuarioID = usuarioID;
            ConcessionariaID = concessionarID;
        }
    }
}
