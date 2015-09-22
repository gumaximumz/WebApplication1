using AutoMapper;
using Data;
using NavTECH.Web;
using NavTECH.Web.Models;
using Services.Models;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SupplierService
    {
        private IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        static SupplierService()
        {
            Mapper.CreateMap<Supplier, SupplierModel>();
            Mapper.CreateMap<SupplierModel, Supplier>();
        }

        public GridResponseModel<SupplierModel> Get(GridRequestModel gridRequestModel, SupplierModel searchModel)
        {
            var query = _unitOfWork.SupplierRepository.Queryable;
            var dbModel = query.ToPagingResult(gridRequestModel);
            var model = dbModel.Rows.Select(m => Mapper.Map<SupplierModel>(m)).ToArray();
            var gridData = new GridResponseModel<SupplierModel>(gridRequestModel, dbModel.RowCount, model);
            return gridData;
        }

        public SupplierModel[] Get()
        {
            var query = _unitOfWork.SupplierRepository.Queryable.ToArray();
            return query.Select(m => Mapper.Map<SupplierModel>(m)).ToArray();
        }

        public void Create(SupplierModel model)
        {
            var dbModel = Mapper.Map<Supplier>(model);
            _unitOfWork.SupplierRepository.Create(dbModel);
        }

        public void Update(SupplierModel model)
        {
            var dbModel = _unitOfWork.SupplierRepository.Get(model.Id);
            dbModel = Mapper.Map(model, dbModel);
            _unitOfWork.SupplierRepository.Edit(dbModel);
        }

        public void Delete(int Id)
        {
            var dbModel = _unitOfWork.SupplierRepository.Get(Id);

            _unitOfWork.SupplierRepository.Delete(dbModel);
        }
    }
}
