namespace MyVaccine.WebApi.Models;

public class Allergy : BaseTable
{
    public string AllergyId { get; set; }
    public string Name { get; set; }

    public List<UsersAllergy> UsersAllergies { get; set; }
}
