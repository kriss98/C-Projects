namespace FDMC.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum Breed
    {
        [Display(Name = "Street Transcended")]
        StreetTranscended = 0,

        [Display(Name = "American Shorthair")]
        AmericanShorthair = 1,

        Munchkin = 2,

        Siamese = 3
    }
}