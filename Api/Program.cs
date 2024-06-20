using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMassTransit(busConfigurator => {
  busConfigurator.SetKebabCaseEndpointNameFormatter();
  busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
  {
    busFactoryConfigurator.Host("localhost", hostConfigurator => { });
  });
});


var app = builder.Build();

app.UseCors(x => x
  .AllowAnyOrigin()
  .AllowAnyMethod()
  .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
