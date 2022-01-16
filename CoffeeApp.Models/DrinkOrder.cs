using CoffeeApp.Models.Abstraction;
using CoffeeApp.Models.Abstraction.Enums;

namespace CoffeeApp.Models;

public class DrinkOrder : IDrinkOrder
{
    private IDrink drink;

    public DrinkOrder(IDrink drink)
    {
        this.drink = drink;
    }

    public Drink GetDrink()
    {
        return drink.GetDrinkType() switch
        {
            DrinkType.Cappuccino => new Cappuccino(),
            DrinkType.Coffee => new Coffee(),
            DrinkType.CoffeeWithMilk => new CoffeeWithMilk(),
            DrinkType.Latte => new Latte(),
            _ => throw new ArgumentNullException($"Drink code does not exist")
        };
    }
}