namespace MeTube.Models
{
    using System.Collections.Generic;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.Tubes = new HashSet<Tube>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<Tube> Tubes { get; set; }
    }
}