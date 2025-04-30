namespace Domain.Entities;

using Domain.Entities.Common;
using Domain.Enums;

public class Mission : BaseEntity
{
    public string? ClientName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? ConsultantId { get; set; }
    public Consultant? Consultant { get; set; }
    public MissionStatus Status { get; set; }
}
