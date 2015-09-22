using Data;
using Data.MockedModel;
using Moq;
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
    public class ProductTypeSpecs
    {
        public class the_producttype<T> : IContext<T>
        {
            public void Initialize(ISpecs<T> state)
            {
                var models = MockedProductType.Object;

                state.GetMockFor<IRepository<ProductType>>()
                    .Setup(x => x.Queryable)
                    .Returns(models.AsQueryable());
                foreach(var producttype in models)
                {
                    state.GetMockFor<IRepository<ProductType>>()
                        .Setup(x => x.Get(producttype.Id))
                        .Returns(producttype);
                }
                state.GetMockFor<IUnitOfWork>()
                    .SetupGet(x => x.ProductTypeRepository)
                    .Returns(state.GetMockFor<IRepository<ProductType>>().Object);
            }
        }

        public class when_getting_producttype : SpecsFor<ProductTypeService>
        {
            protected override void Given()
            {
                Given<the_producttype<ProductTypeService>>();
            }

            GridResponseModel<ProductTypeModel> results;
            GridResponseModel<ProductTypeModel> searchResults;
            GridResponseModel<ProductTypeModel> sortResults;

            protected override void When()
            {
                results = SUT.Get(new GridRequestModel(), null);
                searchResults = SUT.Get(new GridRequestModel(), new ProductTypeModel() { Name = "Type1" });
                sortResults = SUT.Get(new GridRequestModel() { Sidx = "Name", Sord = "desc" }, null);
            }

            [Test]
            public void the_can_search()
            {
                searchResults.records.ShouldBeGreaterThan(0);

                searchResults.rows.All(r => r.Name.Contains("Type1")).ShouldBeTrue();
            }
        }

        //public class when_getting_single_producttype : SpecsFor<ProductTypeService>
        //{
        //    protected override void Given()
        //    {
        //        GetMockFor<the_producttype<ProductTypeService>>();
        //    }
        //    ProductTypeModel result = null;
        //    protected override void When()
        //    {
                
        //    }
        //    [Test]
        //    public void then_call_get_in_repo()
        //    {

        //    }
        //}

        public class when_creating_producttype : SpecsFor<ProductTypeService>
        {
            protected override void Given()
            {
                Given<the_producttype<ProductTypeService>>();
            }

            protected override void When()
            {
                var mockProductTypeModel = new ProductTypeModel()
                {
                    Name = "Type1"
                };
                SUT.Create(mockProductTypeModel);
            }
            [Test]
            public void then_call_create_in_repo()
            {
                GetMockFor<IRepository<ProductType>>()
                    .Verify(x => x.Create(It.IsAny<ProductType>()));
            }
        }

        public class when_edit_producttype : SpecsFor<ProductTypeService>
        {
            protected override void Given()
            {
                Given<the_producttype<ProductTypeService>>();
                Given<ProductSpecs.the_product<ProductTypeService>>();
                Given<SupplierSpecs.the_supplier<ProductTypeService>>();
            }
            ProductTypeModel _mockProductTypeModel = null;
            protected override void When()
            {
                _mockProductTypeModel = new ProductTypeModel()
                {
                    Id = 1,
                    Name = "Type1"
                };
                SUT.Update(_mockProductTypeModel);
            }
            [Test]
            public void then_call_edit_in_repo()
            {
                GetMockFor<IRepository<ProductType>>()
                    .Verify(x => x.Edit(It.Is<ProductType>(p => p.Id == _mockProductTypeModel.Id
                    && p.Name == _mockProductTypeModel.Name)));
            }
        }

        public class when_delete_producttype : SpecsFor<ProductTypeService>
        {
            protected override void Given()
            {
                Given<the_producttype<ProductTypeService>>();
            }
            int productTypeId = 1;
            protected override void When()
            {
                SUT.Delete(productTypeId);
            }
            [Test]
            public void then_call_delete_in_repo()
            {
                GetMockFor<IRepository<ProductType>>()
                    .Verify(x => x.Delete(It.Is<ProductType>(p => p.Id == productTypeId)));
            }
        }
    }
}
