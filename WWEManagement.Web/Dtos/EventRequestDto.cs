using System.ComponentModel.DataAnnotations;

namespace WWEManagement.Web.Dtos;

public class EventRequestDto
{
    [Required]
    [StringLength(100)]
    public string EventName { get; set; } = string.Empty;

    [Required]
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