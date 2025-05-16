using FluentValidation;
using MyVaccine.WebApi.Dtos.VaccineRecord;


namespace MyVaccine.WebApi.Configurations.Validators
{
    public class VaccineRecordDtoValidator : AbstractValidator<VaccineRecordRequestDto>
    {
        public VaccineRecordDtoValidator()
        {
            RuleFor(dto => dto.AdministeredLocation).NotEmpty().MaximumLength(255);
            RuleFor(dto => dto.AdministeredBy).NotEmpty().MaximumLength(255);
            RuleFor(dto => dto.DateAdministered).NotEmpty();
            RuleFor(dto => dto.UserId).NotEmpty();
            RuleFor(dto => dto.DependentId).NotEmpty();
            RuleFor(dto => dto.VaccineId).NotEmpty();
        }

    }
}
