using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    List<Monkey> monkeyList = new();
    HttpClient httpClient;
    public MonkeyService()
    {
        this.httpClient = new HttpClient();
    }
    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeyList?.Count> 0)
              return monkeyList;
        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json");

        if (response.IsSuccessStatusCode)
        {
            monkeyList = await response.Content.ReadFromJsonAsync(MonkeyContext.Default.ListMonkey);
        }

        return monkeyList;
    }
}
