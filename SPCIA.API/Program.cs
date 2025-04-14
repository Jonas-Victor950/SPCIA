using BRCSystem.ClassLibrary.Authorization;
using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.Helpers;
using BRCSystem.ClassLibrary.Logs;
using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SPCIA.API.Services;
using System.Text.Json.Serialization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    services.ConfigureDbContext(builder.Configuration);

    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUnitMeasurementService, UnitMeasurementService>();
    services.AddScoped<IMachineProfileService, MachineProfileService>();
    services.AddScoped<IMachineSetupService, MachineSetupService>();
    services.AddScoped<IProcessService, ProcessService>();
    services.AddScoped<IProcessSamplingService, ProcessSamplingService>();
    services.AddScoped<IControlLimitService, ControlLimitService>();
    services.AddScoped<IChartControlService, ChartControlService>();


    services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "SPC IA API", Version = "v1.0" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });
}



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("x-file-name", "Content-Disposition"));

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.UseMiddleware<RequestLoggingMiddleware>();


    app.MapControllers();
}

// create hardcoded test users in db on startup
{


    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

}

{
    var parameters = new List<TableParametersSPC>();

    string[] lines = {
    "2\t2,121\t1,880\t2,659\t0.7979\t12,533\t0\t3,267\t0\t2,606\t1,128\t0.8865\t0.853\t0\t3,686\t0\t3,267",
    "3\t1,732\t1,023\t1,954\t0.8862\t11,284\t0\t2,568\t0\t2,276\t1,693\t0.5907\t0.888\t0\t4,358\t0\t2,574",
    "4\t1,500\t0,729\t1,628\t0.9213\t10,854\t0\t2,266\t0\t2,088\t2,059\t0.4857\t0.880\t0\t4,698\t0\t2,282",
    "5\t1,342\t0,577\t1,427\t0.9400\t10,638\t0\t2,089\t0\t1,964\t2,326\t0.4299\t0.864\t0\t4,918\t0\t2,114",
    "6\t1,225\t0,483\t1,287\t0.9515\t10,510\t0.030\t1,970\t0.029\t1,874\t2,534\t0.3946\t0.848\t0\t5,078\t0\t2,004",
    "7\t1,134\t0,419\t1,182\t0.9594\t10,423\t0.118\t1,882\t0.113\t1,806\t2,704\t0.3698\t0.833\t0.204\t5,204\t0.076\t1,924",
    "8\t1,061\t0,373\t1,099\t0.9650\t10,363\t0.185\t1,815\t0.179\t1,751\t2,847\t0.3512\t0.820\t0.388\t5,306\t0.136\t1,864",
    "9\t1,000\t0,337\t1,032\t0.9693\t10,317\t0.239\t1,761\t0.232\t1,707\t2,970\t0.3367\t0.808\t0.547\t5,393\t0.184\t1,816",
    "10\t0,949\t0,308\t0,975\t0.9727\t10,281\t0.284\t1,716\t0.276\t1,669\t3,078\t0.3249\t0.797\t0.687\t5,469\t0.223\t1,777",
    "11\t0,905\t0,285\t0,927\t0.9754\t10,252\t0.321\t1,679\t0.313\t1,637\t3,173\t0.3152\t0.787\t0.811\t5,535\t0.256\t1,744",
    "12\t0,866\t0,266\t0,886\t0.9776\t10,229\t0.354\t1,646\t0.346\t1,610\t3,258\t0.3069\t0.778\t0.922\t5,594\t0.283\t1,717",
    "13\t0,832\t0,249\t0,850\t0.9794\t10,210\t0.382\t1,618\t0.374\t1,585\t3,336\t0.2998\t0.770\t1,025\t5,647\t0.307\t1,693",
    "14\t0,802\t0,235\t0,817\t0.9810\t10,194\t0.406\t1,594\t0.399\t1,563\t3,407\t0.2935\t0.763\t1,118\t5,696\t0.328\t1,672",
    "15\t0,775\t0,223\t0,789\t0.9823\t10,180\t0.428\t1,572\t0.421\t1,544\t3,472\t0.2880\t0.756\t1,203\t5,741\t0.347\t1,653",
    "16\t0,750\t0,212\t0,763\t0.9835\t10,168\t0.448\t1,552\t0.440\t1,526\t3,532\t0.2831\t0.750\t1,282\t5,782\t0.363\t1,637",
    "17\t0,728\t0,203\t0,739\t0.9845\t10,157\t0.466\t1,534\t0.458\t1,511\t3,588\t0.2787\t0.744\t1,356\t5,820\t0.378\t1,622",
    "18\t0,707\t0,194\t0,718\t0.9854\t10,148\t0.482\t1,518\t0.475\t1,496\t3,640\t0.2747\t0.739\t1,424\t5,856\t0.391\t1,608",
    "19\t0,688\t0,187\t0,698\t0.9862\t10,140\t0.497\t1,503\t0.490\t1,483\t3,689\t0.2711\t0.734\t1,487\t5,891\t0.403\t1,597",
    "20\t0,671\t0,180\t0,680\t0.9869\t10,133\t0.510\t1,490\t0.504\t1,470\t3,735\t0.2677\t0.729\t1,549\t5,921\t0.415\t1,585",
    "21\t0,655\t0,173\t0,663\t0.9876\t10,126\t0.523\t1,477\t0.516\t1,459\t3,778\t0.2647\t0.724\t1,605\t5,951\t0.425\t1,575",
    "22\t0,640\t0,167\t0,647\t0.9882\t10,119\t0.534\t1,466\t0.528\t1,448\t3,819\t0.2618\t0.720\t1,659\t5,979\t0.434\t1,566",
    "23\t0,626\t0,162\t0,633\t0.9887\t10,114\t0.545\t1,455\t0.539\t1,438\t3,858\t0.2592\t0.716\t1,710\t6,006\t0.443\t1,557",
    "24\t0,612\t0,157\t0,619\t0.9892\t10,109\t0.555\t1,445\t0.549\t1,429\t3,895\t0.2567\t0.712\t1,759\t6,031\t0.451\t1,548",
    "25\t0,600\t0,153\t0,606\t0.9896\t10,105\t0.565\t1,435\t0.559\t1,420\t3,931\t0.2544\t0.708\t1,806\t6,056\t0.459\t1,541"
    };


    foreach (var line in lines)
    {
        var values = line.Split('\t');

        var tableParametersSPC = new TableParametersSPC
        {
            N = double.Parse(values[0], CultureInfo.InvariantCulture),
            A = double.Parse(values[1], CultureInfo.InvariantCulture),
            A2 = double.Parse(values[2], CultureInfo.InvariantCulture),
            A3 = double.Parse(values[3], CultureInfo.InvariantCulture),
            C4 = double.Parse(values[4], CultureInfo.InvariantCulture),
            _1_C4 = double.Parse(values[5], CultureInfo.InvariantCulture),
            B3 = double.Parse(values[6], CultureInfo.InvariantCulture),
            B4 = double.Parse(values[7], CultureInfo.InvariantCulture),
            B5 = double.Parse(values[8], CultureInfo.InvariantCulture),
            B6 = double.Parse(values[9], CultureInfo.InvariantCulture),
            D2 = double.Parse(values[10], CultureInfo.InvariantCulture),
            _1_D2 = double.Parse(values[11], CultureInfo.InvariantCulture),
            D3 = double.Parse(values[12], CultureInfo.InvariantCulture),
            D1 = double.Parse(values[13], CultureInfo.InvariantCulture),
            D2_1 = double.Parse(values[14], CultureInfo.InvariantCulture),
            D3_1 = double.Parse(values[15], CultureInfo.InvariantCulture),
            D4 = double.Parse(values[16], CultureInfo.InvariantCulture)
        };

        parameters.Add(tableParametersSPC);
    }

    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    
    if (dataContext.TableParametersSPC.ToList().Count == 0)
    {
        dataContext.AddRange(parameters);
        dataContext.SaveChanges();
    }
}

app.Run();