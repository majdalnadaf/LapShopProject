using DataAccess.DaModels.Interfaces;
using DataAccess.DaModels;
using DataAccess;
using Domains.Models;
using Domains.Models.VwAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAccess.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json;
using LapShopProject.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core;


var builder = WebApplication.CreateBuilder(args);




#region Configure MVC
//builder.Services.AddControllersWithViews().AddJsonOptions(options =>
//{
//    //options.JsonSerializerOptions.MaxDepth = 0;
//    //options.JsonSerializerOptions.ReferenceHandler =  ReferenceHandler.IgnoreCycles;    
//    //options.JsonSerializerOptions.ReferenceHandler =  ReferenceHandler.Preserve;
//    // options.JsonSerializerOptions.PropertyNamingPolicy = null;
//    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true
//});

builder.Services.AddControllersWithViews();



#endregion


#region Connection String for Database
builder.Services.AddDbContext<DbLapShopContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region Custom Dependency injection
builder.Services.AddScoped<IItem, ClsItem>();
builder.Services.AddScoped<ICategory, ClsCategory>();
builder.Services.AddScoped<IOs, ClsOs>();
builder.Services.AddScoped<IItemType, ClsItemType>();
builder.Services.AddScoped<IVwItem, ClsVwItem>();
builder.Services.AddScoped<IItemImage, ClsItemImage>();
builder.Services.AddScoped<ISetting, ClsSetting>();
builder.Services.AddScoped<ISlider, ClsSlider>();
builder.Services.AddScoped<IItemDiscount, ClsItemDiscount>();
builder.Services.AddScoped<ISalesInvoice, ClsSalesInvoice>();
builder.Services.AddScoped<ISalesInvoiceItems, ClsSalesInvoiceItems>();
builder.Services.AddScoped<IVwSalesInvoice, ClsVwSalesInvoice>();
builder.Services.AddScoped<ICountry, ClsCountry>();
builder.Services.AddScoped<IDeliveryInfo, ClsDeliveryInfo>();
builder.Services.AddScoped<IDelivery, ClsDelivery>();
builder.Services.AddScoped<ICashTransaction, ClsCashTransacion>();
builder.Services.AddScoped<IContactInfo, ClsContactInfo>();
builder.Services.AddScoped<IController, ClsController>();
builder.Services.AddScoped<IAction, ClsAction>();
builder.Services.AddScoped<IUserRole, ClsUserRole>();
builder.Services.AddScoped<ISupplier, ClsSupplier>();
builder.Services.AddScoped<IPurchaseInvoice , ClsPurchaseInvoic>();
builder.Services.AddScoped<IPurchaseInvoiceItem , ClsPurchaseInvoiceItem>();
builder.Services.AddScoped<IPage, ClsPage>();
builder.Services.AddScoped<IReview,ClsReview>();
builder.Services.AddScoped<ICustomer, ClsCustomer>();



#endregion


#region Jwt Config 

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(builder.Configuration["Jwt:Key"])),

//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = false,
//        ValidateIssuerSigningKey = true

//    };
//});

#endregion


#region Asp Identity Config
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{

    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.User.RequireUniqueEmail = true;




}).AddEntityFrameworkStores<DbLapShopContext>();
#endregion

#region Session and Cookies
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/UserAccount/Denied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.LoginPath = "/UserAccount/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
    options.Cookie.Path = "/";


});

#endregion


#region Swagger Config 
//var enableSwaggerUI = builder.Configuration.GetValue<bool>("EnableSwaggerUI");

//if (enableSwaggerUI)
//{


//    builder.Services.AddSwaggerGen(options =>
//    {
//        options.SwaggerDoc("itemApis", new OpenApiInfo()
//        {
//            Version = "itemApis",
//            Title = "LapShop Item Apis Doc",
//            Description = "Api for get All item and get item by id , add and apdate item , delete item",
//            //TermsOfService = new Uri("http://itlengnd.net"),
//            Contact = new OpenApiContact()
//            {
//                Email = "majdalnadaf8@gmail.com",
//                Name = "Majd Al nadaf",
//            },
//            License = new OpenApiLicense()
//            {
//                Name = "License"
//            }


//        });

//        //var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//        //var fullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
//        //options.IncludeXmlComments(fullPath);

//    });



//}

#endregion



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseDeveloperExceptionPage();


//app.UseSwagger();

//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("swagger/itemApis/swagger.json", "itemApis");
//    options.RoutePrefix = "";

//});


app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();


app.UseAuthorization();




app.UseEndpoints(endpoints =>
{

    _ = endpoints.MapControllerRoute(

     name: "Admin",
     pattern: "/{area:exists}/{controller}/{action}"

     );

    //----------------------------------------
    _ = endpoints.MapControllerRoute(

        name: "Admin",
        pattern: "/{area:exists}/{controller}/{action}/{id?}"

      );

    //--------------------------------------------

    _=endpoints.MapControllerRoute(

        name: "default",
        pattern: "/{controller}/{action}",
        defaults: new { controller = "Home", action = "Index" }

      );





});





app.Run();








