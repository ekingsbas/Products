using Products.Business.Contracts;
using System.Text.Json;

namespace Products.Business.Services
{
    public class ExternalDiscountService : IExternalDiscountService
    {
        private readonly HttpClient _httpClient;

        public ExternalDiscountService(HttpClient httpClient)
        {
            //_httpClient = httpClient;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<short> GetDiscountAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://www.randomnumberapi.com/api/v1.0/random?min=1&max=100&count=1");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var integers = JsonSerializer.Deserialize<short[]>(responseBody);
                if (integers != null && integers.Length > 0)
                {
                    var discount = integers[0]; 
                                                
                    return discount;
                }
                else
                {
                    throw new InvalidOperationException("Invalid response format from external API.");
                }
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve discount from external API. Status code: {response.StatusCode}");
            }
        }
    }
}
