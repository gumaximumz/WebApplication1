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
using AutoMapper;
using Data;

namespace Services.Services
{
    public class ProductTypeService
    {
        private IUnitOfWork _unitOfWork;

        public ProductTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        static ProductTypeService()
        {
            Mapper.CreateMap<ProductType, ProductTypeModel>();
            Mapper.CreateMap<ProductTypeModel, ProductType>();

        }

        public GridResponseModel<ProductTypeModel> Get(GridRequestModel gridRequestModel, ProductTypeModel searchModel)
        {
            var query = _unitOfWork.ProductTypeRepository.Queryable;

            var dbModel = query.ToPagingResult(gridRequestModel);

            var model = dbModel.Rows.Select(m => Mapper.Map<ProductTypeModel>(m)).ToArray();

            var gridData = new GridResponseModel<ProductTypeModel>(gridRequestModel, dbModel.RowCount, model);

            return gridData;
        }

        public ProductTypeModel[] Get()
        {
            var query = _unitOfWork.ProductTypeRepository.Queryable.ToArray();

            return query.Select(m => Mapper.Map<ProductTypeModel>(m)).ToArray();

        }

        public void Create(ProductTypeModel model)
        {
            var dbModel = Mapper.Map<ProductType>(model);

            _unitOfWork.ProductTypeRepository.Create(dbModel);

        }

        public void Update(ProductTypeModel model)
        {
            var dbModel = _unitOfWork.ProductTypeRepository.Get(model.Id);
            //MapFromModel(dbModel, model);
            dbModel = Mapper.Map(model, dbModel);


            _unitOfWork.ProductTypeRepository.Edit(dbModel);
        }

        public void Delete(int Id)
        {
            var dbModel = _unitOfWork.ProductTypeRepository.Get(Id);

            _unitOfWork.ProductTypeRepository.Delete(Id);
        }
    }
}
