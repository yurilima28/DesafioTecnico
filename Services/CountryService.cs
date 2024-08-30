using Intelectah.Models;
using Newtonsoft.Json;

namespace Intelectah.Services
{

    public class CountryService
    {
        private readonly HttpClient _httpClient;

        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CountryModel>> GetCountriesAsync()
        {
            var response = await _httpClient.GetStringAsync("https://restcountries.com/v3.1/all");
            var countries = JsonConvert.DeserializeObject<List<CountryModel>>(response);
            return countries;
        }
    }

}