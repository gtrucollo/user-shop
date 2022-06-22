using System.Net.Http.Json;

namespace UI.Services.Base;
public class ServiceBase
{
    private readonly HttpClient _httpClient;

    public ServiceBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public virtual async Task<T> EfetuarRequisicaoGetAsync<T>(string nomeServico)
    {
        HttpResponseMessage resposta;
        try
        {
            resposta = await _httpClient.GetAsync(nomeServico);
        }
        catch (HttpRequestException exp)
        {
            throw new Exception($"{exp.StatusCode}");
        }

        if (!resposta.IsSuccessStatusCode)
        {
            throw new Exception($"{resposta.StatusCode}, {await resposta.Content.ReadAsStringAsync()}");
        }

        return await resposta.Content.ReadFromJsonAsync<T>();
    }

    public virtual async Task EfetuarRequisicaoGetAsync(string nomeServico)
    {
        HttpResponseMessage resposta;
        try
        {
            resposta = await _httpClient.GetAsync(nomeServico);
        }
        catch (HttpRequestException exp)
        {
            throw new Exception($"{exp.StatusCode}");
        }

        if (!resposta.IsSuccessStatusCode)
        {
            throw new Exception($"{resposta.StatusCode}, {await resposta.Content.ReadAsStringAsync()}");
        }
    }

    public virtual async Task<T> EfetuarRequisicaoPostAsync<T>(string nomeServico, object dados)
    {
        HttpResponseMessage resposta;
        try
        {
            resposta = await _httpClient.PostAsJsonAsync(nomeServico, dados);
        }
        catch (HttpRequestException exp)
        {
            throw new Exception($"{exp.StatusCode}");
        }

        if (!resposta.IsSuccessStatusCode)
        {
            throw new Exception($"{resposta.StatusCode}, {await resposta.Content.ReadAsStringAsync()}");
        }

        return await resposta.Content.ReadFromJsonAsync<T>();
    }
}