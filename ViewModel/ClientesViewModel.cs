namespace Intelectah.ViewModel
{
    public class ClientesViewModel
    {
        public int ClienteID { get; set; }

        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
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
