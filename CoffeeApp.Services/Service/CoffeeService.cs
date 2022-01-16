using CoffeeApp.Models;
using CoffeeApp.Models.Abstraction;
using CoffeeApp.Services.Machine;

namespace CoffeeApp.Services.Service;

public class CoffeeService : ICoffeeService
{
    private readonly ICoffeeMachine _coffeeMachine;
    public CoffeeService(ICoffeeMachine coffeeMachine)
    {
        this._coffeeMachine = coffeeMachine;
    }

    public IDrinkOrder StartCoffeeMachine(int orderSelection)
    {
        return orderSelection switch
        {
            1 => new DrinkOrder(new Cappuccino()),
            2 => new DrinkOrder(new Coffee()),
            3 => new DrinkOrder(new Latte()),
            _ => throw new ArgumentException("Selection is invalid. Please try again")
        };
    }

    public (string Message, bool CanContinue) CheckBeanAndMilk(IDrink drink)
    {
        var message = _coffeeMachine.CheckBeanAndMilk(drink);
        bool canContinue = true;
        if (string.IsNullOrEmpty(message))
        {
            if (_coffeeMachine.RemainingBeanCount() <= 5)
            {
                message = $"Warning: You have {_coffeeMachine.RemainingBeanCount()} bean left";
            }
        }
        else
        {
            canContinue = false;
        }

        return (message, canContinue);
    }
    public bool DispenseDrink(IDrink drink)
    {
        return _coffeeMachine.DispenseDrink(drink);
    }
}