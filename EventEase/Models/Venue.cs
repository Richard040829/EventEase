using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    /// <summary>
    /// Represents a physical location that can host events (e.g. a hall, garden or conference room).
    /// </summary>
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required(ErrorMessage = "Please enter the venue name.")]
        [StringLength(150)]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the venue location.")]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Range(1, 100000, ErrorMessage = "Capacity must be a positive number.")]
        public int Capacity { get; set; }

        // Phase 1: a simple placeholder URL is stored here.
        // Phase 2 will replace this with an uploaded file stored in Azure Blob Storage.
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Event>? Events { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
