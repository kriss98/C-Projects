namespace FDMC.Services
{
    using System;
    using System.Collections.Generic;

    using FDMC.Data;
    using FDMC.Models;
    using FDMC.Models.Enums;
    using FDMC.Services.Base;
    using FDMC.Services.Contracts;
    using FDMC.ViewModels.Kittens;

    public class KittenService : BaseService, IKittenService
    {
        public KittenService(FDMCContext context)
            : base(context)
        {
        }

        public void AddKitten(KittenViewModel model)
        {
            var kitten = new Kitten
                             {
                                 Name = model.Name,
                                 Age = model.Age,
                                 Breed = (Breed)Enum.Parse(typeof(Breed), model.Breed, true)
                             };

            this.context.Kittens.Add(kitten);
            this.context.SaveChanges();
        }

        public ICollection<KittenViewModel> GetAllKittens()
        {
            var kittens = this.context.Kittens;

            var kittenModels = new List<KittenViewModel>();
            foreach (var kitten in kittens)
            {
                var kittenViewModel =
                    new KittenViewModel
                        {
                            Name = kitten.Name,
                            Age = kitten.Age,
                            Breed = kitten.Breed.ToString()
                        };

                if (kitten.Breed == Breed.AmericanShorthair) kittenViewModel.ImageUrl = ImageContext.AmericanShorthair;
                else if (kitten.Breed == Breed.Munchkin)
                    kittenViewModel.ImageUrl = ImageContext.Munchkin;
                else if (kitten.Breed == Breed.Siamese)
                    kittenViewModel.ImageUrl = ImageContext.Siamese;
                else if (kitten.Breed == Breed.StreetTranscended)
                    kittenViewModel.ImageUrl = ImageContext.StreetTranscended;

                kittenModels.Add(kittenViewModel);
            }

            return kittenModels;
        }
    }
}