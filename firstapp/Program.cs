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

/*hozz�adja a Swagger gener�l�si �s dokument�ci�s funkci�it az alkalmaz�shoz.
 * Ezek a f�ggv�nyek seg�tenek az API-k dokument�l�s�ban �s a Swagger felhaszn�l�i
 * fel�let l�trehoz�s�ban.
 */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Swagger bekapcsol�sa fejleszt�i m�dban
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<HttpRequestLoggingMiddleware>();

app.MapControllers();

app.Run();
