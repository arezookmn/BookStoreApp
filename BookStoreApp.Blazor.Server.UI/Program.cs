using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Data;
using BookStoreApp.Blazor.Server.UI.Provider;
using BookStoreApp.Blazor.Server.UI.Services;
using BookStoreApp.Blazor.Server.UI.Services.Authentication;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IClient, Client>(
    cl => cl.BaseAddress = new Uri("https://localhost:7231"));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<ApiAuthenticationStateProvider>());

builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true; // Enable detailed error messages
    options.JSInteropDefaultCallTimeout = TimeSpan.FromSeconds(30); // Set a timeout for JavaScript interop calls
    //options = TimeSpan.FromSeconds(30); // Set a keep-alive interval
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
