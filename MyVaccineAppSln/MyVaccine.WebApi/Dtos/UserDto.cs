using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Dtos.UsersAllergy;
using MyVaccine.WebApi.Dtos.VaccineRecord;

namespace MyVaccine.WebApi.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Photo { get; set; }
        public List<DependentResponseDto> Dependents { get; set; }
        public List<FamilyGroupResponseDto> FamilyGroups { get; set; }
        public List<VaccineRecordResponseDto> VaccineRecords { get; set; }
        public List<UsersAllergyResponseDto> UsersAllergies { get; set; }
    }
}
