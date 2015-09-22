using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Services.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Display (Name = "ชื่อสินค้า")]
        [Required (ErrorMessage = "กรุณาใส่ชื่อสินค้า")]
        public string Name { get; set; }

        [Display(Name = "ประเภทสินค้า")]
        public int ProductTypeId { get; set; }

        [Display(Name = "ประเภทสินค้า")]
        public string ProductTypeName { get; set; }

        [Display(Name = "บริษัท")]
        public int SupplierId { get; set; }

        [Display(Name = "บริษัท")]
        public string SupplierName { get; set; }

    }
}