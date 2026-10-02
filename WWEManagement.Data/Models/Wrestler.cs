namespace WWEManagement.Data.Models;

public class Wrestler
{
    public int WrestlerId { get; set; }

    public int EmployeeId { get; set; }

    public string RingName { get; set; } = string.Empty;

    public string? WeightClass { get; set; }

    public DateTime? DebutDate { get; set; }

    public bool IsActive { get; set; }

    public Employee? Employee { get; set; }
}