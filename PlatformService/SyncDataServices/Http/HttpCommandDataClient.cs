using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        this.configuration = configuration;
    }

    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
        var httpContent = new StringContent(
            content: JsonSerializer.Serialize(plat),
            encoding: Encoding.UTF8,
            mediaType: "application/json"
        );

        var response = await httpClient.PostAsync($"{configuration["CommandsService"]}", httpContent);
        
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to CommandsService was OK!");
        }
        else
        {
            Console.WriteLine("--> Sync POST to CommandsService was NOT OK!");
        }
    }
}