namespace MyVaccine.WebApi.Models
{
    public class UsersAllergy
    {
        public string UserAllergyId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public string AllergyId { get; set; }
        public Allergy Allergy { get; set; }

    }
}
