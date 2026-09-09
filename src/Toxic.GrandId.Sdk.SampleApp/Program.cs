using Toxic.GrandId.Sdk;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

// use below code to read the api keys from somewhere or put them in appsettings.Local.json or user secrets
// builder.Services.Configure<GrandIdOptions>(opts =>
// {
//     opts.ApiKey = "abc123";
//     opts.ServiceKey = "asd";
// });
builder.Services.AddBankIdClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
