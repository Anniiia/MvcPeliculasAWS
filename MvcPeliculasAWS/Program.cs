using Amazon.S3;
using MvcPeliculasAWS.Helpers;
using MvcPeliculasAWS.Models;
using MvcPeliculasAWS.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string jsonSecrets = await HelperSecretManager.GetSecretsAsync();
KeysModel keyModel = JsonConvert.DeserializeObject<KeysModel>(jsonSecrets);
builder.Services.AddSingleton<KeysModel>(x=>keyModel);
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<ServiceApiPeliculas>();
builder.Services.AddTransient<ServiceStorageAWS>();
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
