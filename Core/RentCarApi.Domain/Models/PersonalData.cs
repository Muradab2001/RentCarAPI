using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class PersonalData : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string IdCardImage { get; set; }
        public string DrivingLicenseImage { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}