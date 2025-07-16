using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Olympus.Server.Authentication;
using Olympus.Server.Database;
using Olympus.Server.Temp;

var builder = WebApplication.CreateBuilder(args);

// Configure custom configuration path
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
	.AddJsonFile("Configuration/Runtime/Default.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"Configuration/Runtime/{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

// Add authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
		.EnableTokenAcquisitionToCallDownstreamApi()
		.AddInMemoryTokenCaches();

// Add authorization services
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers()
	.AddOData(options => {
		options.EnableQueryFeatures().SetMaxTop(100);
		options.AddRouteComponents("odata", ODataConfig.GetEdmModel());
	}
);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IUserProvider, MicrosoftUserProvider>();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DefaultDatabaseContext>(options =>
	options.UseSqlServer(connectionString)
);

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

app.UseMiddleware<UserProviderMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
	name: "api",
	pattern: "api/{controller}/{action=Index}/{id?}"
);

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
