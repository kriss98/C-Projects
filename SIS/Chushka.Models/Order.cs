namespace Chushka.Models
{
    using System;
    public class Order : BaseModel<Guid>
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ClientId { get; set; }

        public User Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}