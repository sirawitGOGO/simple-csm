using csm_backend.Configs;
using csm_backend.Data;
using csm_backend.Models;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

var dataSourceBuilder = new NpgsqlDataSourceBuilder(DbConfig.DbConnectionString);
dataSourceBuilder.MapEnum<UserRole>();
dataSourceBuilder.MapEnum<PostStatus>();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dataSource,
    npgsql =>{
        npgsql.MapEnum<UserRole>();
        npgsql.MapEnum<PostStatus>();
    })
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
