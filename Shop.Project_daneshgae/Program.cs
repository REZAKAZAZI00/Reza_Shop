

var builder = WebApplication.CreateBuilder(args);
 #region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

});

#endregion
   #region DataBase Context
var connectionString = builder.Configuration.GetConnectionString("ShopConnection");
builder.Services.AddDbContext<ShopDbContext>(x => x.UseSqlServer(connectionString));

    #endregion
   #region IOC

builder.Services.AddTransient<IUserServics,UserServics> ();
builder.Services.AddTransient<IViewRenderService,RenderViewToString> ();
builder.Services.AddTransient<IPermissionServics, PermissionServics>();
builder.Services.AddTransient<IProductServices,ProductServices>();

#endregion

// Add services to the container.
builder.Services.AddMvc();
    
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    
    endpoints.MapRazorPages();
});
app.Run();
