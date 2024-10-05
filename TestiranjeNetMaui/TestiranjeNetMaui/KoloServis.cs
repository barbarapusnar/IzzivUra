using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestiranjeNetMaui
{
    public  class KoloServis
    {
        HttpClient httpClient;
        public KoloServis()
        {
            this.httpClient = new HttpClient();
        }

        List<Kolo> kolesaList;
        public async Task<List<Kolo>> GetKolesa()
        {
            if (kolesaList?.Count > 0)
                return kolesaList;

            // Online
            var response = await httpClient.GetAsync("http://challenger.scng.si/api/kolesa");
            if (response.IsSuccessStatusCode)
            {
                kolesaList = await response.Content.ReadFromJsonAsync(KolesaContext.Default.ListKolo);
            }

            // Offline
            /*using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            monkeyList = JsonSerializer.Deserialize(contents, MonkeyContext.Default.ListMonkey);*/

            return kolesaList;
        }
    }
}
