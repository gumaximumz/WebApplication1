using Data;
using Data.MockedModel;
using NavTECH.Web;
using NavTECH.Web.Models;
using NUnit.Framework;
using Services.Models;
using Services.Repositories;
using Services.Services;
using Should;
using SpecsFor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Tests.Services
{
    public class SupplierSpecs
    {
        public class the_supplier<T> : IContext<T>
        {
            public void Initialize(ISpecs<T> state)
            {
                var models = MockedSupplier.Object;
                state.GetMockFor<IRepository<Supplier>>()
                    .Setup(x => x.Queryable)
                    .Returns(models.AsQueryable());
                foreach (var supplier in models)
                {
                    state.GetMockFor<IRepository<Supplier>>()
                        .Setup(x => x.Get(supplier.Id))
                        .Returns(supplier);
                }
                state.GetMockFor<IUnitOfWork>()
                    .SetupGet(x => x.SupplierRepository)
                    .Returns(state.GetMockFor<IRepository<Supplier>>().Object);
            }
        }
        public class when_getting_supplier : SpecsFor<SupplierService>
        {
            protected override void Given()
            {
                Given<the_supplier<SupplierService>>();
            }

            GridResponseModel<SupplierModel> results;
            GridResponseModel<SupplierModel> searchResults;
            GridResponseModel<SupplierModel> sortResults;

            protected override void When()
            {
                results = SUT.Get(new GridRequestModel(), null);
                searchResults = SUT.Get(new GridRequestModel(), new SupplierModel() { Name = "Supplier1" });
                sortResults = SUT.Get(new GridRequestModel() { Sidx = "Name", Sord = "desc" }, null);
            }
            [Test]
            public void the_can_search()
            {
                searchResults.records.ShouldBeGreaterThan(0);
                searchResults.rows.All(r => r.Name.Contains("Supplier1")).ShouldBeTrue();
            }
        }
        public class when_create_supplier : SpecsFor<SupplierService>
        {
            protected override void Given()
            {
                base.Given();
            }
            protected override void When()
            {
                base.When();
            }
            //[Test]
            //public void then_call_create_in_repo()
        }
    }
}
