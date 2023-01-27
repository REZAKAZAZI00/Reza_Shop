namespace Shop.Project_daneshgae.Conponent
{
    public class ProductGroupComponent : ViewComponent
    {
        private readonly IProductServices  _productServices;
        public ProductGroupComponent(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {

            return await Task.FromResult((IViewComponentResult) View("ProductGroup",_productServices.GetAllGgroups()) );

          
        }
    }
}
