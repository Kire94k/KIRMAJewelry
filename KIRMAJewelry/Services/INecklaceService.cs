using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KIRMAJewelry.Models;
namespace KIRMAJewelry.Services
{
    public interface INecklaceService
    {
        Task<Necklace[]> GetNecklaces();

        Task<Necklace[]> Add(string NecklaceName);

        Task<Necklace[]> Delete(Necklace neklace);

        Necklace[] Search(string text);
    }
}
