using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
// using Olympus.Server.Authentication;
// using Olympus.Server.Database;
using Olympus.Backend.Api.Temp;
using Olympus.Architect.Infrastructure.Database;
using Olympus.Architect.Domain.Repositories;
using Olympus.Architect.Infrastructure.Repositories;
using Olympus.Architect.Application.Services;
using Olympus.Architect.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
		.EnableTokenAcquisitionToCallDownstreamApi()
		.AddInMemoryTokenCaches();

// Add Authorization service
builder.Services.AddAuthorization();

// Add Database service
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<DefaultDatabaseContext>(options =>
// 	options.UseSqlServer(connectionString)
// );

// Temp
builder.Services.AddDbContext<DefaultDatabaseContext>(options => options.UseInMemoryDatabase("Olympus"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

// Add OData Protocol service
builder.Services.AddControllers()
	.AddOData(options => {
		options.EnableQueryFeatures().SetMaxTop(100);
		options.AddRouteComponents("odata", ODataConfig.GetEdmModel());
	}
);

// Add OpenApi service: https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

// builder.Services.AddScoped<IUserProvider, MicrosoftUserProvider>();

// builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
	app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

// app.UseMiddleware<UserProviderMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
	name: "api",
	pattern: "api/{controller}/{action=Index}/{id?}"
);

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

public partial class Program { }
