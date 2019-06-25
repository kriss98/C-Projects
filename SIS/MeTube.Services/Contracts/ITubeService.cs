namespace MeTube.Services.Contracts
{
    using System.Collections.Generic;

    using MeTube.ViewModels.Tubes;

    public interface ITubeService
    {
        ICollection<TubeViewModel> GetAllTubes();

        ICollection<TubeDetailsViewModel> GetAllUserTubes(string username);

        DetailsViewModel GetTube(int id);

        void IncrementViews(int id);

        void UploadTube(UploadViewModel model, string username);
    }
}