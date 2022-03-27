using auto_mowers.Front.ArgumentParser;
using auto_mowers.Front.ArgumentParser.Contract;
using auto_mowers.Front.ArgumentParser.Exception;
using auto_mowers.Front.AutoMowersFront;
using auto_mowers.Front.AutoMowersFront.Contract;
using auto_mowers.Front.AutoMowersFront.Exception;
using auto_mowers.Front.FileParser;
using auto_mowers.Front.FileParser.Contract;
using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.MowerCommandService;
using auto_mowers.Services.MowerCommandService.Contract;
using auto_mowers.Services.MowerFront.Contract;
using auto_mowers.Services.MowerService;
using auto_mowers.Services.MowerService.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;

// use of ServiceCollection in order to use dependancy injection and to get a code easily transformable in web Api for example.
var serviceProvider = new ServiceCollection()
    .AddLogging(cfg => cfg.AddConsole())
    .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.None)  
    .AddTransient<Program>()
    .AddTransient<IArgumentParser, ArgumentParser>()
    .AddTransient<IMowersFront, MowersFront>()
    .AddTransient<IMowerCommandService, MowerCommandService>()
    .AddSingleton<IMowerService, MowerService>()
    .AddSingleton<ILawnService, LawnService>()
    .AddTransient<IFileParser, FileParser>()
    .AddTransient<IFileSystem, FileSystem>()
    .BuildServiceProvider();

var logger = serviceProvider.GetService<ILogger<Program>>();
var autoMowersFront = serviceProvider.GetService<IMowersFront>();

if (logger == null || autoMowersFront == null)
{
    Console.WriteLine("Application couldn't start normally");
    return 1;
}

logger.LogInformation("Program Starts");

int exit_code = autoMowersFront.Run(args);

logger.LogInformation("Program Ends");

return exit_code;

