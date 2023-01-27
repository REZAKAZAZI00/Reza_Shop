
namespace Shop.Core.DTOs.Products
{
    public class ProductsViewModel
    {

        public int ProductId { get; set; }
        public int GroupId { get; set; }

        public int SubGroup { get; set; }

        public int ProductStatusId { get; set; }

        public int ProductSellerId { get; set; }
        [Display(Name = "عنوان محصول")]
        public string ProductTitle { get; set; }
        [Display(Name = "شرح محصول")]

        public string DesCription { get; set; }
        [Display(Name = "قیمت محصول")]

        public int Price { get; set; }
        [Display(Name = "قیمت تخفیفی محصول")]

        public int DisCountPrice { get; set; }

        [Display(Name = "اطلاعات")]

        public string Information { get; set; }
        [Display(Name = "وزن محصول")]

        public string Weight { get; set; }
        [Display(Name = "موجودی کالا")]

        public string stock { get; set; }

        [Display(Name = "کلمات کلیدی ")]

        public string Tags { get; set; }


        public List<ProductImage> Images { get; set; }


    }
    public class CreateProductViewModel 
    {
        public int GroupId { get; set; }

        public int SubGroup { get; set; }

        public int ProductStatusId { get; set; }
       
        public int ProductSellerId { get; set; }
        [Display(Name = "عنوان محصول")]
        public string ProductTitle { get; set; }
        [Display(Name = "شرح محصول")]

        public string DesCription { get; set; }
        [Display(Name = "قیمت محصول")]
        
        public int Price { get; set; }
        [Display(Name = "قیمت تخفیفی محصول")]
       
        public int DisCountPrice { get; set; }

        [Display(Name = "اطلاعات")]

        public string Information { get; set; }
        [Display(Name = "وزن محصول")]
        
        public string Weight { get; set; }
        [Display(Name = "موجودی کالا")]
        
        public string stock { get; set; }

        [Display(Name = "کلمات کلیدی ")]
       
        public string Tags { get; set; }

        [Display(Name = "تاریخ  درج")]
       
        public List<IFormFile>  Images { get; set; }
    }

    public class EditProductViewModel
    {

        public int ProductId { get; set; }
        public int GroupId { get; set; }

        public int? SubGroup { get; set; }

        public int ProductStatusId { get; set; }

        public int ProductSellerId { get; set; }
        [Display(Name = "عنوان محصول")]
        public string ProductTitle { get; set; }
        [Display(Name = "شرح محصول")]

        public string DesCription { get; set; }
        [Display(Name = "قیمت محصول")]

        public int Price { get; set; }
        [Display(Name = "قیمت تخفیفی محصول")]

        public int DisCountPrice { get; set; }

        [Display(Name = "اطلاعات")]

        public string Information { get; set; }
        [Display(Name = "وزن محصول")]

        public string Weight { get; set; }
        [Display(Name = "موجودی کالا")]

        public string stock { get; set; }

        [Display(Name = "کلمات کلیدی ")]

        public string Tags { get; set; }


        public List<ProductImage> Images { get; set; }


    }
}
