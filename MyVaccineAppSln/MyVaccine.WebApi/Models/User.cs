using Microsoft.AspNetCore.Identity;

namespace MyVaccine.WebApi.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? Birthdate { get; set; }
    public string Photo { get; set; }
    public List<Dependent> Dependents { get; set; }
    public virtual ICollection<FamilyGroup> FamilyGroups { get; set; } = new List<FamilyGroup>();
    public List<VaccineRecord> VaccineRecords { get; set; }
    public List<UsersAllergy> UsersAllergies { get; set; }

}
