using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Dtos.VaccineRecord
{
    public class VaccineRecordRequestDto
    {
        public string UserId { get; set; }
   
        public string DependentId { get; set; }
      
        public string VaccineId { get; set; }
        public DateTime DateAdministered { get; set; }
        public string AdministeredLocation { get; set; } 
        public string AdministeredBy { get; set; }
    }
}
