using System.ComponentModel.DataAnnotations;

namespace WWEManagement.Web.Dtos;

public class WrestlerRequestDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime HireDate { get; set; }

    [Required]
    [StringLength(100)]
    public string RingName { get; set; } = string.Empty;

    [StringLength(50)]
    public string? WeightClass { get; set; }

    public DateTime? DebutDate { get; set; }

    public bool IsActive { get; set; } = true;
}