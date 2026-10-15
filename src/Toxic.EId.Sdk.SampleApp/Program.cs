using Toxic.EId.Sdk;
using Toxic.EId.Sdk.SampleApp.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true);

// Add services to the container.
builder.Services.AddControllersWithViews(cfg =>
{
    cfg.Filters.Add<TrapExceptionsFilter>();
});

// get these from somewhere safe (like a key vault) or via environment variables
// builder.Services.Configure<EIdOptions>(opts =>
// {
//     opts.ApiKey = "abc123";
//     opts.BankId.AuthServiceKey = "abc123";
//     opts.BankId.SignServiceKey = "abc123";
//     opts.IsTest = true;
//     opts.UseEuEndpoint = true;
// });
builder.Services.AddBankIdClient();

builder.Services.AddHybridCache();

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
