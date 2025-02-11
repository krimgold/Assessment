using Microsoft.Data.Sqlite;
using Polly;
using Polly.Extensions.Http;
using Refactoring.Processors.Payment;
using Refactoring.Repositories;
using Refactoring.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SqliteConnection>(_ => new SqliteConnection(builder.Configuration.GetConnectionString("SqlLite")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();
builder.Services.AddScoped<IPaymentProcessingFactory, PaymentProcessingFactory>();
builder.Services.AddScoped<IConfirmationService, ConfirmationService>();

builder.Services.AddHttpClient<IConfirmationService, ConfirmationService>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["EmailService:Url"]);
}).AddPolicyHandler(GetRetryPolicy());


static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
	return HttpPolicyExtensions
		.HandleTransientHttpError()
		.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
		.WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}

builder.Services.AddControllers()
	.AddJsonOptions(opt => 
	{ 
		opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); 
	});

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

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/error");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
