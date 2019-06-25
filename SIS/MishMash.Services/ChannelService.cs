namespace MishMash.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using MishMash.Data;
    using MishMash.Models;
    using MishMash.Models.Enums;
    using MishMash.Services.Base;
    using MishMash.Services.Contracts;
    using MishMash.ViewModels.Channels;

    public class ChannelService : BaseService, IChannelService
    {
        public ChannelService(MishMashContext context)
            : base(context)
        {
        }

        public void CreateChannel(ChannelDetailsViewModel model)
        {
            var channel = new Channel
                              {
                                  Name = model.Name,
                                  Description = model.Description,
                                  Tags = model.Tags,
                                  Type = (ChannelType)Enum.Parse(typeof(ChannelType), model.Type, true)
                              };

            this.context.Channels.Add(channel);

            this.context.SaveChanges();
        }

        public void FollowChannel(int channelId, string username)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Username == username);
            var channel = this.context.Channels.FirstOrDefault(ch => ch.Id == channelId);

            var userChannel =
                new UserChannel { ChannelId = channelId, Channel = channel, UserId = user.Id, User = user };

            this.context.UsersChannels.Add(userChannel);
            user.FollowedChannels.Add(userChannel);
            channel.Followers.Add(userChannel);
            this.context.SaveChanges();
        }

        public ChannelDetailsViewModel GetChannelById(int modelChannelId)
        {
            var channel = this.context.Channels.FirstOrDefault(ch => ch.Id == modelChannelId);

            var model = new ChannelDetailsViewModel();
            model.Description = channel.Description;
            model.Name = channel.Name;
            model.Tags = channel.Tags;
            model.Type = channel.Type.ToString();
            model.Followers = this.context.UsersChannels.Count(uc => uc.ChannelId == modelChannelId);

            return model;
        }

        public ICollection<FollowedChannelsViewModel> GetFollowedChannels(string username)
        {
            var channelViewModels = new List<FollowedChannelsViewModel>();
            var userId = this.context.Users.FirstOrDefault(u => u.Username == username).Id;
            var channels = this.context.Channels.Where(ch => ch.Followers.Any(f => f.UserId == userId)).ToArray();

            for (var i = 0; i < channels.Length; i++)
            {
                var channelViewModel = new FollowedChannelsViewModel();
                channelViewModel.Followers = this.context.UsersChannels.Count(uch => uch.ChannelId == channels[i].Id);
                channelViewModel.Name = channels[i].Name;
                channelViewModel.Type = channels[i].Type.ToString();
                channelViewModel.Index = i + 1;
                channelViewModel.Id = channels[i].Id;

                channelViewModels.Add(channelViewModel);
            }

            return channelViewModels;
        }

        public ICollection<MyChannelViewModel> GetMyFollowedChannels(string username)
        {
            var channelViewModels = new List<MyChannelViewModel>();
            var userId = this.context.Users.FirstOrDefault(u => u.Username == username).Id;
            var channels = this.context.Channels.Where(ch => ch.Followers.Any(f => f.UserId == userId)).ToArray();

            foreach (var channel in channels)
            {
                var channelViewModel = new MyChannelViewModel();
                channelViewModel.Followers = this.context.UsersChannels.Count(uch => uch.ChannelId == channel.Id);
                channelViewModel.Name = channel.Name;
                channelViewModel.Type = channel.Type.ToString();

                channelViewModels.Add(channelViewModel);
            }

            return channelViewModels;
        }

        public ICollection<ChannelViewModel> GetOtherChannels(string username)
        {
            this.GetTags(username, out var channelViewModels, out var userTags, out var notFollowedChannels);

            foreach (var channel in notFollowedChannels)
            {
                var tags = channel.Tags.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var containsTag = false;

                for (var i = 0; i < tags.Length; i++)
                {
                    if (userTags.Contains(tags[i]))
                    {
                        containsTag = true;
                    }
                }

                if (!containsTag)
                {
                    var channelViewModel = new ChannelViewModel();

                    channelViewModel.Followers = this.context.UsersChannels.Count(uch => uch.ChannelId == channel.Id);
                    channelViewModel.Name = channel.Name;
                    channelViewModel.Type = channel.Type.ToString();
                    channelViewModel.Id = channel.Id;

                    channelViewModels.Add(channelViewModel);
                }
            }

            return channelViewModels;
        }

        public ICollection<ChannelViewModel> GetSuggestedChannels(string username)
        {
            this.GetTags(username, out var channelViewModels, out var userTags, out var notFollowedChannels);

            foreach (var channel in notFollowedChannels)
            {
                var tags = channel.Tags.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var containsTag = false;

                for (var i = 0; i < tags.Length; i++)
                {
                    if (userTags.Contains(tags[i]))
                    {
                        containsTag = true;
                    }
                }

                if (containsTag)
                {
                    var channelViewModel = new ChannelViewModel();

                    channelViewModel.Followers = this.context.UsersChannels.Count(uch => uch.ChannelId == channel.Id);
                    channelViewModel.Name = channel.Name;
                    channelViewModel.Type = channel.Type.ToString();
                    channelViewModel.Id = channel.Id;

                    channelViewModels.Add(channelViewModel);
                }
            }

            return channelViewModels;
        }

        public void UnfollowChannel(int channelId, string username)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Username == username);
            var channel = this.context.Channels.FirstOrDefault(ch => ch.Id == channelId);

            var userChannel =
                this.context.UsersChannels.FirstOrDefault(ch => ch.ChannelId == channel.Id && ch.UserId == user.Id);

            this.context.UsersChannels.Remove(userChannel);

            this.context.SaveChanges();
        }

        private void GetTags(
            string username,
            out List<ChannelViewModel> channelViewModels,
            out List<string> userTags,
            out IQueryable<Channel> notFollowedChannels)
        {
            channelViewModels = new List<ChannelViewModel>();
            var userId = this.context.Users.FirstOrDefault(u => u.Username == username).Id;

            userTags = new List<string>();
            var userFollowedChannels = this.context.Channels.Where(ch => ch.Followers.Any(f => f.UserId == userId));

            foreach (var userFollowedChannel in userFollowedChannels)
            {
                userTags.AddRange(
                    userFollowedChannel.Tags.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries));
            }

            notFollowedChannels = this.context.Channels.Where(ch => ch.Followers.All(f => f.UserId != userId))
                .Include(ch => ch.Followers);
        }
    }
}