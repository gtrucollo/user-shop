namespace Application.ObjetosDto
{
    public class PedidoVendaDto
    {
        /// <summary>
        /// Obtém ou define Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Obtém ou define ValorTotal
        /// </summary>
        public double ValorTotal { get; set; }

        /// <summary>
        /// Obtém ou define Quantidade
        /// </summary>
        public uint Quantidade { get; set; }

        /// <summary>
        /// Obtém ou define Items
        /// </summary>
        public List<PedidoVendaItemDto> Items { get; set; } = new();
    }
}