using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Supplier : IDbModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ISet<Product> Product { get; set; }
    }
}
