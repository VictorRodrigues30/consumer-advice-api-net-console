using System.Text.Json;

const string endpoint = "https://api.adviceslip.com/advice";

using HttpClient client = new();

try
{
    string json = await client.GetStringAsync(endpoint);

    AdviceResponse? response = JsonSerializer.Deserialize<AdviceResponse>(
        json,
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

    if (response?.Slip?.Advice is not null)
    {
        Console.WriteLine("Conselho de Hoje:");
        Console.WriteLine(response.Slip.Advice);
    }
    else
    {
        Console.WriteLine("Não foi possível obter o conselho.");
    }
}
catch (HttpRequestException)
{
    Console.WriteLine("Não foi possível acessar a API. Verifique sua conexão com a internet.");
}
catch (JsonException)
{
    Console.WriteLine("A API retornou uma resposta em formato inesperado.");
}

public class AdviceResponse
{
    public AdviceSlip? Slip { get; set; }
}

public class AdviceSlip
{
    public int Id { get; set; }
    public string? Advice { get; set; }
}
