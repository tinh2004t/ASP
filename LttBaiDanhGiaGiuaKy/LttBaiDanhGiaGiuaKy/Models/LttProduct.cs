using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LttBaiDanhGiaGiuaKy.Models
{
    public class LttProduct
    {
            [Required(ErrorMessage = "Hãy nhập mã số")]
            public int LttId { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
            [StringLength(100, MinimumLength = 5, ErrorMessage = "Tên sản phẩm phải dài từ 5 đến 100 ký tự")]
            [RegularExpression("[A-Z ]+", ErrorMessage = "Tên sản phẩm chỉ chứa ký tự viết hoa và khoảng trắng")]
            public string LttName { get; set; }
            [Required(ErrorMessage = "Hãy nhập ảnh")]
            public string LttImage { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
            [Range(1, 100, ErrorMessage = "Số lượng sản phẩm phải nằm trong khoảng từ 1 đến 100")]
            public int LttQuantity { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
            [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
            public decimal LttPrice { get; set; }
            public bool LttIsActive { get; set; } = true;
    }
}