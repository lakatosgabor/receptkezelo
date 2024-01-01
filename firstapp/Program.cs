using firstapp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using firstapp.Services;
using firstapp.Middleware;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddDbContext<RecipeDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<RecipeDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAllergensService, AllergensService>();
builder.Services.AddScoped<IIngredientsService, IngredientsService>();
builder.Services.AddScoped<IRecipesService, RecipesService>();
builder.Services.AddScoped<IBasicMaterialCategoryService, BasicMaterialCategoryService>();
builder.Services.AddScoped<IBasicMaterialService, BasicMaterialService>();
builder.Services.AddScoped<IIngredientGroupService, IngredientGroupService>();
builder.Services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();
builder.Services.AddScoped<IIngredientGrouppingService, IngredientGrouppingService>();

builder.Services.AddControllers();
builder.Services.AddAuthorization();
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
