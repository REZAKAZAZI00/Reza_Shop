namespace Shop.DataLayer.Enttities.Products
{
    public class ProductImage
    {
        
        
        [Key]
        public int ProductimageId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "نام عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string IamgeName { get; set; }

        #region Realtions
        [ForeignKey("ProductId")]
        public Product  Products { get; set; }
        #endregion


    }
}
