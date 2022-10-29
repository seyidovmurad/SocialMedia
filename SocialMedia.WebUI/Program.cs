using Microsoft.AspNetCore.Authentication.Cookies;
using SocialMedia.Business.Abstract;
using SocialMedia.Business.Concrete;
using SocialMedia.DataAccess.Abstract;
using SocialMedia.DataAccess.Concrete.EfEntityFramwork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });


builder.Services.AddScoped<IUserDal, UserDal>();
builder.Services.AddScoped<IPersonDal, PersonDal>();
builder.Services.AddScoped<IFriendDal, FriendDal>();
builder.Services.AddScoped<IPostDal, PostDal>();
builder.Services.AddScoped<ITagDal, TagDal>();
builder.Services.AddScoped<IPostTagDal, PostTagDal>();

builder.Services.AddScoped<IFriendService, FriendManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IPostService, PostManager>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
