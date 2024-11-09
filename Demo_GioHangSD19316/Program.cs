using Demo_GioHangSD19316.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GHangDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//khai báo d?ch v? session
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(15); //thi?t l?p cho quá trình session là 15s = k?t thuusc sau 15s
    //n?u th?c 1 hi?n 1 hành ??ng trong kho?ng time 15s r?i ti?p t?c th?c hi?n
    //1 hành ??ng thì thì sesion s? reset v? 0 
    

});
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
app.UseSession(); // kh?i t?o ?? s? d?ng session

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
