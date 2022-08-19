using Microsoft.EntityFrameworkCore;
using PokemonStorage.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<Context>(opt =>
    opt.UseInMemoryDatabase("PokemonStorage"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDBInitializer, DBInitializer>();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IDBInitializer s = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
    s.Seed();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
