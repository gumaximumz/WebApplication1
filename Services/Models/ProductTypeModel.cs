using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.Models
{
    public class ProductTypeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ISet<ProductModel> Products { get; set; }

        public int Count { get; set; }
    }
}