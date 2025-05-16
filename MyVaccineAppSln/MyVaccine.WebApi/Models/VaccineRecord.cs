namespace MyVaccine.WebApi.Models;

public class VaccineRecord : BaseTable
{
    public string VaccineRecordId { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public string DependentId { get; set; }
    public string VaccineId { get; set; }
    public Vaccine Vaccine { get; set; }
    public DateTime DateAdministered { get; set; }
    public string AdministeredLocation { get; set; }
    public string AdministeredBy { get; set; }
    public virtual Dependent Dependent { get; set; }

}
