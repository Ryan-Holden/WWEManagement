namespace WWEManagement.Data.Models;

public class WrestlerDetails
{
    public int WrestlerId { get; set; }

    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string JobTitle { get; set; } = string.Empty;

    public DateTime HireDate { get; set; }

    public string RingName { get; set; } = string.Empty;

    public string? WeightClass { get; set; }

    public DateTime? DebutDate { get; set; }

    public bool IsActive { get; set; }
}