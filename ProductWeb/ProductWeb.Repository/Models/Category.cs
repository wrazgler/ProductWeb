﻿using System.Collections.Generic;

namespace ProductWeb.Repository.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}