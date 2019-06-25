namespace FDMC.Models
{
    using FDMC.Models.Enums;

    public class Kitten : BaseModel<int>
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Breed Breed { get; set; }
    }
}