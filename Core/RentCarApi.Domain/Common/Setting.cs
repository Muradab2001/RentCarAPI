﻿using RentCarApi.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Domain.Common
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
