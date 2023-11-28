using Assignment.Helpers;
using Assignment.Models;
using E_ecommerceAssignment.APi.Filters;
using E_ecommerceAssignment.APi.Middlewares;
using E_ecommerceAssignment.EF;
using E_ecommerceAssignment.EF.Helpers;
using E_ecommerceAssignment.EF.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
{
    
    op.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

})
.AddEntityFrameworkStores<E_ecommerceContext>()
.AddDefaultTokenProviders();
var buid = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<E_ecommerceContext>(options =>
    options.UseSqlServer(buid));

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));


#region Filter
builder.Services.AddScoped<CheckProductEsistesFilter>();
#endregion


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthrServices, AuthrServices>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});


builder.Services.AddSwaggerGen();

var app = builder.Build();
builder.Services.AddCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//انسب مكان ل Middleware
app.UseMiddleware<RateLimitingMiddleware>();
app.UseMiddleware<ProfilingMiddleweares>();
//app.UseMiddleware<PayloadValidationMiddleware>();

 //upload files
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"ProcductImage")),
    RequestPath = new PathString("/ProcductImage")
});
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
