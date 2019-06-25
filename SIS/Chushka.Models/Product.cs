namespace Chushka.Models
{
    using System.Collections.Generic;

    using Chushka.Models.Enums;

    public class Product : BaseModel<int>
    {
        public Product()
        {
            this.Orders = new HashSet<Order>();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}