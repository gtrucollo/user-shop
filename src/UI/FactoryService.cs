using UI.Services;

namespace UI
{
    public class FactoryService
    {
        private readonly HttpClient _httpClient;

        private ProdutoService _produto;

        public FactoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("accept", "*/*");
        }

        public ProdutoService Produto
        {
            get
            {
                _produto ??= new(_httpClient);

                return _produto;
            }
        }
    }
}