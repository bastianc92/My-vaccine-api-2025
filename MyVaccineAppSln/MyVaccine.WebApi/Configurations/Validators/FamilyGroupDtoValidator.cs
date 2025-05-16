using FluentValidation;
using MyVaccine.WebApi.Dtos.FamilyGroup;


namespace MyVaccine.WebApi.Configurations.Validators
{
    public class FamilyGroupDtoValidator : AbstractValidator<FamilyGroupRequestDto>
    {
        public FamilyGroupDtoValidator()
        {
            RuleFor(f => f.Name).NotEmpty().MaximumLength(50);

           
        }


    }
}
