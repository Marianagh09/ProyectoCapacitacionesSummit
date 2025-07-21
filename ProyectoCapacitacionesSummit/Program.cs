using System.Reflection;
using System.Text;
using CAP.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sif.Base;
namespace ProyectoCapacitacionesSummit
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			_ = ConsoleObserver.Instance;
			// Configure CORS
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAngular", policy =>
					{
						policy.WithOrigins("http://localhost:4200")
							.AllowAnyHeader()
							.AllowAnyMethod();
					});
			});

			builder.Services.AddSingleton<StartJWT>(provider =>
			{
				var configuration = provider.GetRequiredService<IConfiguration>();
				return new StartJWT(configuration);
			});

			//Configure authentication jwt 
			builder.Services.AddAuthentication("Bearer")
	 .AddJwtBearer("Bearer", options =>
	 {
		 options.TokenValidationParameters = new TokenValidationParameters
		 {
			 ValidateIssuerSigningKey = true,
			 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
			 ValidateIssuer = true,
			 ValidIssuer = builder.Configuration["Jwt:Issuer"],
			 ValidateAudience = true,
			 ValidAudience = builder.Configuration["Jwt:Audience"],
			 ValidateLifetime = true,
			 ClockSkew = TimeSpan.Zero
		 };
	 });

			builder.Services.AddAuthorization();

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();

			// locura de oswaldo
			builder.Services.AddSwaggerGen(c =>
			{
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				//c.IncludeXmlComments(xmlPath); //SE INCLUYEN COMETARIOS XML

				//HABILITAR AUTORIZACIN EN SWAGGER
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Autenticacion JWT usando el esquema Bearer. \r\n\r\n " +
					 "Ingresa la palabra 'Bearer' seguida de un [espacio] y despues su token en el campo de abajo \r\n\r\n " +
					 "Ejemplo : \"Bearer tkdjfjdkdkd\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
	 {
		  {
				new OpenApiSecurityScheme
				{
					 Reference = new OpenApiReference
					 {
						  Type = ReferenceType.SecurityScheme,
						  Id = "Bearer"
					 },
					 Scheme = "oauth2",
					 Name = "Bearer",
					 In = ParameterLocation.Header
				},
				new List<string>()
		  }

	 });
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("AllowAngular");
			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}
