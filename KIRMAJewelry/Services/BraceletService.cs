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

        private readonly IMessagingService _MessagingService;

        private Bracelet[] bracelets = null;

        public BraceletService(HttpClient client, IMessagingService service)
        {
            Http = client;
            _MessagingService = service;

        }

        public async Task<Bracelet[]> GetBracelets()
        {
            if (bracelets == null)
            {
                await _MessagingService.Add("Bracelet Service: bracelet fetched");
                bracelets = await Http.GetFromJsonAsync<Bracelet[]>("sample-data/bracelets.json");
            }
                return bracelets; 
        }
        public async Task<Bracelet[]> Add(string BraceletName)
        {
            var braceletList = new List<Bracelet>(bracelets);
            var sortedIndexes = braceletList.OrderByDescending(x => x.Id);
            var currentIndex = sortedIndexes.First().Id;
            var newBracelet = new Bracelet() { Name = BraceletName, Id = ++currentIndex };
            braceletList.Add(newBracelet);
            bracelets = await Task.Factory.StartNew(()=> braceletList.ToArray());
            return bracelets;
        }
        public async Task<Bracelet[]> Delete(Bracelet bracelet)
        {
            bracelets = await Task.Factory.StartNew(() =>
            bracelets.Where(x => x != bracelet).ToArray()
            );
            return bracelets;
        }

        public Bracelet[] Search(string text)
        {
            return bracelets.Where(x => x.Name.ToLower()
            .Contains(text.Trim().ToLower())).ToArray();
        }

    }  
}
