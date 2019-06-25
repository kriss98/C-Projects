namespace Chushka.Models
{
    using System.Collections.Generic;

    using Chushka.Models.Enums;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
        }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}