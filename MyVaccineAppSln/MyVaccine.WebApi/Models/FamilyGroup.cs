namespace MyVaccine.WebApi.Models;

public class FamilyGroup : BaseTable
{
    public string FamilyGroupId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

}
