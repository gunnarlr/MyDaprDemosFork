var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// With dependency injection - Add dapr so that whenever we need those types in code, they are available to us
builder.Services.AddControllers().AddDapr();
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

app.UseAuthorization();

// Pubusub uses cloudevents (envelope)
// Middleware to unwrap cloud-events
app.UseCloudEvents();

app.MapControllers();

// Help with just using attributes to subscribe to events
// Every time something happens on that topic on that pubsub, I'll call you with that payload
// And it will be unwrapped, because of the middleware set up above
app.MapSubscribeHandler();

app.Run();
