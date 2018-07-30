namespace PedidosNet.Models.VO
{
    public class ClienteVO : AbstractVO
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public decimal Telefone { get; set; } 
        public string Senha { get; set; }
    }
}
