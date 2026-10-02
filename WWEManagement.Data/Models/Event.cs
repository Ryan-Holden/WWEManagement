using System.ComponentModel.DataAnnotations;

namespace WWEManagement.Data.Models;

public class Event
{
    public int EventId { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Event Name")]
    public string EventName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Event Date")]
    public DateTime EventDate { get; set; }

    [Required]
    [StringLength(150)]
    public string Venue { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    [StringLength(50)]
    public string? State { get; set; }

    [Required]
    [RegularExpression(
        "^(Scheduled|Completed|Cancelled)$",
        ErrorMessage =
            "Status must be Scheduled, Completed, or Cancelled.")]
    public string Status { get; set; } = "Scheduled";
}