namespace MyVaccine.WebApi.Models;

public class Dependent : BaseTable
{
    public string DependentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string UserId { get; set; }
    public string FamilyGroupId { get; set; } 

    public virtual FamilyGroup FamilyGroup { get; set; }
    public virtual ICollection<VaccineRecord> VaccineRecords { get; set; } = new List<VaccineRecord>();

}
