using AutoMapper;
using Data;
using NavTECH.Web.Models;
using Services.Models;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavTECH.Web;
using NavTECH.Web.Extensions;

namespace Services.Services
{
    public class ProductService
    {
        private IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        static ProductService()
        {
            Mapper.CreateMap<Product, ProductModel>();
            Mapper.CreateMap<ProductModel, Product>();
            Mapper.CreateMap<ProductType, ProductTypeModel>();
            Mapper.CreateMap<Supplier, SupplierService>();
        }

        public GridResponseModel<ProductModel> Get(GridRequestModel gridRequestModel, ProductModel searchModel)
        {
            var query = _unitOfWork.ProductRepository.Queryable;
            if (searchModel != null)
            {
                if (searchModel.ProductTypeId > 0)
                    query = query.Where(p => p.ProductType.Id == searchModel.ProductTypeId);
                if (searchModel.SupplierId > 0)
                    query = query.Where(p => p.Supplier != null && p.Supplier.Id == searchModel.SupplierId);
                if (!string.IsNullOrWhiteSpace(searchModel.Name))
                    query = query.Where(p => p.Name.Contains(searchModel.Name));
            }

            switch (gridRequestModel.Sidx)
            {
                default:
                case "Name":
                    query = query.OrderBy(p => p.Name, gridRequestModel.IsSortAsending);
                    break;
            }
            

            var dbModel = query.ToPagingResult(gridRequestModel);

            var model = dbModel.Rows.Select(m => Mapper.Map<ProductModel>(m)).ToArray();

            var gridData = new GridResponseModel<ProductModel>(gridRequestModel, dbModel.RowCount, model);

            return gridData;
        }

        public ProductModel Get(int id)
        {
            var dbModel = _unitOfWork.ProductRepository.Get(id);

            return Mapper.Map<ProductModel>(dbModel);
        }

        public void Create(ProductModel model)
        {
            var dbModel = Mapper.Map<Product>(model);
            dbModel.ProductType = _unitOfWork.ProductTypeRepository.Get(model.ProductTypeId);
            dbModel.Supplier = _unitOfWork.SupplierRepository.Get(model.SupplierId);

            _unitOfWork.ProductRepository.Create(dbModel);
        }

        public void Update(ProductModel model)
        {

            var dbModel = _unitOfWork.ProductRepository.Get(model.Id);
            dbModel = Mapper.Map(model, dbModel);
            dbModel.ProductType = _unitOfWork.ProductTypeRepository.Get(model.ProductTypeId);
            dbModel.Supplier = _unitOfWork.SupplierRepository.Get(model.SupplierId);

            _unitOfWork.ProductRepository.Edit(dbModel);

        }      

        public void Delete(int id)
        {
            var dbModel = _unitOfWork.ProductRepository.Get(id);
            
            _unitOfWork.ProductRepository.Delete(dbModel);
        }
    }
}
