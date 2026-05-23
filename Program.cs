using App08_TodoDb.Components;
using App08_TodoDb.Data;
using App08_TodoDb.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "todo.db");
builder.Services.AddDbContext<TodoDbContext>(o => o.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddScoped<TodoDbService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<TodoDbContext>().Database.EnsureCreated();

if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Error", createScopeForErrors: true); app.UseHsts(); }
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
