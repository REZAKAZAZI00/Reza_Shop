


namespace Shop.DataLayer.Context
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options):base (options) 
        {

        }

        #region Users
        public DbSet<User>  Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole>  UserRoles { get; set; }

        #endregion

        #region Wallet
        public DbSet<Wallet>  Wallets { get; set; }
        public DbSet<WalletType> WalletType { get; set; }
        #endregion

        #region Permissions
        public DbSet<RolePermission>  RolePermission { get; set; }
        public DbSet<Permission>  Permission { get; set; }
        #endregion

        #region Products    

        public DbSet<ProductGroup>  ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSeller>  ProductSellers { get; set; }

        public DbSet<ProductStatus>  productStatuses { get; set; }
        public DbSet<ProductImage>  ProductImages { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);
            modelBuilder.Entity<ProductGroup>()
                .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<ProductSeller>()
               .HasQueryFilter(p => !p.IsDelete);

            base.OnModelCreating(modelBuilder);
        }

    }
}
