namespace PedidosNet.Models.VO
{
    public class PedidoVO : AbstractVO
    {
        private ClienteVO cliente { get; set; }

        public System.DateTime? DataPedido { get; set; }

        public bool? Efetuado { get; set; }

        public decimal ValorPedido { get; set; }

        public ClienteVO Cliente
        {
            set { cliente = value; } 

            get
            {
                if (cliente == null)
                    cliente = new ClienteVO();

                return cliente;
            }
        }
    }
}
