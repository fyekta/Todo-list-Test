using App.Domain.AppServices;
using App.Domain.Core.Contract.Appservice;
using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Service;
using App.Domain.Services;
using App.Infra.Data.Db.SqlServer.Ef.Dbctx;
using App.Infra.Data.Repos.Ef;
using App.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using WebApi.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodoAppService, TodoAppService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();

var app = builder.Build();
//app.UseMiddleware<GetOnlyMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();