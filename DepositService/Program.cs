using DepositService.BlockExplorers;
using DepositService.BlockExplorers.Ltc;
using DepositService.Data;
using DepositService.Workers;
using Serilog;

System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
customCulture.NumberFormat.NumberDecimalSeparator = ".";
Thread.CurrentThread.CurrentCulture = customCulture;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder =>
    {
        builder.ClearProviders();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    })
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("Database");
        MigrationExtensions.RunDataContextMigrations<DataContext>(connectionString);
        
        services.AddTransient(_ => new DataContextFactory(connectionString));
        services.AddDbContext<DataContext>();
        services.AddSingleton<IBlockExplorer, LtcBlockExplorer>();
        services.AddHostedService<DebugWorker>();
        services.AddHostedService<LtcDepositConfirmationWorker>();
        services.AddHostedService<LtcDepositHistoryWorker>();
        
    })
    .Build();

await host.RunAsync();