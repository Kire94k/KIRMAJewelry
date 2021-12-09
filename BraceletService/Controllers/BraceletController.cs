using BraceletService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using System.Security.Cryptography.X509Certificates;

namespace BraceletService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BraceletController : ControllerBase
    {
        X509Certificate2 clientCertificate = new X509Certificate2("C:\\Users\\Anders\\AppData\\Roaming\\Microsoft\\SystemCertificates\\My\\Certificates\\776433949301DAE57813ECE1EE7F063DF71DAAF6");
        private DocumentStore store;

        public BraceletController()
        {
            store = new DocumentStore
            {
                Urls = new[] { "https://a.free.kirma.ravendb.cloud/" },
                Certificate = clientCertificate,
                Database = "Bracelets"
            };

            store.Initialize();
        }

        [HttpGet(Name = "GetAllBracelets")]
        public async Task<IEnumerable<Bracelet>> Get()
        {

            using (var session = store.OpenAsyncSession())
            {
                var bracelets = await session.Query<Bracelet>().Where(s => s.Id != null).ToListAsync();
                return bracelets;
            }
        }

        [HttpPost(Name = "CreateBracelet")]
        public async Task<IEnumerable<Bracelet>> Post(Bracelet bracelet)
        {

            using (var session = store.OpenAsyncSession())
            {
                var newBracelet = new Bracelet
                {
                    Name = bracelet.Name,
                    Price = bracelet.Price,
                    Description = bracelet.Description ?? "no description",
                    Material = bracelet.Material ?? "no material",
                    Designer = bracelet.Designer ?? "no designer",
                    Length = bracelet.Length,
                    Pendant = bracelet.Pendant
                };

                await session.StoreAsync(newBracelet);
                await session.SaveChangesAsync();

                return await session.Query<Bracelet>().Where(s => s.Id != null).ToListAsync();
            }

            
        }

        [HttpDelete(Name = "DeleteBracelet")]
        public IEnumerable<Bracelet> Delete(string Id)
        {
   

            using (var session = store.OpenSession())
            {
                var bracelet = session.Load<Bracelet>(Id);
                session.Delete(bracelet);
                session.SaveChanges();
                return session.Query<Bracelet>().Where(s => s.Id != null).ToList();
            }
        }
    }
}