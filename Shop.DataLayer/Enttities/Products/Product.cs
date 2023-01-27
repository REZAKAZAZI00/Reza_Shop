using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataLayer.Enttities.Products
{
    public class Product
    {
        public Product()
        {

        }
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int GroupId { get; set; }


        public int? SubGroup { get; set; }
        
        [Required]
        public int ProductStatusId { get; set; }
        [Required]
        public int ProductSellerId { get; set; }
        [Display(Name="عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ProductTitle { get; set; }
        [Display(Name = "شرح محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public string DesCription { get; set; }
        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public int Price { get; set; }
        [Display(Name = "قیمت تخفیفی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public int DisCountPrice { get; set; }

        [Display(Name="اطلاعات")]
        
        public string Information { get; set; }  
        [Display(Name="وزن محصول")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Weight { get; set; }
        [Display(Name="موجودی کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string stock { get; set; }

       
        [Display(Name = "کلمات کلیدی ")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string  Tags { get; set; }

        [Display(Name = "تاریخ  درج")]
        public DateTime RegisterDate { get; set; }
        [Display(Name="محصول حذف شده؟")]
        public bool IsDelete { get; set; }



        #region Relaions
        [ForeignKey("GroupId")]
        public ProductGroup ProductGroup { get; set; }

        [ForeignKey("SubGroup")]
        public ProductGroup subGroup { get; set; }

        public  ProductSeller  ProductSellers { get; set; }

        public ProductStatus  ProductStatus { get; set; }

        
        public List<ProductImage>  productImages { get; set; }
        #endregion
    }
}
