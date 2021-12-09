namespace NecklaceService.Models
{
    public abstract class Product
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        public string? Material { get; set; }

        public string? Designer { get; set; }
    }

}
