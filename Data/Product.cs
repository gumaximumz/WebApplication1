using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Product : IDbModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProductType ProductType { get; set; }

        public Supplier Supplier { get; set; }
    }
}
