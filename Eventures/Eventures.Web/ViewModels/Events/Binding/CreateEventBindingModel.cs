namespace Eventures.Web.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateEventBindingModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Event Name must be atleast 10 characters long!")]
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"\S+", ErrorMessage = "Event place should not be empty!")]
        [Display(Name = "Event Place")]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Event Start should be a valid date!")]
        [Display(Name = "Start Date and Time")]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Event End must be a valid date!")]
        [Display(Name = "End Date and Time")]
        public DateTime End { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total tickest must be a non-zero positive number!")]
        [Display(Name = "Total Tickets")]
        public int TotalTickets { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price per Ticket")]
        public decimal PricePerTicket { get; set; }
    }
}