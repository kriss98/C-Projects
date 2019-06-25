namespace MishMash.Models
{
    using System.Collections.Generic;

    using MishMash.Models.Enums;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.FollowedChannels = new HashSet<UserChannel>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<UserChannel> FollowedChannels { get; set; }

        public UserRole Role { get; set; }
    }
}