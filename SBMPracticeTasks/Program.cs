using Microsoft.EntityFrameworkCore;
using SBMPracticeTasks;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigin = "_myAllowSpecificOrigin";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// enable cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigin, builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
}

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigin);
app.UseAuthorization();

app.MapControllers();

app.Run();