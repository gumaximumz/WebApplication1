using NavTECH.Web.Models;
using Services.Models;
using Services.Repositories;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductTypeController : Controller
    {
        private static IUnitOfWork _unitOfWork;
        private ProductTypeService _productTypeService;

        public ProductTypeController()
        {
            _productTypeService = new ProductTypeService(_unitOfWork);
        }

        static ProductTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult Get(GridRequestModel gridRequestModel, ProductTypeModel searchModel, bool _search)
        {
            var data = _productTypeService.Get(gridRequestModel, _search ? searchModel : null);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: ProductType
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductTypeModel model)
        {
            if (this.ModelState.IsValid)
            {
                _productTypeService.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult AjaxCreate(ProductTypeModel model)
        {
            _productTypeService.Create(model);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxEdit(ProductTypeModel model)
        {

            _productTypeService.Update(model);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxDelete(ProductTypeModel model)
        {

            _productTypeService.Delete(model.Id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: ProductType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductType/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }        
    }
}
