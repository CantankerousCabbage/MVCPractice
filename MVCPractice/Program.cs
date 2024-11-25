using Microsoft.EntityFrameworkCore;
using MVCPractice.Data;
using MVCPractice.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Inject DBContext inside application services
builder.Services.AddDbContext<MvcDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));


//Inject Itag Interface into services. If ItagRepository called instead give them the repository
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

var app = builder.Build();

// Middleware ----------------

// Configure the HTTP request pipeline. Apps Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Middleware ^ ----------------

app.Run();
