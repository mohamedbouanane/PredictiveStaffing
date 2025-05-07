namespace Application.Dtos;

public class ConsultantDto
{
    public Guid? Id { get; set; }
    public string? FullName { get; set; }
    public string? Skills { get; set; }
    public string? AvailabilityStatus { get; set; }
    public List<MissionDto>? Missions { get; set; }
}
