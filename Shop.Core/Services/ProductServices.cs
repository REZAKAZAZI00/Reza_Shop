

namespace Shop.Core.Services
{
    public class ProductServices: IProductServices
    {
        private readonly ShopDbContext _context;
        public ProductServices(ShopDbContext context)
        {
              _context = context;       
        }

        public void AddProductImage(ProductImage image)
        {
            _context.ProductImages.Add(image);
            _context.SaveChanges();
        }

        public int AddProducts(Product product,List<IFormFile> images)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            #region Image
            if (images != null && images.Count > 0)
            {
                
                foreach (var img in images)
                {
                    if (img.IsImage())
                    {
                        string ImagePass = "";
                        string imagename = NameGenerator.GenerateNameForImage() + Path.GetExtension(img.FileName);
                        ImagePass = Path.Combine(Directory.GetCurrentDirectory(), "wwwRoot/img/Products/", imagename);

                        using var stream = new FileStream(ImagePass, FileMode.Create);
                        img.CopyTo(stream);

                        ImageConvertor imageConvertor = new ();
                       string thumpPass = Path.Combine(Directory.GetCurrentDirectory(), "wwwRoot/img/Products/thump/", imagename);

                        imageConvertor.Image_resize(ImagePass,thumpPass, 150);
                        ProductImage image = new()
                        {
                            IamgeName = imagename,
                            ProductId = product.ProductId,
                        };
                        AddProductImage(image);
                    }
                }
            }
            #endregion
            return product.ProductId;
        }

        public void DeleteProduct(Product product)
        {
            product.IsDelete = true;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public List<ProductGroup> GetAllGgroups()
        {
            return _context.ProductGroups.ToList();
        }

        public List<SelectListItem> GetGroupForManageProduct()
        {
            return _context.ProductGroups.Where(g=>g.ParentId == null)
                .Select(g=> new SelectListItem() 
                {
                     Text=g.GroupTitle,
                     Value=g.GroupId.ToString()
                }).ToList();
        }

        public Product GetProductById(int productId)
        {
            return  _context.Products.Find(productId);
        }

        public EditProductViewModel GetProductForEdit(int productId)
        {
            return _context.Products.Where(p => p.ProductId == productId)
                .Select(p=> new EditProductViewModel() 
                { 
                
                 ProductId =p.ProductId,
                  Images=_context.ProductImages.Where(i=>i.ProductId==productId).ToList(),
                   stock=p.stock,
                    ProductSellerId=p.ProductSellerId,
                     DesCription=p.DesCription, 
                    DisCountPrice=p.DisCountPrice,
                    GroupId=p.GroupId,
                     Information=p.Information,
                      Price=p.Price,
                    ProductStatusId=p.ProductStatusId, 
                     ProductTitle=p.ProductTitle,
                      Weight=p.Weight, SubGroup=p.SubGroup, Tags=p.Tags

                
                }).Single();
        }

        public List<ProductsViewModel> GetProducts(int pageId = 1, string filterTitle = "")
        {
            return _context.Products.Select(c => new ProductsViewModel() 
            { 
                  ProductTitle=c.ProductTitle,
                   Images=c.productImages.Where(i=>i.ProductId==c.ProductId).Select(i=>new ProductImage()
                   { 
                      IamgeName=i.IamgeName,
                       ProductimageId=i.ProductimageId
                   
                   }).ToList(),
                    ProductId=c.ProductId, 
            
            
            }).ToList();
        }

        public List<SelectListItem> GetProductSellerForManageProduct()
        {
            return _context.ProductSellers
                .Select(g => new SelectListItem()
                {
                    Text = g.NameSeller,
                    Value = g.ProductSellerId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetProductsForManageImageProduct()
        {
            return _context.Products
                .Select(g => new SelectListItem()
                {
                    Text = g.ProductTitle,
                    Value = g.ProductId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetProductStatusForManageProduct()
        {
            return _context.productStatuses
                .Select(g => new SelectListItem()
                {
                    Text = g.StatusTitle,
                    Value = g.ProductStatusId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageProduct(int groupId)
        {
            return _context.ProductGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public int UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
