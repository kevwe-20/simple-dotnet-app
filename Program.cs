using Ampifan.GraphQL;
using Ampifan.GraphQL.Queries;
using Ampifan.GraphQL.Mutations;
using Microsoft.EntityFrameworkCore;
using Ampifan.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPooledDbContextFactory<AppDBContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("AmpifanDb")));


builder.Services
    .AddScoped<INotificationService, NotificationService>()
    .AddGraphQLServer()
    // .AddDefaultTransactionScopeHandler()
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<ActiveChatQuery>()
    .AddTypeExtension<AnalyticQuery>()
    .AddTypeExtension<CategoryQuery>()
    .AddTypeExtension<ConnectionQuery>()
    .AddTypeExtension<InviteCodeQuery>()
    .AddTypeExtension<NotificationQuery>()
    .AddTypeExtension<TalentQuery>()
    .AddTypeExtension<UserQuery>()
    .AddTypeExtension<UserConnectionQuery>()
    .AddTypeExtension<VideoQuery>()
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<ActiveChatMutation>()
    .AddTypeExtension<CategoryMutation>()
    .AddTypeExtension<InviteCodeMutation>()
    .AddTypeExtension<NotificationMutation>()
    .AddTypeExtension<TalentMutation>()
    .AddTypeExtension<UserConnectionMutation>()
    .AddTypeExtension<UserMutation>()
    .AddTypeExtension<VideoMutation>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    ;

var app = builder.Build();
app.MapGraphQL();
app.MapGraphQLVoyager("ui/voyager");
app.MapGet("/", () => "Ampifan is up!");

app.Run();
