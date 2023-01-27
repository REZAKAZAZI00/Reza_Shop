

namespace Shop.DataLayer.Enttities.Permissions
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Display(Name="عنوان نقش")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PermissionTitle { get; set; }

        public int? ParentId { get; set; }

        

        #region Relations
        [ForeignKey("ParentId")]
        public virtual List<Permission> Permissions { get; set; }

        public virtual List<RolePermission> RolePermissions  { get; set; }
        #endregion

    }
}
