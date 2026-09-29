using csm_backend.Configs;
using csm_backend.Data;
using dotenv.net;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql(DbConfig.DbConnectionString));
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
