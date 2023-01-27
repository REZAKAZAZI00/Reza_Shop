using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Project_daneshgae.Pages.Admin.Product
{
    public class DeleteProductModel : PageModel
    {
        private readonly IProductServices _productServices;
        public DeleteProductModel(IProductServices productServices)
        {
            _productServices = productServices;
        }
       
        public DataLayer.Enttities.Products.Product  Product { get; set; }
        public void OnGet(int id)
        {
            Product = _productServices.GetProductById(id);

            ViewData["UserId"] = id;
        }

        public IActionResult OnPost(int UserId) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var product = _productServices.GetProductById(UserId);
            
            _productServices.DeleteProduct(product);
            return Redirect("/admin/product");
        }
    }
}
