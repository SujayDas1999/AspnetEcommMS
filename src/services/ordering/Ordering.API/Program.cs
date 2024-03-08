using Ordering.API.Extentions;
using Ordering.Application;
using Ordering.Infra;
using Ordering.Infra.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.ApplicationContext();
builder.Services.AddInfrastructureConfiguration(builder.Configuration);
//builder.Services.AddInfraServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    OrderSeed.SeedAsync(context).Wait();
});

app.Run();
