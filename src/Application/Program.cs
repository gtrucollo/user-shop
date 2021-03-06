using Application.Services;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("PoliticaCors", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<DatabaseContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("UserShop"), b => b.MigrationsAssembly("Application")));

builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<IPedidoVendaRepository, PedidoVendaRepository>();

builder.Services.AddSingleton(sp => new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus")));
builder.Services.AddSingleton(sp => new ServiceBusAdministrationClient(builder.Configuration.GetConnectionString("ServiceBus")));

builder.Services.AddSingleton<FilaPedidoVenda>();
builder.Services.AddHostedService<FilaPedidoVendaWorker>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("PoliticaCors");
app.Run();
