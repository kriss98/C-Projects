namespace Eventures.Web.MappingConfig
{
    using AutoMapper;

    using Eventures.Data;
    using Eventures.Models;
    using Eventures.Web.ViewModels.Account;
    using Eventures.Web.ViewModels.Events;
    using Eventures.Web.ViewModels.Orders;
    using Eventures.Web.ViewModels.Orders.Binding;

    public class MappingConfiguration : Profile
    { 
        public MappingConfiguration()
        {
            this.CreateMap<ExternalLoginConfirmationViewModel, EventuresUser>().ForMember(
                m => m.UserName,
                opt => opt.MapFrom(src => src.Email));

            this.CreateMap<RegisterBindingModel, EventuresUser>().ForMember(m => m.UCN, opt => opt.MapFrom(src => src.UniversalCitizenNumber));

            this.CreateMap<EventuresEvent, EventViewModel>();

            this.CreateMap<CreateEventBindingModel, EventuresEvent>();

            this.CreateMap<EventuresOrder, MyEventsViewModel>().ForMember(
                m => m.Name,
                opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(
                    m => m.Tickets,
                    opt => opt.MapFrom(src => src.TicketsCount))
                .ForMember(
                    m => m.Start,
                    opt => opt.MapFrom(src => src.Event.Start.ToString("dd-MMM-yyyy hh:mm")))
                .ForMember(
                    m => m.End,
                    opt => opt.MapFrom(src => src.Event.End.ToString("dd-MMM-yyyy hh:mm")))
                .ForMember(
                    m => m.Id,
                    opt => opt.MapFrom(src => src.EventId));

            this.CreateMap<EventuresOrder, OrderViewModel>()
                .ForMember(m => m.Event, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(m => m.Customer, opt => opt.MapFrom(src => src.Customer.UserName)).ForMember(
                    m => m.OrderedOn,
                    opt => opt.MapFrom(src => src.OrderedOn.ToString("dd-MMM-yyyy hh:mm")));

            this.CreateMap<CreateOrderBindingModel, EventuresOrder>().ForMember(
                m => m.TicketsCount,
                opt => opt.MapFrom(src => src.Tickets));

            this.CreateMap<EventuresUser, UserViewModel>();
        }
    }
}