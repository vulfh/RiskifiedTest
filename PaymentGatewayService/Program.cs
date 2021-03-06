using CreditCardConnectors;
using PaymentGatewayService.Data;
using Processors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://localhost:8000");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/healthcheck", () =>
{
    return "SUCCESS";
})
.WithName("Get/Api/HealthCheck");

app.MapPost("/api/charge", async (HttpRequest request) => {

    var chargeDetails = await request.ReadFromJsonAsync<ChargeDetails>();

    string merchanrIndentifier = request.Headers["merchant-identifier"];
    IResult result;
    if (merchanrIndentifier != null)
    {
        if (ValidatorsManager.Instance.Validate(chargeDetails))
        {
           
            var response = await ChargeCard(chargeDetails);
            if (response.Success)
            {
                result = Results.Ok();
            }
            else
            {
                result = Results.BadRequest("Card Declined");
            }
            return result;
        }
        else
        {
            result =  Results.BadRequest();
        }
    }
    else
    {
        result= Results.BadRequest();
    }
    return result;
    
})
    .WithName("Post/Api/Charge");


app.Run();

static async Task<Response> ChargeCard(ChargeDetails chargeDetails)
{

    IResult result = Results.BadRequest();
    var connector = CreditCardConnectorFactory.Create(chargeDetails.creditCardCompany);
    var response = await Utils.RetryActionAsync<Response>(
        new Func<Task<Response>>(async () => { return await connector.sendChargeRequest(chargeDetails); })
        , (response) => !response.TechnicalFailure
        , 3);
    return response;
    
}

