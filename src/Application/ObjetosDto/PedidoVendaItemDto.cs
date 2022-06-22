namespace Application.ObjetosDto
{
    public class PedidoVendaItemDto
    {
        /// <summary>
        /// Obtém ou define Nome
        /// </summary>
        public long IdProduto { get; set; }

        /// <summary>
        /// Obtém ou define Valor
        /// </summary>
        public double Valor { get; set; }

        /// <summary>
        /// Obtém ou define Quantidade
        /// </summary>
        public uint Quantidade { get; set; }
    }
}