using ChTi.Service.Implemention.Injector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
await builder.Services.AddChTiServicesAsync(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(x =>
{
    x.AllowAnyHeader().
            AllowAnyMethod()
            .AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
