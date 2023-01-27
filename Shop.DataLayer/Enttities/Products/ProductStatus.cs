using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataLayer.Enttities.Products
{
    public class ProductStatus
    {
        public ProductStatus()
        {

        }
        [Key]
        public int ProductStatusId { get; set; }

        [Display(Name="نام وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StatusTitle { get; set; }

        #region Relations
        public List<Product>  Productss { get; set; }
        #endregion
    }
}
