using Microsoft.EntityFrameworkCore;
using OrganisationAPI.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<OrganisationAPI.Services.Interfaces.IXLSXParser, OrganisationAPI.Services.XLSXParserService>();
builder.Services.AddTransient<OrganisationAPI.Services.Interfaces.IImportNote, OrganisationAPI.Services.ImportNoteService>();

builder.Services.AddDbContext<OrganisationdbContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
