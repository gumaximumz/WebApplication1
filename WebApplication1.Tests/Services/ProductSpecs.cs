using Data;
using NavTECH.Web;
using Services.Models;
using Services.Services;
using SpecsFor;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Services.Repositories;
using Moq;
using Data.MockedModel;
using NavTECH.Web.Models;

namespace WebApplication1.Tests.Services
{
    public class ProductSpecs
    {
        public class the_product<T> : IContext<T>
        {
            public void Initialize(ISpecs<T> state)
            {
                var models = MockedProduct.Objects;

                state.GetMockFor<IRepository<Product>>()
                    .Setup(x => x.Queryable)
                    .Returns(models.AsQueryable());

                foreach(var product in models)
                {
                    state.GetMockFor<IRepository<Product>>()
                        .Setup(x => x.Get(product.Id))
                        .Returns(product);
                }

                state.GetMockFor<IUnitOfWork>()
                    .SetupGet(x => x.ProductRepository)
                    .Returns(state.GetMockFor<IRepository<Product>>().Object);
            }
        }
      
        public class when_getting_product : SpecsFor<ProductService>
        {
            protected override void Given()
            {
                Given<the_product<ProductService>>();
            }

            GridResponseModel<ProductModel> results;
            GridResponseModel<ProductModel> searchResults;
            GridResponseModel<ProductModel> sortResults;
            protected override void When()
            {
                results = SUT.Get(new GridRequestModel(), null);
                searchResults = SUT.Get(new GridRequestModel(), new ProductModel() { Name = "AB" });
                sortResults = SUT.Get(new GridRequestModel() { Sidx = "Name", Sord= "desc" }, null);
            }

            [Test]
            public void then_product_count()
            {
                results.rows.Count().ShouldBeGreaterThan(0);
            }

            [Test]
            public void then_set_property_correctly()
            {
                results.rows.First().Id.ShouldBeGreaterThan(0);
                results.rows.First().Name.ShouldNotBeEmpty();
                results.rows.First().ProductTypeName.ShouldNotBeNull();
            }

            [Test]
            public void then_can_search()
            {
                searchResults.records.ShouldBeGreaterThan(0);

                searchResults.rows.All(r => r.Name.Contains("AB")).ShouldBeTrue();
            }

            [Test]
            public void then_can_sort()
            {
                sortResults.rows.First().Name.ShouldEqual("SDE");
            }
        }

        public class when_getting_single_product : SpecsFor<ProductService>
        {
            protected override void Given()
            {
                Given<the_product<ProductService>>();
            }

            ProductModel result;
            protected override void When()
            {
                result = SUT.Get(1);
            }

            [Test]
            public void then_set_property_correctly()
            {
                result.Id.ShouldBeGreaterThan(0);
                result.Name.ShouldEqual("AB");
            }
        }

        public class when_creating_product : SpecsFor<ProductService>
        {
            protected override void Given()
            {
                Given<the_product<ProductService>>();
                Given<ProductTypeSpecs.the_producttype<ProductService>>();
                Given<SupplierSpecs.the_supplier<ProductService>>();
            }

            protected override void When()
            {
                var mockProductModel = new ProductModel()
                {
                    Name = "Product1"
                };

                SUT.Create(mockProductModel);
            }

            [Test]
            public void then_call_create_in_repo()
            {
                GetMockFor<IRepository<Product>>()
                   .Verify(x => x.Create(It.IsAny<Product>()));

            }
        }

        public class when_edit_product : SpecsFor<ProductService>
        {
            protected override void Given()
            {
                Given<the_product<ProductService>>();
                Given<ProductTypeSpecs.the_producttype<ProductService>>();
                Given<SupplierSpecs.the_supplier<ProductService>>();
            }

            ProductModel _mockProductModel = null;
            protected override void When()
            {
                _mockProductModel = new ProductModel()
                {
                    Id = 1,
                    Name = "Product1"
                };
                SUT.Update(_mockProductModel);
            }

            [Test]
            public void then_call_edit_in_repo()
            {
                GetMockFor<IRepository<Product>>()
                .Verify(x => x.Edit(It.Is<Product>(p => p.Id == _mockProductModel.Id
                    && p.Name == _mockProductModel.Name)));
            }
        }

        public class when_delete_product : SpecsFor<ProductService>
        {
            protected override void Given()
            {
                Given<the_product<ProductService>>();
            }

            int productId = 1;
            protected override void When()
            {
                SUT.Delete(productId);
            }

            [Test]
            public void then_call_delete_in_repo()
            {
                GetMockFor<IRepository<Product>>()
                    .Verify(x => x.Delete(It.Is<Product>(p => p.Id == productId)));
            }
        }
    }
}
