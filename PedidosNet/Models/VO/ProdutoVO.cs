namespace PedidosNet.Models.VO
{
    public class ProdutoVO : AbstractVO
    {
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public decimal ValorUnitario { get; set; }
        public int QuantidadeEstoque { get; set; } 
    }
}
