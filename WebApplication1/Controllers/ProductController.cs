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
    public class ProductController : Controller
    {
        private ProductService _productService;
        private ProductTypeService _productTypeService;
        private SupplierService _supplierService;
        private static IUnitOfWork _unitOfWork; 

        static ProductController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ProductController()
        {
            _productService = new ProductService(_unitOfWork);
            _productTypeService = new ProductTypeService(_unitOfWork);
            _supplierService = new SupplierService(_unitOfWork);
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            GetProductTypeDropDown();
            GetProductType();
            GetSupplierDropDown();
            GetSupplier();

            return View();
        }

        public ActionResult Get(GridRequestModel gridRequestModel, ProductModel searchModel, bool _search)
        {
            if (searchModel.ProductTypeId > 0 || searchModel.SupplierId > 0)  _search = true;
            var data = _productService.Get(gridRequestModel, _search ? searchModel : null);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ProductTypeModel[] GetyProductType()
        {
            var model = new ProductTypeModel[]
            {
                new ProductTypeModel()
                {
                    Id = 1,
                    Name = "Type1",
                    Products = new HashSet<ProductModel>()
                    {
                        new ProductModel()
                        {
                            Id = 1,
                            Name = "AB"
                        },
                        new ProductModel()
                        {
                            Id = 3,
                            Name = "CD"
                        }
                    }
                },
                new ProductTypeModel()
                {
                    Id = 2,
                    Name = "Type2",
                    Products = new HashSet<ProductModel>()
                    {
                        new ProductModel()
                        {
                            Id = 2,
                            Name = "BC"
                        }
                    }
                }
            };

            return model;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {
            if (this.ModelState.IsValid)
            {
                _productService.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AjaxCreate(ProductModel model)
        {
            _productService.Create(model);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }       

        [HttpPost]
        public ActionResult AjaxEdit(ProductModel model)
        {
            
            _productService.Update(model);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxDelete(ProductModel model) 
        {

            _productService.Delete(model.Id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Edit(int id)
        {
            var model = _productService.Get(id);

            return View(model);
        }      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel model)
        {
            if (this.ModelState.IsValid)
            {
                _productService.Update(model);

                return RedirectToAction("Index");
            }
            

            return View(model);
        } 

        public ActionResult Delete(int id)
        {
            var model = _productService.Get(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductModel model)
        {

            _productService.Delete(model.Id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _productService.Get(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ProductModel model)
        {
                        
            return View(model);
        }

        public void GetProductTypeDropDown()
        {
            var list = new List<SelectListItem>();
            var model = _productTypeService.Get();

            foreach (var pt in model)
            {
                var selectList = new SelectListItem();
                selectList.Value = pt.Id.ToString();
                selectList.Text = pt.Name;

                list.Add(selectList);

            }

            list.Insert(0, new SelectListItem() { Value = "0", Text = "ทั้งหมด" });

            ViewBag.ProductTypeDropDown = list;
            
        }

        public void GetProductType()
        {
            var list = _productTypeService.Get();
            var slectList = new SelectList(list, "Id", "Name");

            ViewBag.ProductTypeSelectList = slectList;
        }

        public void GetSupplierDropDown()
        {
            var list = new List<SelectListItem>();
            var model = _supplierService.Get();

            foreach (var pt in model)
            {
                var selectList = new SelectListItem();
                selectList.Value = pt.Id.ToString();
                selectList.Text = pt.Name;

                list.Add(selectList);

            }

            list.Insert(0, new SelectListItem() { Value = "0", Text = "ทั้งหมด" });

            ViewBag.SupplierDropDown = list;

        }

        public void GetSupplier()
        {
            var list = _supplierService.Get();
            var slectList = new SelectList(list, "Id", "Name");

            ViewBag.SupplierSelectList = slectList;
        }
    }
}