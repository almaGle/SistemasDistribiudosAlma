using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SoapApi.Contracts;
using SoapApi.Infrastructure;
using SoapApi.Repositories;
using SoapApi.Services;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserContract, UserService>();
builder.Services.AddScoped<IBookRepository, BookRepository>(); // Tu repositorio para libros
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddDbContext<RelationalDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseSoapEndpoint<IUserContract>("/UserService.svc", new SoapEncoderOptions());
app.UseSoapEndpoint<IBookService>("/BookService.svc", new SoapEncoderOptions());

app.Run();