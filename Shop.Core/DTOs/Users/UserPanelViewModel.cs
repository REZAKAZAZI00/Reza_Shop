

namespace Shop.Core.DTOs
{
    public class InformationUserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string CodeMelli { get; set; }
        public string UserAvatar { get; set; }
        public int  Wallet { get; set; }
        public DateTime CreateDate { get; set; }

    }
    public class SlideBserUserPanelViewModel 
    {
      
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class EditProfileUserPanelViewModel
    {
     
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }
        
        [Display(Name = "نام")]
        [MaxLength(25, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Name { get; set; }
        [Display(Name = "نام خانوداگی")]
        [MaxLength(35, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Lname { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(110, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "کد ملی")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CodeMelli { get; set; }
        [Display(Name = "کد پستی")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PostalCode { get; set; }
        [Display(Name = "شماره تلفن")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PhoneNamber { get; set; }

        [Display(Name = "آواتار")]
        public IFormFile UserAvatar { get; set; }

        public string AvatarName { get; set; }
    }

    public class ChangePassWordViewModel 
    {
        [Display(Name = "کلمه عبور فعلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(6, ErrorMessage = "{0}نمی تواند کم تر از {1}کلمه باشد")]
        public string OldPassword { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(6, ErrorMessage = "{0}نمی تواند کم تر از {1}کلمه باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("Password", ErrorMessage = "کلمه های عبور یکسان نیست")]

        public string RePassword { get; set; }

    }

}
