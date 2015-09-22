using Data;
using Data.MockedModel;
using NavTECH.Web;
using NavTECH.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    class FakeRepositoryFactory : FakeRepositoryFactoryBase
    {
        static FakeRepositoryFactory()
        {
            AddFactory<IRepository<Product>>(new FakeRepository<Product>(MockedProduct.Objects));
            AddFactory<IRepository<ProductType>>(new FakeRepository<ProductType>(MockedProductType.Object));
            AddFactory<IRepository<Supplier>>(new FakeRepository<Supplier>(MockedSupplier.Object));
        }
    }
}
