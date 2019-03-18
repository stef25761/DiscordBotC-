using System.Net.Http;

namespace DiscordAuthBot.Resources.GW2API
{

    public class HTTPSingelton
    {
        private const string baseURL = "https://api.guildwars2.com/v2/";
        private HttpClient _client;
        public HTTPSingelton()
        {

        }
    }
}
