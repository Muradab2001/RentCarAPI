using RentCarApi.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Domain.Models
{
    public class Color :BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
    }
}
