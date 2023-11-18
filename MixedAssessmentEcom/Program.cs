using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Mapper;
using MixedAssessmentEcom.Repository.CartDetailsRepo;
using MixedAssessmentEcom.Repository.MasterCartRepo;
using MixedAssessmentEcom.Repository.PaymentCardRepo;
using MixedAssessmentEcom.Repository.ProductRepo;
using MixedAssessmentEcom.Repository.SalesDetailsRepo;
using MixedAssessmentEcom.Repository.SalesMasterRepo;
using MixedAssessmentEcom.Repository.UserRepository;
using MixedAssessmentEcom.Servise.CartDetailsServ;
using MixedAssessmentEcom.Servise.MasterCartServ;
using MixedAssessmentEcom.Servise.PaymentCardServ;
using MixedAssessmentEcom.Servise.ProductServ;
using MixedAssessmentEcom.Servise.SalesDetailServ;
using MixedAssessmentEcom.Servise.SalesMasterServ;
using MixedAssessmentEcom.Servise.UserServise;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// add database connection through appsetting.json

builder.Services.AddDbContext<UserContext>(options =>
options.
UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));


//add repository

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMasterCartRepository, MasterCartRepository>();
builder.Services.AddScoped<ICartDetailsRepository, CartDetailsRepository>();
builder.Services.AddScoped<IPaymentCardRepository, PaymentCardRepository>();
builder.Services.AddScoped<ISalesMasterRepository, SalesMasterRepository>();
builder.Services.AddScoped<ISalesDetailRepository, SalesDetailRepository>();








// add servise
builder.Services.AddTransient<IUserServ, UserServ>();
builder.Services.AddTransient<IProductServise, ProductServise>();
builder.Services.AddTransient<IMasterCartService, MasterCartService>();
builder.Services.AddTransient<ICartDetailsService, CartDetailsService>();
builder.Services.AddTransient<IPaymentCardService, PaymentCardService>();
builder.Services.AddTransient<ISalesMasterService, SalesMasterService>();
builder.Services.AddTransient<ISalesDetailService, SalesDetailService>();





// newtonsoft jason for a serialization and deserialization
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// automapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

//adding configuration for jwt

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});


builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddControllers();
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
// cores of anugular 
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
