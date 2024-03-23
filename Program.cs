
using DBService;
using Structure;
using Service;
using UtilityService.Service;
using DBservice;
using Automation;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITable, GetTableID>();
builder.Services.AddScoped<ITableName,GetTableName>();
builder.Services.AddScoped<IPhoneCode,GetPhoneCode>();
builder.Services.AddScoped<IPlaceInformation,GetPlaceInformation>();
builder.Services.AddScoped<ICountryName,GetCountryName>();
builder.Services.AddScoped<IStateNames,GetStateNames>();
builder.Services.AddScoped<ICityNames,GetCityName>();
builder.Services.AddScoped<IZipCodeNames,GetZipCode>();
builder.Services.AddScoped<IState,GetStateId>();
builder.Services.AddScoped<ICityState,GetCityState>();
builder.Services.AddScoped<IZipOfState,GetZipOfState>();
builder.Services.AddScoped<LocationInfoDBService>();

builder.Services.AddScoped<LocationInfoService>();
builder.Services.AddTransient<ISalt, SaltGeneratorService>();
builder.Services.AddTransient<IGenerateOtp, GetOtp>();
builder.Services.AddScoped<IGetPhoneCodeFromID, GetPhoneCodeFromCountryID>();
builder.Services.AddScoped<GetPhoneCodeService>();
builder.Services.AddScoped<FetchCurrencyConversionRates>();
builder.Services.AddScoped<UpdateCurrencyDbService>();
builder.Services.AddHostedService<ExchangeRateUpdateService>();
builder.Services.AddScoped<CurrencyServices>();
builder.Services.AddScoped<CurrencyDBServcie>();
builder.Services.AddScoped<LocationDataAccess>();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
