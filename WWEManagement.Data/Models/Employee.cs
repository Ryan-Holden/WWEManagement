using System.ComponentModel.DataAnnotations;

namespace WWEManagement.Data.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Display(Name = "Job Title")]
    public string JobTitle { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Hire Date")]
    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; }

    public Wrestler? Wrestler { get; set; }
}