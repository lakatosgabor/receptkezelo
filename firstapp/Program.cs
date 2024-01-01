using firstapp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using firstapp.Services;
using firstapp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RecipeDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
builder.Services.AddScoped<IAllergensService, AllergensService>();
builder.Services.AddScoped<IIngredientsService, IngredientsService>();
builder.Services.AddScoped<IRecipesService, RecipesService>();
builder.Services.AddScoped<IBasicMaterialCategoryService, BasicMaterialCategoryService>();
builder.Services.AddScoped<IBasicMaterialService, BasicMaterialService>();
builder.Services.AddScoped<IIngredientGroupService, IngredientGroupService>();

builder.Services.AddControllers();

/*hozzáadja a Swagger generálási és dokumentációs funkcióit az alkalmazáshoz.
 * Ezek a függvények segítenek az API-k dokumentálásában és a Swagger felhasználói
 * felület létrehozásában.
 */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Swagger bekapcsolása fejlesztõi módban
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<HttpRequestLoggingMiddleware>();

app.MapControllers();

app.Run();
