﻿using Newtonsoft.Json;
using System;

namespace refactor_me.Core.Domain.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

    }
}