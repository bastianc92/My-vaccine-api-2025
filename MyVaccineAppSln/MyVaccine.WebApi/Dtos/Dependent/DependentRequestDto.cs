namespace MyVaccine.WebApi.Dtos.Dependent;

public class DependentRequestDto
{
    public string? DependentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string FamilyGroupId { get; set; }
}
