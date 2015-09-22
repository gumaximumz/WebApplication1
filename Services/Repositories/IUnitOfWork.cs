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
    
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }

        IRepository<ProductType> ProductTypeRepository { get; }

        IRepository<Supplier> SupplierRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IRepositoryFactory _factory;

        public UnitOfWork()
        {
            _factory = new FakeRepositoryFactory();
        }

        IRepository<Product> _productRepository;
        public IRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = _factory.CreateInstance<IRepository<Product>>();
                }

                return _productRepository;
            }
        }

        IRepository<ProductType> _productTypeRepository;
        public IRepository<ProductType> ProductTypeRepository
        {
            get 
            {
                if (_productTypeRepository == null)
                {
                    _productTypeRepository = _factory.CreateInstance<IRepository<ProductType>>();
                }

                return _productTypeRepository;
            }
        }

        IRepository<Supplier> _supplierRepository;
        public IRepository<Supplier> SupplierRepository
        {
            get
            {
                if(_supplierRepository == null)
                {
                    _supplierRepository = _factory.CreateInstance<IRepository<Supplier>>();
                }

                return _supplierRepository;
            }
        }
    }
}
