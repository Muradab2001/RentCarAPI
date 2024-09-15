using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class RefreshToken : BaseEntity
    {
        public string JwtId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool HasBeenUsed { get; set; }
        public bool IsRevoked { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}