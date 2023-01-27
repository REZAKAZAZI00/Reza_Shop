using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataLayer.Enttities.Products
{
    public class ProductSeller
    {
        public ProductSeller()
        {

        }
        [Key]
        public int ProductSellerId { get; set; }
        public int UserId { get; set; }
        [Display(Name="نام فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string NameSeller { get; set; }
        [Display(Name = "تاریخ  درج")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "محصول حذف شده؟")]
        public bool IsDelete { get; set; }

        #region Relations
        public  List<Product>  Products { get; set; }
        public virtual User.User Users { get; set; }
        #endregion
    }
}
