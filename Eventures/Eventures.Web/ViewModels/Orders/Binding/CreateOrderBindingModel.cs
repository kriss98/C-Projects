namespace Eventures.Web.ViewModels.Orders.Binding
{
    using System.ComponentModel.DataAnnotations;

    public class CreateOrderBindingModel
    {
        public string EventId { get; set; }

        public string CustomerName { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Tickets")]
        public int Tickets { get; set; }
    }
}