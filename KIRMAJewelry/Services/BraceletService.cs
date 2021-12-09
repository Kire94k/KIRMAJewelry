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
    public class BraceletService : IBraceletService
    {
        private readonly HttpClient Http;

        private readonly IMessagingService _MessagingService;

        private Bracelet[]? bracelets = null;

        public BraceletService(HttpClient client, IMessagingService service)
        {
            Http = client;
            _MessagingService = service;
           

        }

        public async Task<Bracelet[]> GetBracelets()
        {
           
            Http.DefaultRequestHeaders.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Get, " http://localhost:5231/Bracelet");

            var responseMessage = await Http.SendAsync(request);

            var bracelets = JsonConvert.DeserializeObject<List<Bracelet>>(responseMessage.Content.ReadAsStringAsync().Result);
                
            return bracelets.ToArray() ?? new Bracelet[0];

                 
        }
        public async Task<Bracelet[]> Add(string BraceletName)
        {

            Http.DefaultRequestHeaders.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5231/Bracelet");

            
            var newBracelet = new Bracelet() { Id = "default", Name = BraceletName};

            var jsonContent = new StringContent(JsonConvert.SerializeObject(newBracelet), Encoding.UTF8, "application/json");
            request.Content = jsonContent;
            var responseMessage = await Http.SendAsync(request);

            var bracelets = JsonConvert.DeserializeObject<List<Bracelet>>(responseMessage.Content.ReadAsStringAsync().Result);

            return bracelets.ToArray();
        }
        public async Task<Bracelet[]> Delete(Bracelet bracelet)
        {
            Http.DefaultRequestHeaders.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:5231/Bracelet");

            var jsonContent = new StringContent(bracelet.Id, Encoding.UTF8, "application/json");
            request.Content = jsonContent;
            var responseMessage = await Http.SendAsync(request);

            var bracelets = JsonConvert.DeserializeObject<List<Bracelet>>(responseMessage.Content.ReadAsStringAsync().Result);

            return bracelets.ToArray();
        }

        public Bracelet[] Search(string text)
        {
            return bracelets.Where(x => x.Name.ToLower()
            .Contains(text.Trim().ToLower())).ToArray();
        }

    }  
}
