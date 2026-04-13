using Microsoft.EntityFrameworkCore;
using XmlReader.Data.Context;
using XmlReader.Data.Repositories;
using XmlReader.Entities;
using XmlReader.Interfaces;
using XmlReader.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IRepository<XML>, Repository<XML>>();
builder.Services.AddScoped<IRepository<FileTable>, Repository<FileTable>>();
builder.Services.AddScoped<IXmlService, XmlService>();

var app = builder.Build();

// Aplica migrations automaticamente na inicialização
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();