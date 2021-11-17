using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIRMAJewelry.Models
{
    public abstract class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Material { get; set; }

        public string Designer { get; set; }
    }
}
