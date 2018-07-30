namespace PedidosNet.Models.VO
{
    public class PedidoProdutoVO : AbstractVO
    {
        private ProdutoVO produto { get; set; }
        private PedidoVO pedido { get; set; }

        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; } 

        public ProdutoVO Produto
        {
            set { produto = value; }

            get
            {
                if (produto == null)
                    produto = new ProdutoVO();

                return produto;
            }
        }

        public PedidoVO Pedido
        {
            set { pedido = value; }

            get
            {
                if (pedido == null)
                    pedido = new PedidoVO();

                return pedido;
            }
        }


    }
}
