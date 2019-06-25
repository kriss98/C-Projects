namespace MeTube.Models
{
    using System.ComponentModel;

    public class Tube : BaseModel<int>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string YoutubeId { get; set; }

        [DefaultValue(0)]
        public int Views { get; set; }

        public int UploaderId { get; set; }

        public User Uploader { get; set; }
    }
}