﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace refactor_me.Core.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        [JsonIgnore]
        public ICollection<ProductOption> ProductOptions { get; set; }
        
    }

    
}