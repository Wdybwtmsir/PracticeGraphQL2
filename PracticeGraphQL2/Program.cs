using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using PracticeGraphQL2.DataAccess.Data;
using PracticeGraphQL2.DataAccess.Entity;
using PracticeGraphQL2.DataAccess.DAO;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddDbContext<SampleAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer().AddQueryType<Query>().
    AddMutationType<Mutation>().AddSubscriptionType<Subscription>().AddInMemorySubscriptions();
builder.Services.AddScoped<PassengerRepository, PassengerRepository>();
builder.Services.AddScoped<SeatRepository, SeatRepository>();
builder.Services.AddScoped<TicketRepository, TicketRepository>();
builder.Services.AddScoped<TrainRepository, TrainRepository>();
builder.Services.AddScoped<CarriageRepository, CarriageRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);
app.UseWebSockets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<SampleAppDbContext>();
    DataSeeder.SeedData(db);
}
app.MapGraphQL("/graphql");
app.Run();