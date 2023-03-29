using SkyRadio.Client.Identity.Services.Interfaces;
using SkyRadio.Domain.Commons;
using SkyRadio.Domain.Entities;
using System.Dynamic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SkyRadio.Client.Identity.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoginAsync(string username, string password)
        {
            try
            {
                var p = new Playload
                {
                    Buffer = new byte[1024],
                    BuffuredDataLength = 1024,
                    ChannelId = Guid.NewGuid().ToString(),
                };
                var result = await _httpClient.PostAsJsonAsync("/identiy/login", p);
                var get = await _httpClient.GetFromJsonAsync<Response<object>>("");
                if (result.IsSuccessStatusCode)
                {
                    var  json = await result.Content.ReadAsStringAsync();
                    var response = JsonSerializer.Deserialize<Response<object>>(json);
                    
                }
                else
                {
                    if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
