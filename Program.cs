using Microsoft.EntityFrameworkCore;
using UrlShortener;
using UrlShortener.Contracts;
using UrlShortener.Handlers;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUrlManagementService, UrlManagementService>();
builder.Services.AddHttpContextAccessor();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallback((AppDbContext dbContext, HttpContext ctx) => ApiHandlers.FallbackHandler(dbContext, ctx));

app.Run();
