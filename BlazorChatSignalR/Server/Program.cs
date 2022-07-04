using BlazorChatSignalR.Server.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//1.Agregar los servicios 
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options => 
    options.MimeTypes = ResponseCompressionDefaults
    .MimeTypes
    .Concat(new[] {"application/octet-stream "})
);

var app = builder.Build();
// 2. 
app.UseResponseCompression();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
//3. para conectar con el cliente 
app.MapHub<ChatHub>("/chathub");

app.MapFallbackToFile("index.html");

app.Run();
