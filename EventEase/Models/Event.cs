using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    /// <summary>
    /// Represents an event that a customer has requested. An event can be captured
    /// before a venue has been confirmed for it (VenueId is nullable) - once a venue
    /// is available, a Booking record is created to link the two together.
    /// </summary>
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Please enter the event name.")]
        [StringLength(150)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        // Nullable: an event may be captured before it has been assigned a venue.
        [Display(Name = "Venue")]
        public int? VenueId { get; set; }

        [ForeignKey(nameof(VenueId))]
        public Venue? Venue { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
