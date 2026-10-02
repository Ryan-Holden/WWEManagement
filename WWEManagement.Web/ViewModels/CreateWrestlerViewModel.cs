using System.ComponentModel.DataAnnotations;

namespace WWEManagement.Web.ViewModels;

public class CreateWrestlerViewModel
{
    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Hire Date")]
    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Ring Name")]
    public string RingName { get; set; } = string.Empty;

    [StringLength(50)]
    [Display(Name = "Weight Class")]
    public string? WeightClass { get; set; }

    [Display(Name = "Debut Date")]
    [DataType(DataType.Date)]
    public DateTime? DebutDate { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}