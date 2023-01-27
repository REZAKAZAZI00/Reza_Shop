
namespace Shop.Project_daneshgae.Pages.Admin.Product
{
    [PermissionChecker(12)]
    public class CreateProductModel : PageModel
    {
        private readonly IProductServices _productServices;
        public CreateProductModel(IProductServices productServices)
        {
           _productServices= productServices;
        }
        [BindProperty]
        public CreateProductViewModel CreateProductViewModel { get; set; }
        public void OnGet()
        {

            var groups = _productServices.GetGroupForManageProduct();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subgroups = _productServices.GetSubGroupForManageProduct(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subgroups, "Value", "Text");

            var status = _productServices.GetProductStatusForManageProduct();
            ViewData["ProductStatusId"] = new SelectList(status, "Value", "Text");

            var sellers = _productServices.GetProductSellerForManageProduct();
            ViewData["ProductSeller"] = new SelectList(sellers, "Value", "Text");

        }



        public IActionResult OnPost()
        {
            DataLayer.Enttities.Products.Product product = new() 
            {
               IsDelete=false,
               RegisterDate=DateTime.Now,
                DesCription=CreateProductViewModel.DesCription,
                 Price=CreateProductViewModel.Price, 
                  DisCountPrice=CreateProductViewModel.DisCountPrice,
                   Information=CreateProductViewModel.Information, 
                ProductSellerId=CreateProductViewModel.ProductSellerId,
                 stock=CreateProductViewModel.stock,
                  Tags=CreateProductViewModel.Tags,
                   GroupId=CreateProductViewModel.GroupId, 
                     ProductTitle=CreateProductViewModel.ProductTitle, 
                ProductStatusId=CreateProductViewModel.ProductStatusId,
                 Weight=CreateProductViewModel.Weight, 
            };
            var pId = _productServices.AddProducts(product,CreateProductViewModel.Images);
            
            return Redirect("/Admin/product");
        }
    }
}
