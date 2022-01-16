using System.Text;
using CoffeeApp.Models;
using CoffeeApp.Models.Abstraction;
using CoffeeApp.Models.Abstraction.Enums;
using CoffeeApp.Services.Machine;
using CoffeeApp.Services.Service;
using CSharpFunctionalExtensions;

namespace CoffeeApp;

public class CoffeeApplication
{
    private readonly ICoffeeService _coffeeService;

    public CoffeeApplication(ICoffeeService coffeeService)
    {
        _coffeeService = coffeeService;
    }

    public Result<IDrinkOrder> Start()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("Welcome to the ultimate coffee machine");
        stringBuilder.AppendLine();
        stringBuilder.Append("Please select the drink you would like?");
        stringBuilder.AppendLine();

        stringBuilder.Append("Cappuccino = 1");
        stringBuilder.AppendLine();
        stringBuilder.Append("Coffee = 2");
        stringBuilder.AppendLine();
        stringBuilder.Append("Latte = 3");
        stringBuilder.AppendLine();

        stringBuilder.Append("Enter your selection : ");
        
        Console.Write(stringBuilder.ToString());
        
        if (int.TryParse(Console.ReadLine(), out var selection))
        {
            return Result.Success(_coffeeService.StartCoffeeMachine(selection));
        }
        return Result.Failure<IDrinkOrder>("Selection is invalid. Please provide a valid selection");
    }

    public Result<IDrink> SelectDrink(IDrinkOrder drinkOrder)
    {
        IDrink drink = drinkOrder.GetDrink();
        return Result.Success(drink);
    }

    public Result Check(IDrink drink)
    {
        var check = _coffeeService.CheckBeanAndMilk(drink);
        if (check.CanContinue)
        {
            return Result.Success(check.Message);
        }
        return Result.Failure(check.Message);
    }

    public Result<IDrink> ConfirmDrink(IDrink drink)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"You have selected {drink.GetDrinkType().ToString()}. Are you sure you want to continue?");
        stringBuilder.AppendLine();
        stringBuilder.Append("Yes = 1");
        stringBuilder.AppendLine();
        stringBuilder.Append("No = 2");
        stringBuilder.AppendLine();
        stringBuilder.Append("Enter your selection : ");

        Console.Write(stringBuilder.ToString());
        if (int.TryParse(Console.ReadLine(), out var selection))
        {
            var canContinue = selection == 1;
            if (canContinue)
            {
                return drink.GetDrinkType() == DrinkType.Coffee ? ConfirmMilkSelection() : Result.Success(drink);
            }
            else
            {
                return Result.Failure<IDrink>("You have choose not to continue. Thank you for your time. Please come back again");
            }
            
        }
        return Result.Failure<IDrink>("Selection is invalid. Please provide a valid selection");
    }
    
    public Result DispenseDrink(IDrink drink)
    {
        if (_coffeeService.DispenseDrink(drink))
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.Append("YOUR DRINK IS READY!!!!");
            stringBuilder.AppendLine();
            stringBuilder.Append("..........................................................");
            stringBuilder.AppendLine();
            stringBuilder.Append("Do you want another drink?");
            stringBuilder.AppendLine();
            stringBuilder.Append("Yes = yes");
            stringBuilder.AppendLine();
            stringBuilder.Append("No = off");
            stringBuilder.AppendLine();
            stringBuilder.Append("Enter your selection : ");
        
            Console.Write(stringBuilder.ToString());

            var answer = Console.ReadLine();
            if (!string.IsNullOrEmpty(answer))
            {
                switch (answer)
                {
                    case "yes":
                        return Result.Success();
                    case "off":
                        return Result.Try(() => Environment.Exit(0));
                }
            }
            return Result.Failure<Tuple<int, int, bool>>("Selection is invalid. Please provide a valid selection");
        }
        
        return Result.Failure<Tuple<int, int, bool>>("This should not happen");


    }
    
    private Drink ConfirmMilkSelection()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Can we offer you some milk in your coffee?");
        stringBuilder.AppendLine();
        stringBuilder.Append("Yes = 1");
        stringBuilder.AppendLine();
        stringBuilder.Append("No = 2");
        stringBuilder.AppendLine();
        
        stringBuilder.Append("Enter your selection : ");
        
        Console.Write(stringBuilder.ToString());
        if (!int.TryParse(Console.ReadLine(), out var selection)) 
            return ConfirmMilkSelection();
        var milkNeeded = selection == 1;
        return milkNeeded ? new CoffeeWithMilk() : new Coffee();
    }

}