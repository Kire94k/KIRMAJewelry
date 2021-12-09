using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using KIRMAJewelry.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KIRMAJewelry.Services
{
    public class NecklaceService : INecklaceService
    {
        private readonly HttpClient Http;

        private readonly IMessagingService _MessagingService;

        private Necklace[]? necklaces = null;

        public NecklaceService(HttpClient client, IMessagingService service)
        {
            Http = client;
            _MessagingService = service;
            

        }

        public async Task<Necklace[]> GetNecklaces()
        {

            Http.DefaultRequestHeaders.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7046/Necklace");

            var responseMessage = await Http.SendAsync(request);

            var necklaces = JsonConvert.DeserializeObject<List<Necklace>>(responseMessage.Content.ReadAsStringAsync().Result);

            return necklaces.ToArray() ?? new Necklace[0];


        }
        public async Task<Necklace[]> Add(string NecklaceName)
        {

            Http.DefaultRequestHeaders.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7046/Necklace");


            var newNecklace = new Necklace() { Id = "default", Name = NecklaceName };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(newNecklace), Encoding.UTF8, "application/json");
            request.Content = jsonContent;
            var responseMessage = await Http.SendAsync(request);

            var necklaces = JsonConvert.DeserializeObject<List<Necklace>>(responseMessage.Content.ReadAsStringAsync().Result);

            return necklaces.ToArray();
        }
        public async Task<Necklace[]> Delete(Necklace necklace)
        {
            Http.DefaultRequestHeaders.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:7046/Necklace");

            var jsonContent = new StringContent(necklace.Id, Encoding.UTF8, "application/json");
            request.Content = jsonContent;
            var responseMessage = await Http.SendAsync(request);

            var necklaces = JsonConvert.DeserializeObject<List<Necklace>>(responseMessage.Content.ReadAsStringAsync().Result);

            return necklaces.ToArray();
        }

        public Necklace[] Search(string text)
        {
            return necklaces.Where(x => x.Name.ToLower()
            .Contains(text.Trim().ToLower())).ToArray();
        }

    }
}