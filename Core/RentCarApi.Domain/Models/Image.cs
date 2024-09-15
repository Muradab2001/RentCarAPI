using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models
{
    public class Image<T> : BaseEntity where T : BaseEntity
    {
        public int ItemId { get; set; }
        public T Item { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return ImageUrl;
        }
    }
}