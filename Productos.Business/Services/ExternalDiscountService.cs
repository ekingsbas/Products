using Products.Business.Contracts;

namespace Products.Business.Services
{
    public class ExternalDiscountService : IExternalDiscountService
    {
        private readonly HttpClient _httpClient;

        public ExternalDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetDiscountAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://api.example.com/discounts");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                int discount = int.Parse(content);
                return discount;
            }
            else
            {
                throw new Exception("Error retrieving discount from external API.");
            }
        }
    }
}
