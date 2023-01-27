



namespace Shop.Core.Security
{
    public class PermissionCheckerAttribute:AuthorizeAttribute,IAuthorizationFilter
    {
        private readonly int  _PermissionId=0;
        private  IPermissionServics _permissionServics;
        public PermissionCheckerAttribute(int permissionId)
        {
            _PermissionId=permissionId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionServics = (IPermissionServics)context.HttpContext.RequestServices.GetService(typeof(IPermissionServics));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName=context.HttpContext.User.Identity.Name;
                if (!_permissionServics.CheckPermission(_PermissionId,userName))
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
