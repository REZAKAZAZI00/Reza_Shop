using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Project_daneshgae.Pages.Admin.Product
{
    public class EditProductModel : PageModel
    {
        private readonly IProductServices _productServices;
        public EditProductModel(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [BindProperty]
        public EditProductViewModel Product { get; set; }
        public void OnGet(int id)
        {
            Product = _productServices.GetProductForEdit(id);
        }

        public IActionResult OnPost() 
        {
            return Redirect("/admin/product");
        }
    }
}
