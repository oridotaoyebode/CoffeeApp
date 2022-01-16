// See https://aka.ms/new-console-template for more information

using CoffeeApp;
using CoffeeApp.Services;
using CoffeeApp.Services.Machine;
using CoffeeApp.Services.Service;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddSingleton<ICoffeeService, CoffeeService>()
            .AddSingleton<ICoffeeMachine, CoffeeMachine>())
    .Build();

StartDrinkMachine(host.Services);

await host.RunAsync();


static void StartDrinkMachine(IServiceProvider serviceProvider)
{
    while (true)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var provider = serviceScope.ServiceProvider;
        var coffeeService = provider.GetRequiredService<ICoffeeService>();
        var coffeeApplication = new CoffeeApplication(coffeeService);
        var result = coffeeApplication.Start()
            .Bind(order => coffeeApplication.SelectDrink(order)
                .Check(coffeeApplication.Check)
                .Bind(drink => coffeeApplication.ConfirmDrink(drink)
                    .Bind(chosenDrink => coffeeApplication.DispenseDrink(chosenDrink))));
        
        if (!result.IsSuccess)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(result.Error);
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}