using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KIRMAJewelry.Models;

namespace KIRMAJewelry.Services
{
    public class BraceletService : IBraceletService
    {
        private readonly HttpClient Http;
      

        public BraceletService(HttpClient client)
        {
            Http = client;

        }

        public async Task<Bracelet[]> GetBracelets()
        {

            return await Http.GetFromJsonAsync<Bracelet[]>("sample-data/products.json");
        }

    }  
}
