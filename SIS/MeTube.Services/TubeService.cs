namespace MeTube.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MeTube.Data;
    using MeTube.Models;
    using MeTube.Services.Base;
    using MeTube.Services.Contracts;
    using MeTube.ViewModels.Tubes;

    public class TubeService : BaseService, ITubeService
    {
        public TubeService(MeTubeContext context)
            : base(context)
        {
        }

        public ICollection<TubeViewModel> GetAllTubes()
        {
            var tubes = this.context.Tubes;
            var tubeViewModels = new List<TubeViewModel>();

            foreach (var tube in tubes)
            {
                var tubeViewModel = new TubeViewModel
                                        {
                                            Title = tube.Title,
                                            Author = tube.Author,
                                            YoutubeId = tube.YoutubeId,
                                            Id = tube.Id
                                        };
                tubeViewModels.Add(tubeViewModel);
            }

            return tubeViewModels;
        }

        public ICollection<TubeDetailsViewModel> GetAllUserTubes(string username)
        {
            var userId = this.context.Users.FirstOrDefault(u => u.Username == username).Id;

            var tubes = this.context.Tubes.Where(t => t.UploaderId == userId).ToArray();

            var tubeViewModels = new List<TubeDetailsViewModel>();

            for (var i = 0; i < tubes.Length; i++)
            {
                var tubeViewModel = new TubeDetailsViewModel
                                        {
                                            Author = tubes[i].Author,
                                            Id = tubes[i].Id,
                                            Title = tubes[i].Title,
                                            Index = i + 1
                                        };

                tubeViewModels.Add(tubeViewModel);
            }

            return tubeViewModels;
        }

        public DetailsViewModel GetTube(int id)
        {
            var tube = this.context.Tubes.FirstOrDefault(t => t.Id == id);

            var model = new DetailsViewModel
                            {
                                Author = tube.Author,
                                Title = tube.Title,
                                YoutubeId = tube.YoutubeId,
                                Views = tube.Views,
                                Description = tube.Description
                            };

            return model;
        }

        public void IncrementViews(int id)
        {
            this.context.Tubes.FirstOrDefault(t => t.Id == id).Views++;
            this.context.SaveChanges();
        }

        public void UploadTube(UploadViewModel model, string username)
        {
            var uploader = this.context.Users.FirstOrDefault(u => u.Username == username);

            var youtubeId = model.YoutubeLink.Substring(model.YoutubeLink.IndexOf("=", StringComparison.Ordinal) + 1);

            var tube = new Tube
                           {
                               Author = model.Author,
                               Description = model.Description,
                               Title = model.Title,
                               YoutubeId = youtubeId,
                               UploaderId = uploader.Id,
                               Uploader = uploader
                           };

            this.context.Tubes.Add(tube);
            this.context.SaveChanges();
            this.context.SaveChanges();
        }
    }
}