namespace Domain.Entities;

using Domain.Entities.Common;

public class Consultant : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Skills { get; set; }
    public DateTime? AvailabilityDate { get; set; }
    public List<Mission> Missions { get; set; } = [];
}
