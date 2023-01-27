
namespace Shop.DataLayer.Enttities.Products
{
    public class ProductGroup

    {
        public ProductGroup()
        {

        }
        [Key]
        public int GroupId { get; set; }
        [Display(Name="عنوان گروه ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }
        [Display(Name = "حذف شده")]
        public bool IsDelete  { get; set; }
        [Display(Name = "گروه اصلی ")]
        public int? ParentId { get; set; }




        #region Relations
        [ForeignKey("ParentId")]
        public List<ProductGroup> ProductGroups { get; set; }
        [InverseProperty("ProductGroup")]
        public  List<Product> Products { get; set; }

        [InverseProperty("subGroup")]
        public List<Product> SubProduct { get; set; }
        #endregion

    }
}
