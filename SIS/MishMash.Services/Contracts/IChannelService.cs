namespace MishMash.Services.Contracts
{
    using System.Collections.Generic;

    using MishMash.ViewModels.Channels;

    public interface IChannelService
    {
        void CreateChannel(ChannelDetailsViewModel model);

        void FollowChannel(int channelId, string username);

        ChannelDetailsViewModel GetChannelById(int modelChannelId);

        ICollection<FollowedChannelsViewModel> GetFollowedChannels(string username);

        ICollection<MyChannelViewModel> GetMyFollowedChannels(string username);

        ICollection<ChannelViewModel> GetOtherChannels(string username);

        ICollection<ChannelViewModel> GetSuggestedChannels(string username);

        void UnfollowChannel(int channelId, string username);
    }
}