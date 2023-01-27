
namespace Shop.DataLayer.Enttities.Wallet
{
    public class Wallet
    {
        public Wallet()
        {

        }
        [Key]
        public int WalletId { get; set; }

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TypeId { get; set; }

        [Display(Name = " کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "شرح")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
        [Display(Name="تایید شده")]
        public bool IsPay { get; set; }
        [Display(Name="تاریخ وساعت")]
        public DateTime CreateDate { get; set; }

        #region Relations
       
        public virtual User.User User { get; set; }
        [ForeignKey("TypeId")]
        public virtual WalletType  WalletType { get; set; }
        #endregion

    }
}
