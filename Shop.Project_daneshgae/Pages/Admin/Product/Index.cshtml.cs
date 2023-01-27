using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Project_daneshgae.Pages.Admin.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductServices  _productServices;
        public IndexModel(IProductServices productServices)
        {
            _productServices=productServices;
        }
        public List<ProductsViewModel> ListProduct  { get; set; }
        public void OnGet()
        {
            ListProduct=_productServices.GetProducts();
        }
    }
}
