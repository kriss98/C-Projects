namespace FDMC.Services.Contracts
{
    using System.Collections.Generic;

    using FDMC.ViewModels.Kittens;

    public interface IKittenService
    {
        void AddKitten(KittenViewModel model);

        ICollection<KittenViewModel> GetAllKittens();
    }
}