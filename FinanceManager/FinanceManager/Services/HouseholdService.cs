using FinanceManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Services
{
    public class HouseholdService
    {
        private readonly HttpClient httpClient;

        public HouseholdService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Household> GetHouseholdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            var uri = new Uri($"/household/{userId}", UriKind.Relative);

            var result = await httpClient.GetAsync(uri);

            if (result.StatusCode is HttpStatusCode.NotFound)
                return null;

            var content = JsonConvert.DeserializeObject<Household>(await result.Content.ReadAsStringAsync());
            return content;
        }

        public async Task<Household> CreateHouseholdAsync(Household household)
        {
            if (household is null)
                throw new ArgumentNullException(nameof(household));

            var uri = new Uri("/household", UriKind.Relative);

            var response = await httpClient.PostAsync(uri, BuildPostContent(household));

            if (response.StatusCode is HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Household>(await response.Content.ReadAsStringAsync());

            return null;
        }

        private static StringContent BuildPostContent(object data)
        {
            string serializedData = JsonConvert.SerializeObject(data);

            return new StringContent(serializedData, Encoding.UTF8, "application/json");
        }
    }
}
