namespace Api.Dtos;

public class ConsultantDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Skills { get; set; }
    public string AvailabilityStatus { get; set; }
    public List<MissionDto> Missions { get; set; }
}
