namespace MishMash.Models
{
    using System.Collections.Generic;

    using MishMash.Models.Enums;

    public class Channel : BaseModel<int>
    {
        public Channel()
        {
            this.Followers = new HashSet<UserChannel>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public ChannelType Type { get; set; }

        public string Tags { get; set; }

        public ICollection<UserChannel> Followers { get; set; }
    }
}