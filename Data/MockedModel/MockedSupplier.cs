using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MockedModel
{
    public class MockedSupplier
    {

        public static Supplier[] Object
        {
            get
            {
                Supplier[] model = new Supplier[]
                {
                    new Supplier() { Id = 1, Name = "Supplier 1" },
                    new Supplier() { Id = 2, Name = "Supplier 2" }
                };
                return model;
            }
        }

    }
}
