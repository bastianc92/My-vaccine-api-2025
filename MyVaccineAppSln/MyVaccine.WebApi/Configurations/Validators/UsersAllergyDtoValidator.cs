using FluentValidation;
using MyVaccine.WebApi.Dtos.UsersAllergy;


namespace MyVaccine.WebApi.Configurations.Validators
{
    public class UsersAllergyDtoValidator : AbstractValidator<UsersAllergyRequestDto>
    {
        public UsersAllergyDtoValidator()
        {
          
            RuleFor(dto => dto.UserId).NotEmpty();
            RuleFor(dto => dto.AllergyId).NotEmpty();
       
        }
    }
}
