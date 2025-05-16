using MyVaccine.WebApi.Dtos.Dependent;

namespace MyVaccine.WebApi.Dtos.FamilyGroup
{
    public class FamilyGroupRequestDto
    {
        public string Name { get; set; }
        public List<DependentRequestDto>? Dependents { get; set; }
    }
}
