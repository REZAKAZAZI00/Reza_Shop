

namespace Shop.Project_daneshgae.Pages.Admin.ProductGroup
{
    public class IndexModel : PageModel
    {
        private readonly IProductServices _productServices;
        public IndexModel(IProductServices productServices)
        {
                _productServices=productServices;
        }
        public List<Shop.DataLayer.Enttities.Products.ProductGroup>  ProductGroups { get; set; }
        public void OnGet()
        {
            ProductGroups = _productServices.GetAllGgroups();
        }
    }
}
