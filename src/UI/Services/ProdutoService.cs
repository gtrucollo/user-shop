using UI.ObjetosDto;
using UI.Services.Base;

namespace UI.Services;
public class ProdutoService : ServiceBase
{
    private const string serviceName = "Produto";

    public ProdutoService(HttpClient httpClient)
        : base (httpClient)
    {        
    }

    public async Task<List<ProdutoDto>> SelecionarTodos()
    {
        return await this.EfetuarRequisicaoGetAsync<List<ProdutoDto>>($"{serviceName}/SelecionarTodos");
    }

    public async Task<ProdutoDto> SelecionarPorId(long id)
    {
        return await this.EfetuarRequisicaoGetAsync<ProdutoDto>($"{serviceName}/SelecionarPorId/{id}");
    }

    public async Task<ProdutoDto> Adicionar(ProdutoDto produtoDto)
    {
        return await this.EfetuarRequisicaoPostAsync<ProdutoDto>($"{serviceName}/Adicionar/", produtoDto);
    }

    public async Task<ProdutoDto> Atualizar(long id, ProdutoDto produtoDto)
    {
        return await this.EfetuarRequisicaoPostAsync<ProdutoDto>($"{serviceName}/Atualizar/{id}", produtoDto);
    }

    public async Task<ProdutoDto> Remover(long id)
    {
        return await this.EfetuarRequisicaoGetAsync<ProdutoDto>($"{serviceName}/Remover/{id}");
    }
}
