using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TMT_Code_Migration1.Data;
using TMT_Code_Migration1.DataLogics.Utility;

var builder = WebApplication.CreateBuilder(args);
//register the controiller and views 
builder.Services.AddControllersWithViews();
//register teh common Data logic file
builder.Services.AddScoped<CommonDl>();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


//middlewire to accept the file in xml format
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true; // Respect Accept headers
}).AddXmlSerializerFormatters(); // Add XML Formatter

//register the jwt token authentication service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWTKey:validIssuer"],
        ValidAudience = builder.Configuration["JWTKey:validAudience"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_dominionva_key_2022")) // not using this as this will rise the 'HS256' issue hashed key issue 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:key"])) // using this to resolve the token issue 
    };
});


var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

//custom auth valiadtor middleware
app.UseCustomUnauthorizedHandling();
//to use the authentication we need to tell the built to use it 
app.UseAuthentication();
app.UseAuthorization();
//custom error handling if the route exist or not
app.UseCustomNotFoundHandling();
//custom method available or not exeception 
app.UseCustomMethodNotAllowedHandling();
// Use custom middleware
app.UseCustomExceptionHandling();



app.MapControllers();
app.Run();
