using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Payload.Converter;
using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.PayLoad.Converter;
using DoAnMonHocBE.PayLoad.DTO;
using DoAnMonHocBE.PayLoad.Response;
using DoAnMonHocBE.Service.Implements;
using DoAnMonHocBE.Service.Interface;
using DoAnMonHocBE.Service.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Thêm CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "MyPolicy",
//        policy =>
//        {
//            policy.WithOrigins("*")
//                    .AllowAnyMethod().AllowAnyHeader();
//        });
//});



//Ket noi database
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ResponseBase>();

// DTO
builder.Services.AddScoped<ResponseObject<DTO_Role>>();
builder.Services.AddScoped<ResponseObject<DTO_User>>();
builder.Services.AddScoped<ResponseObject<DTO_Comic>>();
builder.Services.AddScoped<ResponseObject<DTO_ComicType>>();
builder.Services.AddScoped<ResponseObject<DTO_History>>();
builder.Services.AddScoped<ResponseObject<DTO_Hobby>>();
builder.Services.AddScoped<ResponseObject<DTO_Comment>>();



// Converter
builder.Services.AddScoped<Converter_Role>();
builder.Services.AddScoped<Converter_User>();
builder.Services.AddScoped<Converter_Comic>();
builder.Services.AddScoped<Converter_ComicType>();
builder.Services.AddScoped<Converter_History>();
builder.Services.AddScoped<Converter_Hobby>();
builder.Services.AddScoped<Converter_Comment>();



// IService, Service
builder.Services.AddScoped<IService_Role, Service_Role>();
builder.Services.AddScoped<IService_User, Service_User>();
builder.Services.AddScoped<IService_Comic, Service_Comic>();
builder.Services.AddScoped<IService_ComicType, Service_ComicType>();
builder.Services.AddScoped<IService_History, Service_History>();
builder.Services.AddScoped<IService_Hobby, Service_Hobby>();
builder.Services.AddScoped<IService_Comment, Service_Comment>();




builder.Services.AddScoped<AppDbContext>();



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

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // Kích hoat CORS

//app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
