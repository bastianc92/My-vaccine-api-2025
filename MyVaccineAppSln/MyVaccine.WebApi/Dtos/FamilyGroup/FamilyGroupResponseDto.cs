using MyVaccine.WebApi.Dtos.Dependent;

namespace MyVaccine.WebApi.Dtos.FamilyGroup
{
    public class FamilyGroupResponseDto: FamilyGroupRequestDto
    {
        public string FamilyGroupId { get; set; }
        public List<DependentResponseDto> Dependents { get; set; }

    }
}
