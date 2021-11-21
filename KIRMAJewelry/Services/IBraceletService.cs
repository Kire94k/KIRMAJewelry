using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KIRMAJewelry.Models;

namespace KIRMAJewelry.Services
{
    interface IBraceletService
    {
        Task<Bracelet[]> GetBracelets();

        Task<Bracelet[]> Add(string BraceletName);

        Task<Bracelet[]> Delete(Bracelet bracelet);
    }
}
