using Microsoft.AspNetCore.Mvc;
using NecklaceService.Models;
using Raven.Client.Documents;
using System.Security.Cryptography.X509Certificates;

namespace NecklaceService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NecklaceController : ControllerBase
    {
        X509Certificate2 clientCertificate = new X509Certificate2("C:\\Users\\Anders\\AppData\\Roaming\\Microsoft\\SystemCertificates\\My\\Certificates\\776433949301DAE57813ECE1EE7F063DF71DAAF6");
        private DocumentStore store;

        public NecklaceController()
        {
            store = new DocumentStore
            {
                Urls = new[] { "https://a.free.kirma.ravendb.cloud/" },
                Certificate = clientCertificate,
                Database = "Necklaces"
            };

            store.Initialize();
        }

        [HttpGet(Name = "GetAllNecklaces")]
        public async Task<IEnumerable<Necklace>> Get()
        {

            using (var session = store.OpenAsyncSession())
            {
                var Necklaces = await session.Query<Necklace>().Where(s => s.Id != null).ToListAsync();
                return Necklaces;
            }
        }

        [HttpPost(Name = "CreateNecklace")]
        public async Task<IEnumerable<Necklace>> Post(Necklace necklace)
        {

            using (var session = store.OpenAsyncSession())
            {
                var newNecklace = new Necklace
                {
                    Name = necklace.Name,
                    Price = necklace.Price,
                    Description = necklace.Description ?? "no description",
                    Material = necklace.Material ?? "no material",
                    Designer = necklace.Designer ?? "no designer",
                    Length = necklace.Length,
                    Pendant = necklace.Pendant
                };

                await session.StoreAsync(newNecklace);
                await session.SaveChangesAsync();

                return await session.Query<Necklace>().Where(s => s.Id != null).ToListAsync();
            }


        }

        [HttpDelete(Name = "DeleteBracelet")]
        public IEnumerable<Necklace> Delete(string Id)
        {


            using (var session = store.OpenSession())
            {
                var bracelet = session.Load<Necklace>(Id);
                session.Delete(bracelet);
                session.SaveChanges();
                return session.Query<Necklace>().Where(s => s.Id != null).ToList();
            }
        }
    }

}
