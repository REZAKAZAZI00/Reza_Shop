


namespace Shop.Core.Services.InterFace
{
    public interface IProductServices
    {


        #region ProductGroup

        List<ProductGroup> GetAllGgroups();
        List<SelectListItem> GetGroupForManageProduct();
        List<SelectListItem> GetSubGroupForManageProduct(int groupId);
        List<SelectListItem> GetProductStatusForManageProduct();

        #endregion
        #region Image
        void AddProductImage(ProductImage image);
        #endregion
        #region Products
        int AddProducts(Product product,List<IFormFile> images);
        List<SelectListItem> GetProductsForManageImageProduct();
        List<ProductsViewModel> GetProducts(int pageId=1,string filterTitle="");
        Product GetProductById(int productId);
        EditProductViewModel GetProductForEdit(int productId);
        void DeleteProduct(Product product);
        int  UpdateProduct(Product product);
        #endregion

        #region Seller
        List<SelectListItem> GetProductSellerForManageProduct();
        #endregion

    }
}
