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
    public class SupplierController : Controller
    {
        private static IUnitOfWork _unitOfWork;

        private SupplierService _supplierService;

        public SupplierController()
        {
            _supplierService = new SupplierService(_unitOfWork);
        }

        static SupplierController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult Get(GridRequestModel gridRequestModel, SupplierModel searchModel, bool _search)
        {
            var data = _supplierService.Get(gridRequestModel, _search ? searchModel : null);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AjaxCreate(SupplierModel model)
        {
            _supplierService.Create(model);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Supplier/Edit/5
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

        [HttpPost]
        public ActionResult AjaxEdit(SupplierModel model)
        {
            _supplierService.Update(model);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Supplier/Delete/5
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

        public ActionResult AjaxDelete(SupplierModel model)
        {
            _supplierService.Delete(model.Id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
