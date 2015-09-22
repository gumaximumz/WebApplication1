using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class SupplierModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ISet<ProductModel> Product { get; set; }

        public ISet<ProductTypeModel> ProductType { get; set; }
    }
}
